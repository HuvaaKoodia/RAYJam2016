using UnityEngine;
using System.Collections;

public class RapidFireSkill: PlayerSkillBase 
{
	public Bullet BulletPrefab;
	public float BulletSpeed = 5f;

	public float BulletSpreadAngle = 10f;

	protected override void OnActivate ()
	{
		var bullet = Instantiate (BulletPrefab, transform.position + skillSystem.MouseDirection * 1f, Quaternion.identity) as Bullet;

		float angle = BulletSpreadAngle * Random.Range(-1f,1f);

		bullet.Ridigbody.AddForce (Quaternion.AngleAxis(angle, Vector3.forward) * skillSystem.MouseDirection * BulletSpeed, ForceMode2D.Impulse);
	}
}
