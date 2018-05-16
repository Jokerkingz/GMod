using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scr_GameEngine : MonoBehaviour {
	public static Scr_GameEngine gGE;
	public Material gMat_Fabricate;
	public LoadingOverlay cLO;
	public string vNextRoom;

	// Timer Setup
	public Text vTextTimer;
	private float vStartTime;

	void Awake(){
		GameObject tGS =  GameObject.FindGameObjectWithTag("GlobalSystem");
		if (tGS != null)
			vStartTime = tGS.GetComponent<Scr_Game_System>().gStartTime;
		else
			fStartTimer();

		if (gGE != null){
			GameObject.Destroy(this);
			return;
			}
		else{
			gGE = this;
			DontDestroyOnLoad(this);}

	}
	void Update(){

		float t = Time.time - vStartTime;	

		string minutes = ((int)t / 60).ToString ();
		string seconds = (t % 60).ToString ("f0");

		vTextTimer.text = minutes + ":" + seconds;

	}

	public void fStartTimer(){
		vStartTime = Time.time;
	}

	public void fGotoNextRoom(string tScene){
		cLO.FadeIn();
		cLO.vIsChangingScene = true;
		vNextRoom = tScene;



	}
	public void fGoToScene(){
		//SceneManager.LoadScene(0);
		GameObject tTemp = GetComponentInChildren<OVRPlayerController>().gameObject;
		tTemp.transform.position = new Vector3(-.2f,65.37f,45.37f);
		cLO.FadeOut();
		SceneManager.LoadScene(vNextRoom);
		Destroy(this.gameObject);
	}
}
