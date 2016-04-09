using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour 
{
	public GameObject ExplosionPrefab;
	public Rigidbody2D Rigidbody;
	public float Lifetime = 1f, ExplosionRadius = 5f;

	IEnumerator Start () 
	{
		yield return new WaitForSeconds(Lifetime);

		Destroy (gameObject);
	}


	void OnDestroy()
	{
		//DESTROY ENEMIESdw
		var enemies = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius, Layers.Enemy);

		for (int i = 0; i < enemies.Length; i++)
		{
			GameObject.Destroy (enemies [i].gameObject);
		}

		//PARTICLES
		Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
	}
}
