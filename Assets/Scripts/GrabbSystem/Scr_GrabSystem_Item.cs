﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GrabSystem_Item : MonoBehaviour {
    public bool vIsGripped;
    public bool vIsGrippedByRight;
    public GameObject vHandObj;
    public GameObject vMainObject;
    public Transform vTransformAdjustment;
    public Rigidbody cRB;
    // Use this for initialization
    void Start () {
        cRB = vMainObject.GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void fGrabBegin(Scr_GrabSystem_Main tGripper)
    {

        cRB.isKinematic = true;
        cRB.useGravity = false;
        //vIsGrabbed = true;
        Scr_ModSystem_Handler tHandle = vMainObject.GetComponent<Scr_ModSystem_Handler>();
        if (tHandle != null)
        {
            if (tHandle.vHolsterConnectedTo != null)
            {
                Scr_Belt_Holsters tHolster = tHandle.vHolsterConnectedTo.GetComponent<Scr_Belt_Holsters>();
                if (tHolster != null)
                {
                    tHolster.fRemoveHandle(vMainObject.gameObject);
                }
            }
        }


        vIsGripped = true;
        vIsGrippedByRight = tGripper.vIsTheRightHand;
        vHandObj = tGripper.gameObject;

        
        Scr_Male_Socket tNewSocket = vMainObject.GetComponent<Scr_Male_Socket>();
        if (tNewSocket != null)
        {
            tNewSocket.enabled = true;
            tNewSocket.vConnectableSockets.Clear();
            tNewSocket.Detach(vMainObject.gameObject);
        }
        
        //m_grabbedBy = hand;
       // m_grabbedCollider = grabPoint;
        //gameObject.GetComponent<Rigidbody>().isKinematic = true;


    }

    public void fGrabEnd()
    {
        vIsGripped = false;
        vHandObj = null;
        if (tag != "Display")
        {
            cRB.isKinematic = false;
            cRB.useGravity = true;
            cRB.velocity *= 1.5f;
            cRB.angularVelocity *= 1.5f;
        }


        Scr_Male_Socket tNewSocket = vMainObject.GetComponent<Scr_Male_Socket>();
        if (tNewSocket != null)
        {
            tNewSocket.enabled = false;
            tNewSocket.CheckForAttach();
            tNewSocket.vConnectableSockets.Clear();
        }

        Scr_ModSystem_Handler tHandle = vMainObject.GetComponent<Scr_ModSystem_Handler>();
        if (tHandle != null)
        {
            if (tHandle.vHolsterShowHollowTo != null)
            {
                Scr_Belt_Holsters tHolster = tHandle.vHolsterShowHollowTo.GetComponent<Scr_Belt_Holsters>();
                if (tHolster != null)
                {
                    tHolster.fReceiveHandle(vMainObject.gameObject);
                }
            }
        }
        //this.GetComponentInChildren<Scr_SocketF>().RemoveAttachement(this.gameObject);

        //Scr_Male_Socket[] tTmp = GetComponentsInChildren<Scr_Male_Socket>();
        //foreach (Scr_Male_Socket tThat in tTmp)
        //	tThat.CheckForAttach();
    }
}
