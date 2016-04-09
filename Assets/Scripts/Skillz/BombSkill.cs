using UnityEngine;
using System.Collections;

public class BombSkill: PlayerSkillBase 
{
           


	public Bomb BombPrefab;
	public float BulletSpeed = 12f;

	protected override void OnActivate ()
	{
		var bomb = Instantiate (BombPrefab, transform.position + skillSystem.MouseDirection * 2f, Quaternion.identity) as Bomb;
		bomb.Rigidbody.AddForce (skillSystem.MouseDirection * BulletSpeed, ForceMode2D.Impulse);
	}

}
