using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_FabricationCollective : MonoBehaviour {
	public float vMeter;
	public float vMaxMeter = 1f;
	public Material vSourceMaterial;
	public Material vMaterialClone;
	private List<Scr_FabricationData> vFabricateList = new List<Scr_FabricationData>();
	public Vector3 vLockSpot;
	public bool vIsLocked = true;
	public bool vHasStarted;
	public bool vDontChange;
	// Use this for initialization
	public void Start () {
		if (vHasStarted)
			return;
		vHasStarted = true;
		vSourceMaterial = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scr_GameEngine>().gMat_Fabricate;
		Renderer[] tThose = GetComponentsInChildren<Renderer>();
		Scr_FabricationData tCFD;
		vMaterialClone = new Material(vSourceMaterial);
		foreach (Renderer tThat in tThose){
			tCFD = tThat.gameObject.AddComponent<Scr_FabricationData>();
			tCFD.vOriginalMaterial = tThat.material;
			tCFD.vRenderSource = tThat;
			vFabricateList.Add(tCFD);
			tThat.material = vMaterialClone;
		}
		Rigidbody cRB = GetComponent<Rigidbody>();
		if (cRB != null && !vDontChange){
			cRB.useGravity = false;
			cRB.isKinematic = true;
			}
		vMaterialClone.SetFloat("_DissolveAmount",1f-(vMeter/vMaxMeter));
	}
	
	// Update is called once per frame
	void Update () {
	vMeter += Time.deltaTime;
	if (vIsLocked)
		transform.position = vLockSpot;
	if (vMaterialClone != null){
		if (vMeter < vMaxMeter){
//			Debug.Log("Changing proprties " + (1f-(vMeter/vMaxMeter)).ToString());
			vMaterialClone.SetFloat("_DissolveAmount",1f-(vMeter/vMaxMeter));
			}
		else{vMeter = vMaxMeter;
			vMaterialClone.SetFloat("_DissolveAmount",1f-(vMeter/vMaxMeter));
			foreach (Scr_FabricationData tFD in vFabricateList){
				tFD.vRenderSource.material = tFD.vOriginalMaterial;
				Destroy(tFD,1f);
				}
			Rigidbody cRB = GetComponent<Rigidbody>();
			if (cRB != null && !vDontChange){
				cRB.useGravity = true;
				cRB.isKinematic = false;
				}
			Destroy(vMaterialClone);
			Destroy(this);
			}
		}
	}
	/* Save until and in case there are multiple material per model
	void ChangeAllMaterials(){
		Renderer[] tThose = GetComponentsInChildren<Renderer>();
		Scr_FabricationData tCFD;
		vMaterialClone = new Material(vSourceMaterial);
		foreach (Renderer tThat in tThose){
			tCFD = tThat.gameObject.AddComponent<Scr_FabricationData>();
			tCFD.vOriginalMaterial = tThat.material;
			vFabricateList.Add(tCFD);
			tThat.material = vMaterialClone;

		}
	}*/
	/*
	public void fStartReloading(GameObject tSource, float tTimeTillCompletion,Scr_Mod_Magazine tMM, Scr_GameEngine cGE){
		vOriginalModel = tSource;
		cMMSource = tMM;
		vOriginalMaterial = tMM.vMagazineToPop.GetComponent<Renderer>().material;
		cRdr = tMM.vMagazineToPop.GetComponent<Renderer>(); 
		cRdr
		vMaxMeter = tTimeTillCompletion;
		vSourceMaterial = cGE.gMat_Fabricate;
		//vMaterialClone = new Material(Scr_GameEngine.gMat_Fabricate);
		vMaterialClone = new Material(vSourceMaterial);
		cRdr.material = vMaterialClone;

		vMaterialClone.SetFloat("_node_3736",1f-(vMeter/vMaxMeter));
		//	vSourceMaterial.SetFloat("_node_3736",vMeter);
	}*/

	void OnDestroy(){
		if (vMaterialClone != null)
			Destroy(vMaterialClone);
	}
}
