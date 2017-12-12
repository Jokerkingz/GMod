using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SocketF : MonoBehaviour {
	public GameObject vHologram;

	public GameObject vSource;

	public GameObject vAttachedObject;

	//public GameObject vPartConnected;
	public GameObject vPseudoPart;
	public GameObject vOriginalPart;
	public Material vMaterialGood;
	public Material vMaterialBad;
	private float vOpl;

	private GameObject tSource;
	public List<Scr_CollisionCheck> tCollideList = new List<Scr_CollisionCheck>();

	private Scr_SubStatus cSS;

	public Vector3 vHollowAngle;
	public string vPartType;



	private AudioSource cAS;
	public AudioClip vSFX;
	void Start(){
		cAS = this.GetComponent<AudioSource>();
		cSS = GetComponent<Scr_SubStatus>();
		if (cSS = null)
			Debug.Log(this.name);
	}

	void Update () {
		Material tMat = vMaterialGood;
		if (vOpl > 0){
			vOpl -= .5f;
			vHollowAngle = Reorientate(tSource);
			vHologram.transform.localEulerAngles = vHollowAngle;
			foreach (Scr_CollisionCheck tSample in tCollideList){
				if (tSample.vHere > 0f){
					tMat = vMaterialBad;
					}
			}

			Renderer[] tListA =  vHologram.GetComponentsInChildren <Renderer>();
			foreach (Renderer tR in tListA){
				Material[] tNew = new Material[tR.materials.Length];
				for(int i = 0; i < tR.materials.Length;i++)
					tNew[i] = tMat;
				tR.materials = tNew;
				}
		}
		else if (vHologram != null){
			Destroy(vHologram.gameObject);
			tSource = null;
			vHologram = null;


		}
		vOpl = Mathf.Clamp(vOpl,0f,1f);
	}

	public void RemoveAttachement(GameObject tReference){
		tReference.transform.SetParent(null);
		Transform[] tOldParts = this.GetComponentsInChildren<Transform>();
		foreach (Transform tThat in tOldParts) {
			if ((tThat.tag != "GripPart") && (this.gameObject != tThat.gameObject)){
				tThat.SetParent(tReference.transform);
			}
		}
		tReference.GetComponent<Rigidbody>().useGravity = true;
		tReference.GetComponent<Rigidbody>().isKinematic = false; 
		tCollideList.Clear();
	}

	public void AcceptPart(GameObject tReference,string tName){
		bool tIsCollisionFree = true;
		foreach (Scr_CollisionCheck tSample in tCollideList){
			if (tSample.vHere > 0f){
				tIsCollisionFree = false;
				}
			}
		if (tIsCollisionFree){
		// Attache the following[
			vAttachedObject = tReference;
			cAS.PlayOneShot(vSFX);
			tReference.transform.SetParent(this.transform);
			tReference.transform.localPosition= Vector3.zero;
			tReference.transform.localEulerAngles = vHollowAngle;//Reorientate(tReference);//+this.transform.eulerAngles;
			tReference.GetComponent<Rigidbody>().useGravity = false;
			tReference.GetComponent<Rigidbody>().isKinematic = true; 
			Transform[] tSubParts = tReference.GetComponentsInChildren<Transform>();
			foreach (Transform tThat in tSubParts) {
				if (tThat.tag != "GripPart")
					tThat.SetParent(this.transform);
			}
			tReference.transform.SetParent(this.transform);
			vOriginalPart = tReference.gameObject;
			tReference.GetComponent<Scr_Socket>().BroadCastThis("NewEquiped");
			tReference.GetComponent<Scr_Socket>().vAttachedTo = this.gameObject;
			tReference.SetActive(true);
			}
		//return this.gameObject;
	}
	public void ShowHollogram(GameObject tReference, string tName){
		if (this.GetComponentInParent<OVRGrabbable>().vIsBeingGripped){
			vOpl += 1f;
		if (vHologram == null && GameObject.FindGameObjectsWithTag("Hollow").Length <= 0){
			GameObject tSkipThis;
			tCollideList.Clear();
			tSource = tReference;
			vHologram = Instantiate(tReference.gameObject) as GameObject;
			vHologram.GetComponent<Scr_Socket>().enabled = false;
			vHologram.transform.SetParent(this.transform);
			vHologram.transform.localPosition= Vector3.zero;
			vHologram.transform.eulerAngles = Reorientate(tReference);
			vHologram.BroadcastMessage("TurnOff","Hollow");
			Collider[] tList =  vHologram.GetComponentsInChildren <Collider>();
			foreach (Collider tC in tList){
				if (tC.GetComponent<Scr_CollisionCheck>() == null)
					tC.enabled = false;
				}

			Scr_CollisionCheck tSkipCheck = tReference.GetComponentInChildren<Scr_CollisionCheck>();
			tSkipThis = tSkipCheck.gameObject;
			tCollideList.Clear();
			Scr_CollisionCheck[] tSubCheck = vHologram.GetComponentsInChildren <Scr_CollisionCheck>();
			foreach (Scr_CollisionCheck tCC in tSubCheck){
				tCollideList.Add(tCC);
				tCC.vException = tSkipThis.gameObject;
						//tCC.enabled = tr;
				}
			Renderer[] tListA =  vHologram.GetComponentsInChildren <Renderer>();
			foreach (Renderer tR in tListA){
				Material[] tNew = new Material[tR.materials.Length];
				for(int i = 0; i < tR.materials.Length;i++)
					tNew[i] = vMaterialGood;
				tR.materials = tNew;
				}
			}
		}
	}

	Vector3 Reorientate(GameObject tObject){
		Vector3 tNewVect = tObject.transform.localEulerAngles;
		OVRGrabbable tCheck = this.GetComponentInParent<OVRGrabbable>();
		GameObject tObj = tCheck.gameObject;
		Vector3 tOwnVect = tObj.transform.eulerAngles;
		//tNewVect.x = tNewVect.x-tOwnVect.x;
		tNewVect.y = tNewVect.y-tOwnVect.y;
		//tNewVect.z = tNewVect.z-tOwnVect.z;

		//tNewVect.x = tNewVect.x;
		//tNewVect.y = tNewVect.y;
		//tNewVect.z = tNewVect.z;

		//tNewVect.x = tOwnVect.x;
		//tNewVect.y = tOwnVect.y;
		//tNewVect.z = tOwnVect.z;
		//if (tNewVect.x >360) tNewVect.x -= 360f;
		if (tNewVect.y >360) tNewVect.y -= 360f;
		//if (tNewVect.z >360) tNewVect.z -= 360f;

		tNewVect.x = 0;//((Mathf.Round(tNewVect.x/90f))*90f);
		tNewVect.y = ((Mathf.Round(tNewVect.y/90f))*90f);
		tNewVect.z = 0;//((Mathf.Round(tNewVect.z/90f))*90f);

		//tNewVect.x = ((Mathf.Round((tNewVect.x+tOwnVect.x)/90f))*90f);
		//tNewVect.y = ((Mathf.Round((tNewVect.y-tOwnVect.y)/90f))*90f);
		//tNewVect.z = ((Mathf.Round((tNewVect.z-tOwnVect.z)/90f))*90f);
		//if (tNewVect.x >360) tNewVect.x -= 360f;
		//if (tNewVect.y >360) tNewVect.y -= 360f;
		//if (tNewVect.z >360) tNewVect.z -= 360f;
		return tNewVect;
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
