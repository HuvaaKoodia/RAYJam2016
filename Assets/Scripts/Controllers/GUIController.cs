using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
	public static GUIController I;

	public Image[] SkillImages;
	public CanvasGroup[] SkillGroups;
	public Text[] SkillAmounts;

	void Awake() 
	{
		I = this;
	}

	void Start()
	{
		GameController.I.Player.SkillSystem.OnSkillUsed += OnSkillUsed;
	}

	private void OnSkillUsed(int index, PlayerSkillBase skill)
	{
		StartCoroutine (SkillUsedCoroutine (index, skill));
	}

	IEnumerator SkillUsedCoroutine(int index, PlayerSkillBase skill)
	{
		if (skill.UsesLeft != -1)
			SkillAmounts [index].text = "" + skill.UsesLeft;
		else
			SkillAmounts [index].text = "";

		if (skill.UsesLeft == 0) 
		{
			SkillImages [index].fillAmount = 1;
			SkillGroups [index].alpha = 0.3f;
			
		} 
		else 
		{
			while (true) {
				SkillGroups [index].alpha = 1 - skill.CooldownPercent;
				SkillImages [index].fillAmount = 1 - skill.CooldownPercent;

				if (skill.CooldownPercent >= 1 || skill.CooldownPercent < 0)
					break;

				yield return null;
			}

			SkillImages [index].fillAmount = 1;
			SkillGroups [index].alpha = 1;
		}
	}

	public void UpdatePlayerSkills(PlayerSkillSystem skillSystem)
	{
		UpdatePlayerSkill (0, skillSystem);
		UpdatePlayerSkill (1, skillSystem);
		UpdatePlayerSkill (2, skillSystem);
	}

	private void UpdatePlayerSkill(int index, PlayerSkillSystem skillSystem)
	{
		if (skillSystem.SkillIDs [index] == SkillID.None)
			SkillGroups [index].alpha = 0;
		else {
			SkillImages [index].sprite = SkillzDatabase.I.GetIcon (skillSystem.SkillIDs [index]);
			SkillGroups [index].alpha = 1;
		}

		if (skillSystem.Skills[index].UsesLeft != -1)
			SkillAmounts [index].text = "" + skillSystem.Skills[index].UsesLeft;
		else
			SkillAmounts [index].text = "";
	}
}
