using UnityEngine;
using System.Collections;

public class PSAutoDestroy : MonoBehaviour
{
	public ParticleSystem PS;

	void Start()
	{
		if (!PS)
			PS = GetComponent<ParticleSystem> ();
	}

	void Update () 
	{
		if (!PS.IsAlive() && PS.particleCount == 0)
			Destroy (gameObject);
	}
}
