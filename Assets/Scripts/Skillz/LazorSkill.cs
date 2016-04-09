using UnityEngine;
using System.Collections;

public class LazorSkill : PlayerSkillBase
{
    public float shakeDuration;
    public float shakeIntensity;

    public Lazor LazorPrefab;

    protected override void OnActivate()
    {
        //angle hack
        float angle = Vector3.Angle(Vector3.up, skillSystem.MouseDirection);
        if (skillSystem.MouseDirection.x > 0)
            angle = -angle;

        //SHAKE
        CameraControl.I.StartShake(shakeDuration, shakeIntensity);

        Instantiate(LazorPrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
    }
}
