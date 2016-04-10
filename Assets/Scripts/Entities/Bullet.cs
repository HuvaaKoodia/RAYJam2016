using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public Rigidbody2D Ridigbody;

	public float Lifetime = -1;

	IEnumerator Start()
	{
		if (Lifetime == -1)
			Lifetime = 100;

		yield return new WaitForSeconds (Lifetime);

		Destroy (gameObject);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		var enemy = collision.gameObject.GetComponent<EnemyMovement> ();
		enemy.Die (true);

		Destroy (gameObject);
	}
}
