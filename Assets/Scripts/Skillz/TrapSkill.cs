using UnityEngine;
using System.Collections;

public class TrapSkill : PlayerSkillBase
{

    public Trap TrapPrefab;
    // Use this for initialization
    protected override void OnActivate()
    {
        Instantiate(TrapPrefab, transform.position, Quaternion.identity);
    }
}
