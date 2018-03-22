using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Elevator : MonoBehaviour {

	private Animation anim;
	public AudioClip[] audioClip;
	void Start () {
		anim= this.gameObject.GetComponent<Animation>();
	}
	
	void Update () {
		//------TESTING
		if (Input.GetKeyDown(KeyCode.L))
		{ElevatorStart();}
	}

	void PlaySound(int clip)
	{
		AudioSource audio = GetComponent<AudioSource> ();

		audio.clip = audioClip [clip];
		audio.Play ();

	}
	void ElevatorStart()
	{
		anim.Play();
	}
	//ACCESSED VIA ANIMATION EVENTS
	public void SoundElevatorFloorSwitchBeep()
	{PlaySound(0);}

	public void SoundElevatorDestinationBeep()
	{PlaySound(1);}

	public void SoundElevatorStartUp()
	{PlaySound(2);}
	public void SoundElevatorEndThud()
	{PlaySound(3);}
}
