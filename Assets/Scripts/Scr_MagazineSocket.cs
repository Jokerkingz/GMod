using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_MagazineSocket : MonoBehaviour {
	public GameObject vHologram;

	public GameObject vSource;

	public GameObject vAttachedObject;

	public Material vMaterial;
	private float vOpl;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (vOpl > 0){
			vOpl -= .5f;
			vHologram.transform.position = this.transform.position;
			vHologram.transform.eulerAngles = this.transform.eulerAngles;

		}
		else if (vHologram != null){
			Destroy(vHologram.gameObject);
			vHologram = null;


		}
		vOpl = Mathf.Clamp(vOpl,0f,1f);
	}
	/*public void RemoveAttachement(GameObject tReference){
		if (vAttachedObject == tReference){
			tReference.transform.SetParent(null);
			tReference.GetComponent<Rigidbody>().useGravity = true;
			tReference.GetComponent<Rigidbody>().isKinematic = false;
			tReference.GetComponent<Scr_Socket>().enabled = true;
			tReference.GetComponent<OVRGrabbable>().enabled = true;
			}

	}
	*/
	public void AcceptPart(GameObject tReference,string tName){
		vAttachedObject = tReference;
		tReference.transform.SetParent(this.transform);
		tReference.transform.localPosition= Vector3.zero;
		tReference.transform.eulerAngles = this.transform.eulerAngles;
		tReference.GetComponent<Rigidbody>().useGravity = false;
		tReference.GetComponent<Rigidbody>().isKinematic = true;
		tReference.GetComponent<Scr_Socket>().enabled = false;
		tReference.GetComponent<OVRGrabbable>().enabled = false;
	}
	public void ShowHollogram(GameObject tReference, string tName){
		vOpl += 1f;
		if (vHologram == null){
			vHologram = Instantiate(tReference.gameObject) as GameObject;
			vHologram.GetComponent<Scr_Socket>().enabled = false;



			//Angle Correction
			vHologram.transform.localEulerAngles = tReference.transform.eulerAngles;




			Collider[] tList =  vHologram.GetComponentsInChildren <Collider>();
			foreach (Collider tC in tList)
				tC.enabled = false;

			Renderer[] tListA =  vHologram.GetComponentsInChildren <Renderer>();
			foreach (Renderer tR in tListA)
				tR.material = vMaterial;
				/*
			SphereCollider[] tListA =  vHologram.GetComponentsInChildren <SphereCollider>();
			foreach (SphereCollider tSC in tListA)
				tSC.enabled = false;
			BoxCollider[] tListB =  vHologram.GetComponentsInChildren <BoxCollider>();
			foreach (BoxCollider tBC in tListB)
				tBC.enabled = false;
			CapsuleCollider[] tListC =  vHologram.GetComponentsInChildren <CapsuleCollider>();
			foreach (CapsuleCollider tCC in tListC)
				tCC.enabled = false;
				*/
		}
	}

}
