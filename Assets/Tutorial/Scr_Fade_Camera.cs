using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Fade_Camera : MonoBehaviour {
	public GameObject[] vCameraArray;
	public Canvas[] vCanvasList;
	public Image[] vImageList;
	public float vAlpha;
	public OVRScreenFade cOVRSCF;
	// Use this for initialization
	void Start () {
		vCameraArray = GameObject.FindGameObjectsWithTag("MainCamera");

		for (int i = 0; i < 3; i++) {
			vCanvasList[i].renderMode = RenderMode.ScreenSpaceCamera;
			vCanvasList[i].worldCamera = vCameraArray[i].GetComponent<Camera>();
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.A))
			cOVRSCF.StartFade();
			/*vState = "FadeOut";
		if (vState == "FadeOut"){
			StartCoroutine(FadeOut());
			}*/


		vAlpha += .1f*Time.deltaTime;
		if (vAlpha >1f)
			vAlpha =1f;
		foreach (Image tImage in vImageList)
			tImage.color = new Vector4(1,1,1,vAlpha);
	}
}
