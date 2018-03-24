using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Mod_AutoReload : MonoBehaviour {
	public float vMeter;
	public float vMaxMeter;
	private string vStatus;
	public Material vOriginalMaterial;
	public Material vSourceMaterial;
	private Material vMaterialClone;
	private Scr_Mod_Magazine cMMSource;

	public GameObject vOriginalModel;
	private Renderer cRdr;
	public Texture vText;
	// Use this for initialization
	void Start () {
		//cRdr = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		vMeter += Time.deltaTime;
		if (vMaterialClone != null){
		if (vMeter < vMaxMeter){
				vMaterialClone.SetFloat("_DissolveAmount",1f-(vMeter/vMaxMeter));
			//vSourceMaterial.SetFloat("_node_3736",vMeter);
			}
		else{vMeter = vMaxMeter;
				vMaterialClone.SetFloat("_DissolveAmount",1f-(vMeter/vMaxMeter));
			//vSourceMaterial.SetFloat("_node_3736",vMeter);
			cMMSource.fReload();
			cRdr.material = vOriginalMaterial;
			Destroy(vMaterialClone);
			Destroy(this);
			}
		}
	}
	public void fStartReloading(GameObject tSource, float tTimeTillCompletion,Scr_Mod_Magazine tMM, Scr_GameEngine cGE,Texture tTxt){
		vOriginalModel = tSource;
		cMMSource = tMM;
		vOriginalMaterial = tMM.vMagazineToPop.GetComponent<Renderer>().material;
		cRdr = tMM.vMagazineToPop.GetComponent<Renderer>(); 
		vMaxMeter = tTimeTillCompletion;
		vSourceMaterial = cGE.gMat_Fabricate;
		//vMaterialClone = new Material(Scr_GameEngine.gMat_Fabricate);
		vMaterialClone = new Material(vSourceMaterial);
		cRdr.material = vMaterialClone;
		//vText = cRdr.material.mainTexture;
		vMaterialClone.SetTexture ("_BaseTexture",tTxt);
		vMaterialClone.SetFloat("_DissolveAmount",1f-(vMeter/vMaxMeter));
		//	vSourceMaterial.SetFloat("_node_3736",vMeter);
	}

	void OnDestroy(){
		if (vMaterialClone != null)
			Destroy(vMaterialClone);
	}
}
