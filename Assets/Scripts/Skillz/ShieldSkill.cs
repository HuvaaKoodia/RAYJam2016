using UnityEngine;
using System.Collections;

public class ShieldSkill: PlayerSkillBase 
{
	public GameObject ShieldObject;

	public float ShieldDuration = 2f;

    public PlayAudio ShieldAudio;

	protected override void OnActivate ()
	{
		StopCoroutine ("ShieldCoroutine");
		StartCoroutine ("ShieldCoroutine");
	}

	IEnumerator ShieldCoroutine()
	{
		ShieldObject.SetActive (true);

        ShieldAudio.Play();

		yield return new WaitForSeconds (ShieldDuration);

		ShieldObject.SetActive (false);
	}

}
