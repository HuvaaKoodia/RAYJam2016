using UnityEngine;
using System.Collections;

public class PlayerSkillSystem : MonoBehaviour 
{
	public Vector3 MouseDirection {get {return mouseDirection;}}
	private Vector3 mouseDirection;

	public PlayerSkillBase[] Skills;

	void Awake()
	{
		if (Skills == null || Skills.Length != 3) 
		{
			Skills = new PlayerSkillBase[3];
		}

	}

	void Update () 
	{
		//calculate mouse direction
		mouseDirection= (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
		mouseDirection.z = 0;

		//check skill input
		if (Skills [0] != null) 
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				Skills [0].Activate ();
			}
		}
		if (Skills [1] != null) 
		{
			if (Input.GetMouseButtonDown (2)) 
			{
				Skills [1].Activate ();
			}
		}
		if (Skills [2] != null) 
		{
			if (Input.GetMouseButtonDown (1)) 
			{
				Skills [2].Activate ();
			}
		}
	}

	public void ReplaceSkill(int index, SkillID id)
	{
		Skills[index] = gameObject.GetComponent (SkillzDatabase.I.GetSkill (id)) as PlayerSkillBase;
	}

	public void RemoveSkill(int index, SkillID id)
	{
		Skills [index] = null;
	}
} 
