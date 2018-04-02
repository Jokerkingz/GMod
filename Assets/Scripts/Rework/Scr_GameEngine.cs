using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_GameEngine : MonoBehaviour {
	public static Scr_GameEngine gGE;
	public Material gMat_Fabricate;
	public LoadingOverlay cLO;
	public string vNextRoom;
	void Awake(){
		if (gGE != null){
			GameObject.Destroy(this);
			return;
			}
		else{
			gGE = this;
			DontDestroyOnLoad(this);}

	}
	void Update(){

		//if (Input.GetKeyDown(KeyCode.A)){
		//cLO.FadeIn();
		//}

	}
	public void fGotoNextRoom(string tScene){
		cLO.FadeIn();
		cLO.vIsChangingScene = true;
		vNextRoom = tScene;



	}
	public void fGoToScene(){
		//SceneManager.LoadScene(0);
		GameObject tTemp = GetComponent<OVRPlayerController>().gameObject;
		tTemp.transform.position = new Vector3(-.2f,65.37f,45.37f);
		cLO.FadeOut();
		SceneManager.LoadScene("Scene/Sce_CleanedRoom");
	}
}
