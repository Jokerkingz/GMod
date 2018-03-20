using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_doorGoal : MonoBehaviour {

	private Animation anim;
	//private AudioSource audioSource;
	public AudioClip[] audioClip;
	//public AnimationClip[] animationClip;
	public bool unlocked;

	void Start () {

		anim = this.gameObject.GetComponent<Animation>();
		//unlocked=false;
	}
	
	
	void Update () {

		//-----TESTING, SWITCH WITH VR CONTROLS LATER
		if (Input.GetKeyDown(KeyCode.Space)&&!unlocked)
		{
			//CAN PRESS BUTTON REGARDLESS BUT WILL PLAY AN "ACCESS DENIED" SOUND
			PlaySound(2);
		}
		if (Input.GetKeyDown(KeyCode.Space)&&unlocked)
		{
			PlaySound(1);
			DoorOpen();
		}

		if(Input.GetKeyDown(KeyCode.A))
		{
			DoorClose();
		}

	}


		void PlaySound(int clip)
	{
		AudioSource audio = GetComponent<AudioSource> ();

		audio.clip = audioClip [clip];
		audio.Play ();

	}
		/*void PlayAnimation(int aniClip)
		{
			anim = this.gameObject.GetComponent<Animation>();
			anim.clip= animationClip [aniClip];
			anim.Play();
		}*/

	public void DoorUnlocked()
	{
		//PLAYER CAN OPEN DOOR NOW
		PlaySound(0);
		unlocked =true;
	}

	 void DoorOpen()
	{
		PlaySound(3);
		//PlayAnimation(1)
		anim.Play(anim.clip.name="ani_doorOpen");
		//anim.Play("ani_doorOpen");
		
	}

	public void DoorClose()
	{
		PlaySound(4);
		//PlayAnimation(0);
		anim.Play(anim.clip.name="ani_doorClose");
		//anim.Play("ani_doorClose");
	}

	//ACCESSED VIA ANIMATION EVENT
	public void SoundDoorClosedBang()
	{
		PlaySound(5);
	}
}
