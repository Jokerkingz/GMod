using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CreditSystem_PartHolder : MonoBehaviour {
    public Scr_CreditSystem_Main cCSM;
    public string vPartToCheck;
    public GameObject vPartObject;
    public int vPartsThatShouldExists;
    public GameObject vHeldPart;
    public Scr_GrabSystem_Item cGSG;

    public void CheckPart()
    {
        GameObject[] tObj = GameObject.FindGameObjectsWithTag("SocketMale");
        int tIndex = 0;
        vHeldPart = null;
        foreach (GameObject tPart in tObj)
        {
            Scr_ModSaverPart tMSP = tPart.GetComponent<Scr_ModSaverPart>();
            if (tMSP.vCreation == "CreditSystem" && tMSP.vPartType == vPartToCheck)
                tIndex++;

        }
        if (cCSM.vIsCreative)
        {

            vHeldPart = Instantiate(vPartObject);
            Scr_ModSaverPart tMSP = vHeldPart.GetComponent<Scr_ModSaverPart>();
            cGSG = tMSP.cGrabSystItem;
            tMSP.vCreation = "CreativeSystem";
            Rigidbody tRB = vHeldPart.GetComponent<Rigidbody>();
            tRB.isKinematic = true;
            tRB.useGravity = false;
            vHeldPart.AddComponent<Scr_Destroy_OnY>();
        }
        else if (tIndex < vPartsThatShouldExists)
        {
            vHeldPart = Instantiate(vPartObject);
            Scr_ModSaverPart tMSP = vHeldPart.GetComponent<Scr_ModSaverPart>();
            cGSG = tMSP.cGrabSystItem;
            tMSP.vCreation = "CreditSystem";
            Rigidbody tRB = vHeldPart.GetComponent<Rigidbody>();
            tRB.isKinematic = true;
            tRB.useGravity = false;
            vHeldPart.AddComponent<Scr_Destroy_OnY>();
        }
    }
    void Update()
    {
        if (vHeldPart != null)
        {   if (!cGSG.vIsGripped)
                vHeldPart.transform.position = this.transform.position;
            else
                CheckPart();
        }
        else
            CheckPart();
    }
    private void OnDestroy()
    {
        if (vHeldPart != null)
            if (!cGSG.vIsGripped)
                Destroy(vHeldPart);
    }
}
