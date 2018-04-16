using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Part_Information : MonoBehaviour {
	public float vYOffSet;
	public GameObject vSource;
	public string vStatus = "Start";
	public string vTextToSay;
	private string vTextToShow =""; 
	public Text cText;
	public GameObject vTarget;
	public float vEnduce;

	public LineRenderer cLR;
	public Transform vCanvas;
	public Sprite[] vSpriteList;
	public Image vImage;
	public OVRGrabbable vRoot;
	private float vDelayTime;
	// Use this for initialization
	void Start () {
		cText.text = "";
		vTarget = GameObject.FindGameObjectWithTag("MainCamera");
		vRoot = vSource.transform.root.gameObject.GetComponent<OVRGrabbable>();

	}
	public void fImageUpdate(int tIndex){
		vImage.sprite = vSpriteList[tIndex];
	}
	// Update is called once per frame
	void Update () {
	transform.LookAt(vTarget.transform);
	switch (vStatus){
		case "Start":
			vYOffSet += .6f*Time.deltaTime;
			if (vYOffSet > .1f){
				vYOffSet = .1f;
				vStatus = "Type";
				}
			Vector3 vOffset = new Vector3(0f,vYOffSet,0f);
			cLR.SetPosition(1,vOffset);
			vCanvas.transform.localPosition = vOffset;
		break;
		case "Type":
		if (vDelayTime > 0){
			vDelayTime -= Time.deltaTime;
			break;
			}
		else vDelayTime = .025f;
		if (vTextToSay.Length > 1)
			{vTextToShow += vTextToSay.Remove(1);
			vTextToSay = vTextToSay.Remove(0,1);}
		else if (vTextToSay.Length > 0){
			vTextToShow += vTextToSay;
			vTextToSay = "";}
		else{//vStatus = "Death";
			}
			cText.text = vTextToShow;
		break;

		case "Death":
		if (vDelayTime > 0){
			
			vDelayTime -= Time.deltaTime;
			break;
			}
		else vDelayTime = .05f;
			if (vTextToShow.Length > 0)
			{vTextToShow = vTextToShow.Remove(vTextToShow.Length-1);
			}
		else {Destroy(this.gameObject);
			}
			cText.text = vTextToShow;
		break;
		}
		//if (vRoot.vIsBeingGripped){
		//	vStatus = "Death";
		//	cLR.enabled = false;
		//	}
		if (vSource == null){
			vStatus = "Death";
			cLR.enabled = false;
		}
	}
}
