using UnityEngine;
using System.Collections;

public class EnemyDeathAnimation : MonoBehaviour
{
	public GameObject DeathPrefab;
	public AnimationCurve ScaleCurve;
	public float AnimationSpeedMulti = 1f;

	public void Animate()
	{
		transform.SetParent (null, true);
		StartCoroutine (DeathCoroutine ());
	}

	IEnumerator DeathCoroutine ()
	{
		float percent = 0;
		while (percent < 1)
		{
			transform.localScale = Vector3.one * (1+ScaleCurve.Evaluate(percent));
			percent += Time.deltaTime * AnimationSpeedMulti;
			yield return null;
		}

		Instantiate (DeathPrefab, transform.position, Quaternion.identity);

		Destroy (gameObject);
		yield break;
	}
}
