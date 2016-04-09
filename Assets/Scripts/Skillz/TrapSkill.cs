using UnityEngine;
using System.Collections;

public class TrapSkill : MonoBehaviour
{

    public Trap TrapPrefab;
    // Use this for initialization
    protected override void OnActivate()
    {
        Instantiate(TrapPrefab, transform.position, Quaternion.identity);
    }
}
