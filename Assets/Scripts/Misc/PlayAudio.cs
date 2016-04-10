using UnityEngine;
using System.Collections;

public class PlayAudio : MonoBehaviour
{
	public AudioSource[] Sources;

	public void Play()
	{
        var source = Sources[Random.Range(0, Sources.Length)];
        source.enabled = true;
		source.Play();
	}
}
