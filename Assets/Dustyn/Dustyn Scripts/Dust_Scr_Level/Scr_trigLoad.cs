using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_trigLoad : MonoBehaviour {

	public string roomToLoad;
	public string roomToUnload;

	public float loadTimer;
	public float unloadTimer;
	private Collider trigCollider;
	void Start () {
		trigCollider = this.gameObject.GetComponent<Collider>();
	}
	void OnTriggerEnter (Collider col)
	{
		if (col.tag =="Player")
		{
			trigCollider.enabled=false;
			if (roomToLoad !=null)
			{
				
				StartCoroutine(LoadNextRoom());
			}
			if (roomToUnload !=null)
			{
				StartCoroutine(UnloadPreviousRoom());
			}
		}
	}


	IEnumerator LoadNextRoom()
	{
		yield return new WaitForSeconds(loadTimer);
		Scr_SceneManager.Instance.LoadNext(roomToLoad);
	}

	IEnumerator UnloadPreviousRoom()
	{
		yield return new WaitForSeconds(unloadTimer);
		Scr_SceneManager.Instance.UnloadPrevious(roomToUnload);
	}
}
