using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_DebugMenu : MonoBehaviour {
	public Text vTxtLeftAnalogX;
	public Text vTxtLeftAnalogY;
	public Text vTxtRytAnalogX;
	public Text vTxtRytAnalogY;
	public Text vTxtDpadX;
	public Text vTxtDpadY;
	public Text vTxtLIndexTrigger;
	public Text vTxtRIndexTrigger;
	public Text vTxtLThumbPress;
	public Text vTxtRThumbPress;
	public Text vTxtLIndexTrigger2nd;
	public Text vTxtRIndexTrigger2nd;
	public Text vTxtLIndexTouch;
	public Text vTxtRIndexTouch;
	public Text vTxtLGrip;
	public Text vTxtRGrip;

	public float vFltLeftAnalogX;
	public float vFltLeftAnalogY;
	public float vFltRytAnalogX;
	public float vFltRytAnalogY;
	public float vFltDpadX;
	public float vFltDpadY;
	public bool vBolLIndexTrigger;
	public bool vBolRIndexTrigger;
	public bool vBolLThumbPress;
	public bool vBolRThumbPress;
	public bool vBolLIndexTrigger2nd;
	public bool vBolRIndexTrigger2nd;
	public bool vBolLIndexTouch;
	public bool vBolRIndexTouch;
	public bool vBolLGrip;
	public bool vBolRGrip;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float tFloat;
		string tTemp;
		vTxtLeftAnalogX.text = "Left Thumb Stick X: " + Input.GetAxis("Oculus_GearVR_LThumbstickX");
		vTxtLeftAnalogY.text = "Left Thumb Stick Y: " + Input.GetAxis("Oculus_GearVR_LThumbstickY");
		vTxtRytAnalogX.text = "Right Thumb StickX: " + Input.GetAxis("Oculus_GearVR_RThumbstickX");
		vTxtRytAnalogY.text = "Right Thumb StickY: " +Input.GetAxis("Oculus_GearVR_RThumbstickY");
		/*
		vTxtDpadX.text = "Dpad X: " +Input.GetAxis("Oculus_GearVR_DpadX");
		vTxtDpadY.text = "Dpad Y: " +Input.GetAxis("Oculus_GearVR_DpadY");
		vTxtLIndexTrigger.text = "L Index Trigger: " + Input.GetButton("Oculus_GearVR_LIndexTrigger");
		vTxtRIndexTrigger.text = "R Index Trigger: " + Input.GetButton("Oculus_GearVR_RIndexTrigger");
		*/

		//if (Input.GetButton("OGVR_LThumbPress")) tTemp = "True"; else tTemp = "False";
		vTxtLThumbPress.text = "L Thumb Press: " + Input.GetButton("OGVR_LThumbPress");
		//if (Input.GetButton("OGVR_RThumbPress")) tTemp = "True"; else tTemp = "False";
		vTxtRThumbPress.text = "R Thumb Press: " + Input.GetButton("OGVR_RThumbPress");
		//if (Input.GetButton("OGVR_LIndexTrigger")) tTemp = "True"; else tTemp = "False";
		vTxtLIndexTrigger2nd.text = "L Index Trigger: " + Input.GetAxis("OGVR_LIndexTrigger");
		//if (Input.GetButton("OGVR_RIndexTrigger")) tTemp = "True"; else tTemp = "False";
		vTxtRIndexTrigger2nd.text = "R Index Trigger: " + Input.GetAxis("OGVR_RIndexTrigger");
		//if (Input.GetButton("OGVR_LIndexTouch")) tTemp = "True"; else tTemp = "False";
		vTxtLIndexTouch.text = "L Index Touch: " + Input.GetAxis("OGVR_LIndexTouch");
		//if (Input.GetButton("OGVR_RIndexTouch")) tTemp = "True"; else tTemp = "False";
		vTxtRIndexTouch.text = "R Index Touch: " + Input.GetAxis("OGVR_RIndexTouch");
		//if (Input.GetButton("OGVR_LGrip")) tTemp = "True"; else tTemp = "False";
		vTxtLGrip.text = "L Grip: " + Input.GetAxis("OGVR_LGrip");
		//if (Input.GetButton("OGVR_RGrip")) tTemp = "True"; else tTemp = "False";
		vTxtRGrip.text = "R Grip: " + Input.GetAxis("OGVR_RGrip");
		//vTxtLIndexTrigger.text = "L Index Trigger: " + Input.GetAxis("Oculus_GearVR_LIndexTrigger");
		//vTxtRIndexTrigger.text = "R Index Trigger: " + tTemp;


	}
}
