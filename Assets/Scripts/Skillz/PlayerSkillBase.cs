using UnityEngine;
using System.Collections;

public class PlayerSkillBase : MonoBehaviour 
{
	//TODO cooldown etc
	protected PlayerSkillSystem skillSystem;
	protected PlayerMovement movementSystem;

	public int UsesLeft{get{ return usesLeft;}}

	public float CooldownDelay = -1;
	private float cooldownTimer = -1;

	public int UsesPerRound = -1, ShotsPerActivation = 1;
	private int usesLeft = -1;

	public float DelayBetweenActivations = 0.5f;

	private bool activationRunning = false;

	public float CooldownPercent { get { return (cooldownTimer - Time.time) / CooldownDelay; }}

	protected int currentActivation = 0;

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
			currentActivation = i;
			OnActivate ();
			if (i < ShotsPerActivation - 1 && DelayBetweenActivations > 0)
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
