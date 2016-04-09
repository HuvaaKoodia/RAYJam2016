using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
	public static GUIController I;

	public Image Skill1, Skill2, Skill3;

	void Awake() 
	{
		I = this;
	}

	void Start()
	{

	}

	public void UpdatePlayerSkills(PlayerSkillSystem skillSystem)
	{
		if (skillSystem.SkillIDs [0] == SkillID.None)
			Skill1.enabled = false;
		else {
			Skill1.sprite = SkillzDatabase.I.GetIcon (skillSystem.SkillIDs [0]);
			Skill1.enabled = true;
		}

		if (skillSystem.SkillIDs [1] == SkillID.None)
			Skill2.enabled = false;
		else {
			Skill2.sprite = SkillzDatabase.I.GetIcon (skillSystem.SkillIDs [1]);
			Skill2.enabled = true;
		}

		if (skillSystem.SkillIDs [2] == SkillID.None)
			Skill3.enabled = false;
		else {
			Skill3.sprite = SkillzDatabase.I.GetIcon (skillSystem.SkillIDs [2]);
			Skill3.enabled = true;
		}

	}
}
