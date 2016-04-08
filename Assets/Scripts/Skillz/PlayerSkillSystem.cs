using UnityEngine;
using System.Collections;

public class PlayerSkillSystem : MonoBehaviour 
{
	public Vector3 MouseDirection {get;private set;}

	public PlayerSkillBase Skill1;

	void Start () 
	{
	
	}

	void Update () 
	{
		//calculate mouse direction
		MouseDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

		//check skill input
		if (Skill1) 
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				Skill1.Activate ();
			}
		}
	}
}
