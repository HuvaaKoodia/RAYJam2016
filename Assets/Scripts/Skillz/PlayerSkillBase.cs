using UnityEngine;
using System.Collections;

public class PlayerSkillBase : MonoBehaviour 
{
	//TODO cooldown etc
	protected PlayerSkillSystem skillSystem;
	protected PlayerMovement movementSystem;

	public float CooldownDelay = -1, UsesPerRound = -1, ShotsPerActivation = 1, DelayBetweenActivations = 0.5f;
	private float cooldownTimer = -1, usesLeft = -1;

	private bool activationRunning = false;

	void Awake()
	{
		skillSystem = GetComponent<PlayerSkillSystem>();
		movementSystem= GetComponent<PlayerMovement>();
	}

	void Start()
	{
		Refresh ();
	}

	public void Activate()
	{
		if (activationRunning)
			return;

		if (Time.time < cooldownTimer)
			return ;

		if (usesLeft == 0)
			return;

		StartCoroutine (ActivateCoroutine ());
	}

	public IEnumerator ActivateCoroutine()
	{
		activationRunning = true;

		cooldownTimer = Time.time + CooldownDelay;
		if (usesLeft > 0)
			usesLeft--;

		for (int i = 0; i < ShotsPerActivation; i++) 
		{
			OnActivate ();

			if (i < ShotsPerActivation - 1)
				yield return new WaitForSeconds (DelayBetweenActivations);
		}

		activationRunning = false;
		yield break;
	}

	protected virtual void OnActivate()
	{
		
	}

	public void Refresh()
	{
		cooldownTimer = 0;
		usesLeft = UsesPerRound;
	}
}
