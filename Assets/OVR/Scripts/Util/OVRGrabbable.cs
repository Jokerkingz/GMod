﻿/************************************************************************************

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.3 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculus.com/licenses/LICENSE-3.3

Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/

using System;
using UnityEngine;

/// <summary>
/// An object that can be grabbed and thrown by OVRGrabber.
/// </summary>
public class OVRGrabbable : MonoBehaviour
{
    [SerializeField]
    protected bool m_allowOffhandGrab = true;
    [SerializeField]
    protected bool m_snapPosition = false;
    [SerializeField]
    protected bool m_snapOrientation = false;
    [SerializeField]
    protected Transform m_snapOffset;
    [SerializeField]
    protected Collider[] m_grabPoints = null;

    protected bool m_grabbedKinematic = false;
    protected Collider m_grabbedCollider = null;
    protected OVRGrabber m_grabbedBy = null;

	public bool vIsBeingGripped;
	public string vIsGrippedBy;
    public GameObject vHandObj;
    public bool vIsGunHandle;
    private Scr_ModHandle cMH;
	/// <summary>
	/// If true, the object can currently be grabbed.
	/// </summary>
    public bool allowOffhandGrab
    {
        get { return m_allowOffhandGrab; }
    }

	/// <summary>
	/// If true, the object is currently grabbed.
	/// </summary>
    public bool isGrabbed
    {
        get { return m_grabbedBy != null; }
    }

	/// <summary>
	/// If true, the object's position will snap to match snapOffset when grabbed.
	/// </summary>
    public bool snapPosition
    {
        get { return m_snapPosition; }
    }

	/// <summary>
	/// If true, the object's orientation will snap to match snapOffset when grabbed.
	/// </summary>
    public bool snapOrientation
    {
        get { return m_snapOrientation; }
    }

	/// <summary>
	/// An offset relative to the OVRGrabber where this object can snap when grabbed.
	/// </summary>
    public Transform snapOffset
    {
    	// This is original
		 get {return m_snapOffset;}
		 /*
		get {
			Transform tNew = new Transform;
			tNew.transform.position=m_snapOffset.localPosition;
			tNew.eulerAngles=m_snapOffset.localEulerAngles;
			tNew.localScale=m_snapOffset.localScale;
			return tNew; }
			*/
    }

	/// <summary>
	/// Returns the OVRGrabber currently grabbing this object.
	/// </summary>
    public OVRGrabber grabbedBy
    {
        get { return m_grabbedBy; }
    }

	/// <summary>
	/// The transform at which this object was grabbed.
	/// </summary>
    public Transform grabbedTransform
    {
        get { return m_grabbedCollider.transform; }
    }

	/// <summary>
	/// The Rigidbody of the collider that was used to grab this object.
	/// </summary>
    public Rigidbody grabbedRigidbody
    {
        get { return m_grabbedCollider.attachedRigidbody; }
    }

	/// <summary>
	/// The contact point(s) where the object was grabbed.
	/// </summary>
    public Collider[] grabPoints
    {
        get { return m_grabPoints; }
    }
	/// <summary>
	/// Notifies the object that it has been grabbed.
	/// </summary>
	virtual public void GrabBegin(OVRGrabber hand, Collider grabPoint)
	{	
	Scr_ModSystem_Handler tHandle = this.GetComponent<Scr_ModSystem_Handler>();
		if (tHandle != null){
			if (tHandle.vHolsterConnectedTo != null){
				Scr_Belt_Holsters tHolster = tHandle.vHolsterConnectedTo.GetComponent<Scr_Belt_Holsters>();
					if (tHolster != null){
					tHolster.fRemoveHandle(this.gameObject);
				}
			}
		}


		vIsBeingGripped = true;
		vIsGrippedBy = hand.vWhichHand;
		vHandObj = hand.gameObject;
		Scr_Male_Socket tNewSocket = this.GetComponent<Scr_Male_Socket>();
		if (tNewSocket!=null){
			tNewSocket.enabled = true;
			tNewSocket.vConnectableSockets.Clear();
			tNewSocket.Detach(this.gameObject);
			}

		/*Scr_Socket tCheck = this.GetComponent<Scr_Socket>();
		if (tCheck!=null){
			FastList<GameObject> tTemp = new FastList<GameObject>();
			tTemp.Clear();
			tCheck.Detach(tTemp);
			}
			*/
        m_grabbedBy = hand;
        m_grabbedCollider = grabPoint;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

		
    }

	/// <summary>
	/// Notifies the object that it has been released.
	/// </summary>
	virtual public void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
	{	vIsBeingGripped = false;
		vHandObj = null;
		if (tag != "Display"){
	        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
	        rb.isKinematic = m_grabbedKinematic;
			rb.isKinematic = false;
			rb.useGravity = true;
	        rb.velocity = linearVelocity;
	        rb.angularVelocity = angularVelocity;
	        }
	        m_grabbedBy = null;
	        m_grabbedCollider = null;


		Scr_Male_Socket tNewSocket = this.GetComponent<Scr_Male_Socket>();
		if (tNewSocket!=null){
			tNewSocket.enabled = false;
			tNewSocket.CheckForAttach();
			tNewSocket.vConnectableSockets.Clear();
			}

		Scr_ModSystem_Handler tHandle = this.GetComponent<Scr_ModSystem_Handler>();
		if (tHandle != null){
			if (tHandle.vHolsterShowHollowTo != null){
				Scr_Belt_Holsters tHolster = tHandle.vHolsterShowHollowTo.GetComponent<Scr_Belt_Holsters>();
					if (tHolster != null){
					tHolster.fReceiveHandle(this.gameObject);
				}
			}
		}
		//this.GetComponentInChildren<Scr_SocketF>().RemoveAttachement(this.gameObject);

		//Scr_Male_Socket[] tTmp = GetComponentsInChildren<Scr_Male_Socket>();
		//foreach (Scr_Male_Socket tThat in tTmp)
		//	tThat.CheckForAttach();
    }

    void Awake()
    {
        if (m_grabPoints.Length == 0)
        {
            // Get the collider from the grabbable
            Collider collider = this.GetComponent<Collider>();
            if (collider == null)
            {	
				throw new ArgumentException("Grabbables cannot have zero grab points and no collider -- please add a grab point or collider.");
            }

            // Create a default grab point
            m_grabPoints = new Collider[1] { collider };
        }
    }

    protected virtual void Start()
	{	Rigidbody tRB = GetComponent<Rigidbody>();
		cMH = GetComponent<Scr_ModHandle>();
		if (tRB != null)
        m_grabbedKinematic = GetComponent<Rigidbody>().isKinematic;
    }

    void OnDestroy()
    {
        if (m_grabbedBy != null)
        {
            // Notify the hand to release destroyed grabbables
            m_grabbedBy.ForceRelease(this);
        }
    }

    void Update(){
		if (((vIsGrippedBy == "Right" && Input.GetAxis("OGVR_RIndexTrigger") > 0.8f) || (vIsGrippedBy == "Left" && Input.GetAxis("OGVR_LIndexTrigger") > 0.8f)) && vIsGunHandle && vIsBeingGripped && cMH != null){
			cMH.fTriggerPressed();
			//this.gameObject.BroadcastMessage("Triggered",SendMessageOptions.DontRequireReceiver);
			//Debug.Log("Triggered Button");
			}

    }
    public void TurnOff(string vWhy){
    	switch (vWhy){
    	case "Hollow":
    		this.enabled = false;
    		this.tag = "Hollow";
    	break;
    	}
    }
}
