using UnityEngine;
using System.Collections;

public class Lazor : MonoBehaviour 
{
	public float Lifetime = 1f;
	public AnimationCurve SizeOverLifetimeCurve;
	public Transform GraphicsParent;
	public BoxCollider2D Collider;

	public Vector3 Normal = Vector3.up;

	IEnumerator Start ()
	{
		float colliderHeight = Collider.size.y;
		//GraphicsParent.localPosition += Normal * height *0.5f ;
		float time = 0;

		while (true) 
		{

			GraphicsParent.localScale = new Vector3 (1,SizeOverLifetimeCurve.Evaluate (time / Lifetime), 1);
			Collider.size = new Vector3 (SizeOverLifetimeCurve.Evaluate (time / Lifetime), colliderHeight, 1);

			time += Time.deltaTime;

			if (time > Lifetime)
				break;

			yield return null;
		}
		Destroy (gameObject);
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		var enemy = collider.gameObject.GetComponent<EnemyMovement> ();
		enemy.Die (true);
	}
}
