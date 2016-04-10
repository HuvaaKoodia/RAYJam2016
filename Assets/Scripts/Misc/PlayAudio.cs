using UnityEngine;
using System.Collections;

public class PlayAudio : MonoBehaviour
{
	public AudioSource[] Sources;

	public void Play()
	{
		Sources [Random.Range (0, Sources.Length)].Play();
	}
}
