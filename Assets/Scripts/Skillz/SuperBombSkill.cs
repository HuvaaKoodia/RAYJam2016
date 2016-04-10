using UnityEngine;
using System.Collections;

public class SuperBombSkill: PlayerSkillBase 
{
	public GameObject ExplosionPrefab;
	public float ExplosionRadius;

	protected override void OnActivate ()
	{
		//DESTROY ENEMIES
		var enemies = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius, Layers.Enemy);

		for (int i = 0; i < enemies.Length; i++)
		{
			var enemy = enemies [i].GetComponent<EnemyMovement> ();
			enemy.Die (true);
		}

		CameraControl.I.StartShake (1.5f, 10);

		Instantiate (ExplosionPrefab, transform.position + skillSystem.MouseDirection * 2f, Quaternion.identity);
	}

}
