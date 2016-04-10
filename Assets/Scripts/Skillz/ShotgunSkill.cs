using UnityEngine;
using System.Collections;

public class ShotgunSkill: PlayerSkillBase 
{
    public float shakeDuration;
    public float shakeIntensity;

	public Bullet BulletPrefab;
	public float BulletSpeed = 5f;

	public float BulletSpreadAngle = 90f;

    public PlayAudio ShotgunAudio;

	protected override void OnActivate ()
	{
		var bullet = Instantiate (BulletPrefab, transform.position + skillSystem.MouseDirection * 1f, Quaternion.identity) as Bullet;

		float angle = -BulletSpreadAngle * 0.5f + (BulletSpreadAngle / ShotsPerActivation) * currentActivation;

		bullet.Ridigbody.AddForce (Quaternion.AngleAxis(angle, Vector3.forward) * skillSystem.MouseDirection * BulletSpeed, ForceMode2D.Impulse);

        //SHAKE
        CameraControl.I.StartShake(shakeDuration, shakeIntensity);

        ShotgunAudio.Play();
	}
}
