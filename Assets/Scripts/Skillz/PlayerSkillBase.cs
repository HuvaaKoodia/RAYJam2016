using UnityEngine;
using System.Collections;

public class PlayerSkillBase : MonoBehaviour 
{
	//TODO cooldown etc
	protected PlayerSkillSystem skillSystem;

	void Awake()
	{
		skillSystem = GetComponent<PlayerSkillSystem>();
	}

	public virtual void Activate()
	{
		
	}
}
