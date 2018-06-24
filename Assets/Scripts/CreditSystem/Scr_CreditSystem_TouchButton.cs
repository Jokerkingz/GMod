using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CreditSystem_TouchButton : MonoBehaviour {

    public Scr_CreditSystem_Main vCreditSource;
    public string vMessageToSend;
    

    void OnTriggerEnter(Collider tOther)
    {
        if (tOther.tag == "FingerTip")
        {
            if (tOther.GetComponent<Scr_TouchTip>().vPointing)
                vCreditSource.gameObject.SendMessage(vMessageToSend, SendMessageOptions.DontRequireReceiver);
        }
    }
}
