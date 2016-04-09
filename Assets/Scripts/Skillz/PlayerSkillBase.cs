using UnityEngine;
using System.Collections;

public class PlayerSkillBase : MonoBehaviour 
{
	//TODO cooldown etc
	protected PlayerSkillSystem skillSystem;
	protected PlayerMovement movementSystem;

	void Awake()
	{
		skillSystem = GetComponent<PlayerSkillSystem>();
		movementSystem= GetComponent<PlayerMovement>();
	}

	public void Activate()
	{
		OnActivate ();
	}

	protected virtual void OnActivate()
	{
	}
}
