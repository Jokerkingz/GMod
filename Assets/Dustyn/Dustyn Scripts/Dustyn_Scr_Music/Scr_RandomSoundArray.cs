using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_RandomSoundArray : MonoBehaviour {

public AudioClip[] audioClip;
public int soundToPlay;


void Start()
{
 soundToPlay = (Random.Range (0, audioClip.Length));
 PlaySound(soundToPlay);
}

void Update()
{
	//---TESTING
	/*if (Input.GetKeyDown(KeyCode.O))
	{
		soundToPlay = (Random.Range (0, audioClip.Length));
 		PlaySound(soundToPlay);
	}*/
}

void PlaySound(int clip)
	{
		AudioSource audio = GetComponent<AudioSource> ();

		audio.clip = audioClip [clip];
		audio.Play ();

	}
}
