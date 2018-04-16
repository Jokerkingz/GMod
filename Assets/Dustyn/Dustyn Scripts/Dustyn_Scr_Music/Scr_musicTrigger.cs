using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_musicTrigger : MonoBehaviour {

	private Collider trigCollider;
	public Scr_NewMusicManager musicManager;

	[Header("Purpose")]
	public bool stopMusic;
	public bool playSpecialMusic;
	void Start () {
		musicManager= FindObjectOfType<Scr_NewMusicManager>();
		trigCollider = this.gameObject.GetComponent<Collider>();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag=="Player")
		{
		
		if (stopMusic)
		{
			musicManager.StopMusic();
		}

		if (playSpecialMusic)
		{
			musicManager.PlayFinalRoomMusicIntro();
		}

		trigCollider.enabled=false;
		}
	}
}
