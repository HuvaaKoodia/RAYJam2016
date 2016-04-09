using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GUIController : MonoBehaviour
{
	public static GUIController I;

	public Image[] SkillImages;
	public CanvasGroup[] SkillGroups;
	public Text[] SkillAmounts;

	public RectTransform CanvasRect;

	public SlotMachine[] SlotMachines;
	public RectTransform SlotMachinePanel;
	public AnimationCurve SlotMachineAppearCurve;

	private List<SkillID> allSkills;

	private float slotHeight;
	private int currentMachine = 0;

	void Awake() 
	{
		I = this;
	}
		
	void Start()
	{
		SlotMachineVisible = false;

		allSkills = new List<SkillID> ();

		for (int i = 0; i < (int)SkillID._AmountSkills; i++) 
		{
			allSkills.Add ((SkillID)i);
		}

		GameController.I.Player.SkillSystem.OnSkillUsed += OnSkillUsed;

		slotHeight = CanvasRect.sizeDelta.y + 50;
		SlotMachinePanel.anchoredPosition = Vector3.up * slotHeight;
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
			while (true) 
			{
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

	private void UpdatePlayerSkill(int index, PlayerSkillSystem skillSystem, bool showText = true)
	{
		if (skillSystem.SkillIDs [index] == SkillID.None) {
			SkillGroups [index].alpha = 0;
			SkillAmounts [index].text = "";
			return;
		}
		else if (skillSystem.SkillIDs [index] == SkillID.DestroySkill) 
		{
			SkillImages [index].sprite = SkillzDatabase.I.GetIcon (skillSystem.SkillIDs [index]);
			SkillGroups [index].alpha = 1;
			StartCoroutine (FadeSkillIconCoroutine (index));
		}
		else 
		{
			SkillImages [index].sprite = SkillzDatabase.I.GetIcon (skillSystem.SkillIDs [index]);
			SkillGroups [index].alpha = 1;
		}
	
		if (showText && skillSystem.Skills[index].UsesLeft != -1)
			SkillAmounts [index].text = "" + skillSystem.Skills[index].UsesLeft;
		else
			SkillAmounts [index].text = "";
	}

	private IEnumerator FadeSkillIconCoroutine(int index)
	{
		yield return new WaitForSeconds (0.67f);

		while (SkillGroups[index].alpha > 0) 
		{
			SkillGroups [index].alpha -= Time.deltaTime;
			yield return null;
		}
	}

	public void ShowSlotMachinePanel()
	{
		SlotMachineVisible = true;
		StartCoroutine(SlotMachineCoroutine(1));

		currentMachine = 0;

		var deadlySkills = new List<SkillID> (allSkills);
		int amountOfSkulls = 3;

		for (int i = 0; i < amountOfSkulls; i++) 
		{
			deadlySkills.Insert(Random.Range(0, deadlySkills.Count), SkillID.DestroySkill);	
		}

		foreach (var machine in SlotMachines) 
		{
			machine.SetItems (new List<SkillID>(deadlySkills));
			machine.ToggleSpin ();
		}

		StartCoroutine (SlotMachineInputCoroutine ());
	}

	public void HideSlotMachinePanel()
	{
		StartCoroutine(SlotMachineCoroutine(-1));
	}

	private bool slotMachineMoving = false;

	public bool SlotMachineVisible { get; private set;}
	public float SlotMachineAppearSpeed = 1f;

	IEnumerator SlotMachineCoroutine(int direction)
	{
		slotMachineMoving = true;
		float percent = 0;
		while (percent < 1)
		{
			percent += Time.deltaTime * SlotMachineAppearSpeed;

			float percent2 = percent;
			if (direction < 0)
				percent2 = 1 - percent;

			SlotMachinePanel.anchoredPosition = Vector3.up * slotHeight * SlotMachineAppearCurve.Evaluate (percent2);

			yield return null;
		}
		slotMachineMoving = false;
		if (direction < 0) SlotMachineVisible = false;
	}

	IEnumerator SlotMachineInputCoroutine()
	{
		while (slotMachineMoving)
			yield return null;

		UpdatePlayerSkill (0, GameController.I.Player.SkillSystem, false);
		UpdatePlayerSkill (1, GameController.I.Player.SkillSystem, false);
		UpdatePlayerSkill (2, GameController.I.Player.SkillSystem, false);

		while (true)
		{
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown(0)) 
			{
				if (currentMachine == 3) 
				{
					HideSlotMachinePanel ();

					//remove useless skills
					for (int i = 0; i < 3; i++) {
						if (GameController.I.Player.SkillSystem.SkillIDs [i] == SkillID.DestroySkill) 
						{
							GameController.I.Player.SkillSystem.RemoveSkill (i);
						}
					}

					UpdatePlayerSkills (GameController.I.Player.SkillSystem);

					yield break;
				}
				else
				{
					SlotMachines [currentMachine].ToggleSpin ();

					while (SlotMachines [currentMachine].Spinning) yield return null;

					//set skill
					var selectedID = SlotMachines [currentMachine].SelectedID;

					GameController.I.Player.SkillSystem.ReplaceSkill(currentMachine, SlotMachines [currentMachine].SelectedID);

					UpdatePlayerSkill (currentMachine, GameController.I.Player.SkillSystem, false);

					//rig the next machine
					++currentMachine;

					if (currentMachine == 3)
						continue;

					var deadlySkills = new List<SkillID> (allSkills);
					deadlySkills.Remove (selectedID);
					int amountOfSkulls = 3;

					for (int i = 0; i < amountOfSkulls; i++) 
					{
						deadlySkills.Insert(Random.Range(0, deadlySkills.Count), SkillID.DestroySkill);	
					}

					SlotMachines [currentMachine].SetItems (deadlySkills);
				}
			}
			yield return null;
		}
	}
}
