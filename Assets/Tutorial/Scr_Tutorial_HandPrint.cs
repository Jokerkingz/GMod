using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Tutorial_HandPrint : MonoBehaviour {
	public string vUsedFor = "Table";
	public Scr_Tutorial_Table vTableSource;
	public Scr_Challenge vChallengeSource;
	public Scr_GameEngine cGE;
	private float vCook;
	private float vSpeed;
	private bool vDone;
	private ParticleSystem[] vList = new ParticleSystem[0];

	void Start(){
		vList = GetComponentsInChildren<ParticleSystem>();
		cGE = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scr_GameEngine>();
	}
	void Update(){
		vCook -= Time.deltaTime;
		foreach (ParticleSystem tThis in vList){
			//tThis.main.simulationSpeed = ;
			var main = tThis.main;
			main.simulationSpeed = .3f+(.5f*vCook);
			} 
		if (!vDone && vCook > 2f){
			switch (vUsedFor){
				case "Table":
					vTableSource.fHandPressed();
					vDone = true;
					Destroy(this.gameObject);
					//Debug.Log("I found the Hand");
					enabled = false;
				break;
				case "EndTutorial":
				cGE.fGotoNextRoom("Scene/Sce_CleanedRoom");
					vDone = true;
					enabled = false;

				break;
				case "BeginRaid":
					cGE.fGotoNextRoom("Dustyn/Level One - Scenes/Sce_level0000");
					vDone = true;
					enabled = false;

				break;
			case "Challenge":
					vChallengeSource.vChallengeStep = 1;
				vChallengeSource.fNextChallenge();
					vDone = true;
					Destroy(this.gameObject);
					//Debug.Log("I found the Hand");
					enabled = false;

				break;
				case "GotoShowcase":
				cGE.fGotoNextRoom("Challenge/Sce_Challenge");
					vDone = true;
					enabled = false;

				break;
				}
			}
		vCook = Mathf.Clamp(vCook,0f,3f);
	}

	void OnTriggerStay(Collider tOther){
		if (tOther.tag == "Hand"){
			vCook += 2*Time.deltaTime;
				
			}
	}
}
