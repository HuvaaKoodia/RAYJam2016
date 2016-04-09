using UnityEngine;
using System.Collections;

public class PlayerSkillSystem : MonoBehaviour 
{
	public Vector3 MouseDirection {get {return mouseDirection;}}
	public Vector3 MousePosition { get {return mousePosition;}}
	private Vector3 mousePosition, mouseDirection;

	public PlayerSkillBase[] Skills;
	public SkillID[] SkillIDs;

	void Awake()
	{
		if (Skills == null || Skills.Length != 3) 
		{
			Skills = new PlayerSkillBase[3];
		}
		SkillIDs = new SkillID[3];
	}

	void Update () 
	{
		//calculate mouse stats
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 0;
		mouseDirection = (MousePosition - transform.position).normalized;

		//check skill input
		if (Skills [0] != null)
		{
			if (Input.GetMouseButton (0)) 
			{
				Skills [0].Activate ();
			}
		}

		if (Skills [1] != null) 
		{
			if (Input.GetMouseButton (2)) 
			{
				Skills [1].Activate ();
			}
		}

		if (Skills [2] != null) 
		{
			if (Input.GetMouseButton (1)) 
			{
				Skills [2].Activate ();
			}
		}
	}

	public void ReplaceSkill(int index, SkillID id)
	{
		Skills[index] = gameObject.GetComponent (SkillzDatabase.I.GetSkill (id)) as PlayerSkillBase;
		SkillIDs [index] = id;
	}

	public void RemoveSkill(int index, SkillID id)
	{
		Skills [index] = null;
		SkillIDs [index] = SkillID.None;
	}

	public void RefreshSkills()
	{
		for (int i = 0; i < 3; i++) {
			Skills [i].Refresh ();
		}

	}
} 
