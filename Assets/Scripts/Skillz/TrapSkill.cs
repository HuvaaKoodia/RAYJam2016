using UnityEngine;
using System.Collections;

public class TrapSkill : PlayerSkillBase
{
    public Trap TrapPrefab;
    public PlayAudio TrapAudio;

    // Use this for initialization
    protected override void OnActivate()
    {
        Instantiate(TrapPrefab, transform.position, Quaternion.identity);
        TrapAudio.Play();
    }
}
