using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Door : MonoBehaviour {

[Header("ROOMS TO LOAD/UNLOAD")]
//private Scr_SceneManager sceneManager;
public string roomToLoad;
public string roomToUnload;

[Header("DOOR PROPERTIES")]

public GameObject doorToDelete;
public AudioClip[] audioClip;
public bool isUnlocked;
 public bool canInteract;
 public bool doorActive;
public KeyCode openDoor = KeyCode.Space;
private Animation anim;
public bool IsDebug;

	void Start () {
		//sceneManager = FindObjectOfType<Scr_SceneManager>();
		anim = this.gameObject.GetComponent<Animation>();
	}
	

	void Update () {

		//---TESTING, NEED TO INPUT ACTUAL VR CONTROLS
		if (canInteract &&Input.GetKeyDown(openDoor) &&isUnlocked &&!doorActive)
		{
			doorActive=true;
			PlaySound(1);
			StartLoadingNext();
			StartUnloadingPrevious();
			DeleteUnnecessaryDoors();
			Debug.Log("Opening");
			
		}

		if (canInteract &&Input.GetKeyDown(openDoor) &&!isUnlocked)
		{
			PlaySound(2);
			Debug.Log("Locked");
		}


	}


//--- LOADING/UNLOADING SECTION
	void StartLoadingNext()
	{
		if (roomToLoad !=null)
		{	if (!IsDebug){
			Scr_SceneManager.Instance.LoadNext(roomToLoad);
			fDoorOpen();
			}
		}

	}

	void StartUnloadingPrevious()
	{	
		if (roomToUnload !=null)
		{
			Scr_SceneManager.Instance.UnloadPrevious(roomToUnload);
		}
	}

	void DeleteUnnecessaryDoors()
	{
		if (doorToDelete !=null)
		{
			Destroy(doorToDelete);
		}
	}

//--- ANIMATION AND SOUNDS FOR DOOR
		void PlaySound(int clip)
	{
		AudioSource audio = GetComponent<AudioSource> ();

		audio.clip = audioClip [clip];
		audio.Play ();

	}

	public void DoorUnlocked()
	{
		//PLAYER CAN OPEN DOOR NOW
		PlaySound(0);
		isUnlocked =true;
	}

	public void fDoorOpen()
	{
		/*doorActive=true;
		StartLoadingNext();
		StartUnloadingPrevious();
		DeleteUnnecessaryDoors();
		Debug.Log("Opening");*/
		
		//if (!doorActive)
		//{
		//doorActive=true;
		PlaySound(3);
		anim.Play(anim.clip.name="ani_doorOpen");
		//}
	}

	public void DoorClose()
	{
		PlaySound(4);
		anim.Play(anim.clip.name="ani_doorClose");
	}

	//ACCESSED VIA ANIMATION EVENT
	public void SoundDoorClosedBang()
	{
		PlaySound(5);
	}

}
