using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Elevator : MonoBehaviour {

	private Animation anim;
	[Header("Elevator Properties")]
	public AudioClip[] audioClip;
	public GameObject invisibleWall;

	[Header("Loading/Unloading")]
	public string roomToLoad;
	public string roomToUnload;
	void Start () {
		anim= this.gameObject.GetComponent<Animation>();
		invisibleWall.GetComponent<Collider>().enabled=false;
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

	public void ElevatorDoorOpen()
	{
		PlaySound(4);
		invisibleWall.GetComponent<Collider>().enabled=false;
		anim.Play(anim.clip.name="ani_elevatOpen");	
	}

	public void ElevatorDoorClose()
	{
		PlaySound(5);
		invisibleWall.GetComponent<Collider>().enabled=true;
		anim.Play(anim.clip.name="ani_elevatClose");
	}
	public void ElevatorStart()
	{
		anim.Play(anim.clip.name="ani_elevatMove");
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


	//UNLOADING AND LOADING ROOMS, ACCESSED VIA ANIMATION EVENTS
	public void ElevatorLoading()
	{
		Scr_SceneManager.Instance.LoadNext(roomToLoad);
	}
	public void ElevatorUnloading()
	{
		Scr_SceneManager.Instance.LoadNext(roomToUnload);
	}
}
