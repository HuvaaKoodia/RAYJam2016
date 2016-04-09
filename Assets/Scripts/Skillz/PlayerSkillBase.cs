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

	public void Activate()
	{
		OnActivate ();
	}

	protected virtual void OnActivate()
	{

	}
}
