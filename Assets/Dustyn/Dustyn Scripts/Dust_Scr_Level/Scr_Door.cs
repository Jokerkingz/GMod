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
		if (!isUnlocked){
			HandSymbolState[] HandList = GetComponentsInChildren<HandSymbolState>();
			foreach (HandSymbolState Hand in HandList){
				Hand.active = false;
				}	
			}
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

	//--- REAL TESTING 
		if (Input.GetKeyDown(KeyCode.A))
		{fDoorOpen();}

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

		HandSymbolState[] HandList = GetComponentsInChildren<HandSymbolState>();
		foreach (HandSymbolState Hand in HandList){
			Hand.active = true;
			}	
		
	}

	public void fDoorOpen()
	{
		/*doorActive=true;
		StartLoadingNext();
		StartUnloadingPrevious();
		DeleteUnnecessaryDoors();
		Debug.Log("Opening");*/
		
		if (!doorActive &&isUnlocked)
		{
		doorActive=true;
		StartLoadingNext();
		StartUnloadingPrevious();
		DeleteUnnecessaryDoors();
		PlaySound(1);
		//anim.Play(anim.clip.name="ani_SideDoorOpen");
		StartCoroutine(AllowHolopadSoundToPlay());
		}

		if (!isUnlocked)
		{
		PlaySound(2);
		}
	}
	private IEnumerator AllowHolopadSoundToPlay()
	{
		yield return new WaitForSeconds (1);
		ActuallyOpenDoor();
	}

	void ActuallyOpenDoor()
	{
		PlaySound(3);
		anim.Play(anim.clip.name="ani_SideDoorOpen");
	}
	public void DoorClose()
	{
		PlaySound(4);
		anim.Play(anim.clip.name="ani_SideDoorClose");
	}

	//ACCESSED VIA ANIMATION EVENT
	public void SoundDoorClosedBang()
	{
		PlaySound(5);
	}

}
