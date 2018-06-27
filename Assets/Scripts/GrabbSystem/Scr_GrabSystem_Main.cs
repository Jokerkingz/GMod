using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GrabSystem_Main : MonoBehaviour
{
    public Transform vSourcePoint;
    public GameObject vGrabbedItem;
    public List<GameObject> lPossibleGrabbables;
    public GameObject vClosestItem;
    public bool vIsTheRightHand;
    public string vGripState = "Idle";
    public Rigidbody vItemRB;


    public float vPrevGripRelease; // Makes sure that gripping is fine;
    // Use this for initialization
    //Label Information
    public Scr_Give_Label cGL; 
	public Scr_Give_Label cOtherGL;
	public GameObject vLabelTarget;
    public Scr_ModHandle cMH; // for shooting purposes
    public int vCount;
    public Scr_PointToMove cPTM;


    public bool vShouldTeleport;
    [Header("ParticleSystem")]
    public Scr_GrabSystem_Main cOtherGrabber;
    public Scr_Guide_Particle cGP;
	public Scr_Guide_Particle cOtherGP;

    
    public float vCD;
    void Update()
    {

        if (lPossibleGrabbables.Count > 0)
        {
            GameObject tEmpty = null;
            bool tCall = false;
            GameObject tClosest = null;
            float tClosestDistance = 100f;
            float tItemDistance = 100f;
            Vector3 vSelf = vSourcePoint.position;
            foreach (GameObject tItem in lPossibleGrabbables)
            {
                if (tItem != null)
                {
                    if (tClosest == null)
                    {
                        tClosest = tItem;
                        tClosestDistance = Vector3.Distance(tItem.transform.position, vSelf);
                    }
                    else
                    {
                        tItemDistance = Vector3.Distance(tItem.transform.position, vSelf);
                        if (tItemDistance < tClosestDistance)
                        {
                            tClosestDistance = tItemDistance;
                            tClosest = tItem;
                        }

                    }
                }
                else
                    { 
                    tEmpty = tItem;
                    tCall = true;
                    }
            }
            if (tCall)
                lPossibleGrabbables.Remove(tEmpty);
            //vClosestItem = tClosest;
            //* Temporary fix until 
            Scr_ModSaverPart tMSP = tClosest.transform.root.GetComponent<Scr_ModSaverPart>();
            if (tMSP != null)
            {
                if (tMSP.cGrabSystItem.vIsGripped)
                    vClosestItem = tClosest;
                else
                    vClosestItem = tClosest.transform.root.GetComponent<Scr_ModSaverPart>().cGrabSystItem.gameObject;
            }
            else
                vClosestItem = tClosest.transform.root.GetComponent<Scr_ModSaverPart>().cGrabSystItem.gameObject;
            //     Give part that has Grabsystem Item;

        }
        else vClosestItem = null;

        // Show label about the thing //
        if (vClosestItem != null)
        {
            vClosestItem = vClosestItem.GetComponent<Scr_GrabSystem_Item>().vMainObject;
            Scr_FabricationCollective tFabCheck  = vClosestItem.GetComponentInChildren<Scr_FabricationCollective>();
            if (tFabCheck != null)
            {
                vClosestItem = null;
                return;
            }
                
            if (vGripState == "Idle")
            {

                vLabelTarget = vClosestItem;
                cGL.fShowLabel(vClosestItem);

            }
            else
            {
                vLabelTarget = null;
                cGL.fShowLabel(null);
            }
        }
        else
        {
            vLabelTarget = null;
            cGL.fShowLabel(null);
        }

        // End showing the label about the thing //

        // Particle System // Particle guidance activation system
        /*
        if (vGrabbedItem != null && cOtherGrabber.vGrabbedItem != null)
        {
            cGP.fGiveMaleParticles(vGrabbedItem.gameObject);
            cOtherGP.fGiveMaleParticles(cOtherGrabber.vGrabbedItem.gameObject);
            Debug.Log("Sending Particles");
        }
        */
        // Start Shooting stuff
        if (cMH != null)
            { 
            if ((vIsTheRightHand && Input.GetAxis("OGVR_RIndexTrigger") > 0.5f) || (!vIsTheRightHand && Input.GetAxis("OGVR_LIndexTrigger") > 0.5f))
                {
                cMH.fTriggerPressed();


                }
            }
        }


    private void FixedUpdate()
    {
        float tGripButton;
            if(vIsTheRightHand)
            tGripButton = Input.GetAxis("OGVR_RGrip");
        else
            tGripButton = Input.GetAxis("OGVR_LGrip");
        switch (vGripState)
        {
            case "Idle":
                if (vClosestItem != null)
                {
                    if (tGripButton > 0.4f)
                    {
                        vGripState = "Lerp";
                        vGrabbedItem = vClosestItem;
                        vItemRB = vClosestItem.GetComponent<Scr_ModSaverPart>().cRB;
                        vItemRB.isKinematic = true;
                        vItemRB.useGravity = false;
                        vClosestItem.GetComponent<Scr_ModSaverPart>().cGrabSystItem.fGrabBegin(this);
                        cMH = vClosestItem.GetComponent<Scr_ModHandle>();
                        // Particle System // Particle guidance activation system
        
                        if (vGrabbedItem != null && cOtherGrabber.vGrabbedItem != null)
                        {
                            cGP.fGiveMaleParticles(vGrabbedItem.gameObject);
                            cOtherGP.fGiveMaleParticles(cOtherGrabber.vGrabbedItem.gameObject);
                            Debug.Log("Sending Particles");
                        }
                    }
                }
                vPrevGripRelease = 0;
                break;

            case "Lerp":
                if (vItemRB == null)
                {
                    vGrabbedItem = null;
                    vGripState = "Idle";
                    break;

                }
                
                vItemRB.isKinematic = true;
                vItemRB.useGravity = false;
                Vector3 tPoint = Vector3.zero;
                Vector3 tAngle = Vector3.zero;
                Vector3 tPosOffset = Vector3.zero;
                Quaternion tAngOffset = transform.rotation;
                Vector3 tPosHand;
                //Vector3 tRotation;
                Scr_GrabSystem_Item tTemp = vItemRB.GetComponentInChildren<Scr_GrabSystem_Item>();
                if (tTemp != null)
                {
                    tPosOffset = tTemp.vTransformAdjustment.localPosition*.1f;
                    tAngOffset = tTemp.vTransformAdjustment.localRotation;
                }

                tPosHand = vSourcePoint.position;


                Quaternion tQuad = vSourcePoint.rotation * tAngOffset;
                float tDistance = Vector3.Distance(tPosHand - transform.TransformDirection(tPosOffset), vItemRB.position );
                Quaternion tAngleDif = Quaternion.RotateTowards(vItemRB.transform.rotation, tQuad, 25f);

                if (tDistance > .1f && !vShouldTeleport)
                {
                    tPoint = vItemRB.position + Vector3.Normalize(tPosHand - vItemRB.position - transform.TransformDirection(tPosOffset)) * .1f;
                }
                else
                {
                    tPoint = tPosHand - transform.TransformDirection(tPosOffset);
                    vShouldTeleport = false;
                }
                vItemRB.MovePosition(tPoint);
                vItemRB.MoveRotation(tAngleDif);

                if (tGripButton < 0.4f)
                {
                    vGrabbedItem = null;
                    vGripState = "Idle";
                    vItemRB.isKinematic = false;
                    vItemRB.useGravity = true;
                    vItemRB.GetComponentInChildren<Scr_GrabSystem_Item>().fGrabEnd();
                    cMH = null;
                    cGP.fDestroyParticles();
                    cOtherGP.fDestroyParticles();
                }
                //vPrevGripRelease = Input.GetAxis("OGVR_RGrip");


                /*
                if (vCD > 0)
                    vCD -= 1;
                if (Input.GetAxis("OGVR_RIndexTrigger") > 0.5f && vCD <= 0)
                {
                    vCD += 3;
                    Debug.Log("recoil");
                    fRecoil(vItemRB.transform.forward, -.5f);
                }
                */

                break;


            case "LetGo":
                

                break;
        }
    }
    void OnTriggerEnter(Collider tOther)
    {Scr_GrabSystem_Item tGSI = tOther.GetComponent<Scr_GrabSystem_Item>();
        if (tGSI == null)
            return;
     if (tGSI.enabled)
        {if (!lPossibleGrabbables.Contains(tOther.gameObject))
                lPossibleGrabbables.Add(tOther.gameObject);        
        }
    }
    void OnTriggerExit(Collider tOther)
    {if(lPossibleGrabbables.Contains(tOther.gameObject))
        lPossibleGrabbables.Remove(tOther.gameObject);

    }
    public void fRecoil(Vector3 tPushbackDireciton, float Power)
    {
        Vector3 tAngle = Vector3.zero;
        tAngle = vItemRB.transform.eulerAngles;
        tAngle.x += Random.Range(-1f, 1f);
        tAngle.y += Random.Range(-1f, 1f);
        tAngle.z += Random.Range(-1f, 1f);
        vItemRB.transform.eulerAngles = tAngle;
        //vItemRB.MovePosition(transform.TransformDirection(tAngle)*1000f);
        vItemRB.transform.position = vItemRB.transform.position + (tPushbackDireciton * .01f * Power);


    }
    float fAngleFix(float tTmp)
    {
        while (tTmp < -180)
            tTmp += 360;
        while (tTmp > 180)
            tTmp -= 360;

        return tTmp;
    }
    public void fTeleportToHand()
    {
        vShouldTeleport = true;
        /*
        if (vGrabbedItem == null)
            return;
        
        Vector3 tPosHand;
        Vector3 tPosOffset = Vector3.zero;
        Quaternion tAngOffset = transform.rotation;
        Scr_GrabSystem_Item tTemp = vItemRB.GetComponentInChildren<Scr_GrabSystem_Item>();
        if (tTemp != null)
        {
            tPosOffset = tTemp.vTransformAdjustment.localPosition * .1f;
            tAngOffset = tTemp.vTransformAdjustment.localRotation;
        }
        tPosHand = vSourcePoint.position;
        Vector3 tPoint = tPosHand - transform.TransformDirection(tPosOffset);
        vItemRB.MovePosition(tPoint);
        
        vGrabbedItem.transform.position= vSourcePoint.transform.position;
        Debug.Break();
        //Debug.Log("Teleported");
        */
    }
}
