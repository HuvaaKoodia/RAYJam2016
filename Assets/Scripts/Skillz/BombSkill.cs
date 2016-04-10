using UnityEngine;
using System.Collections;

public class BombSkill: PlayerSkillBase 
{
	public Bomb BombPrefab;
	public float BulletSpeed = 12f;
    public PlayAudio BombAudio;

	protected override void OnActivate ()
	{
		var bomb = Instantiate (BombPrefab, transform.position + skillSystem.MouseDirection * 2f, Quaternion.identity) as Bomb;
        BombAudio.Play();
		bomb.Rigidbody.AddForce (skillSystem.MouseDirection * BulletSpeed, ForceMode2D.Impulse);
        bomb.Rigidbody.AddTorque(Random.Range(-1f,1f),ForceMode2D.Impulse);
	}

}
