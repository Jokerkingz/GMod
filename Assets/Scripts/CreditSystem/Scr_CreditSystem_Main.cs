using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CreditSystem_Main : MonoBehaviour {
    public string vStatus;
    public Scr_System_SourceList cSSL;

    public string vMainCategory = "Barrel";
    // Print Data
    private string[] vCategoryList = new string[] { "Handle", "Base", "Barrel", "Magazine", "Module", "Accessory" };//,"Extension","Sword","Shield"};
    public int vModTypIndex;
    private string[] vHandleList = new string[] { "Handle_Simple_A" };
    public int vHandleTypIndex;
    private string[] vBaseType = new string[] { "Base_Simple_A", "Base_Cylinder_A", "Base_Magazine_A", "Base_Battery_A" };
    public int vBaseTypIndex;
    private string[] vBarrelType = new string[] { "Barrel_Simple_A", "Barrel_Plasma_A", "Barrel_Rifle_A", "Barrel_Curve_A", "Barrel_Rail_A" };
    public int vBarrelTypIndex;
    private string[] vMagazineType = new string[] { "Magazine_Simple_A", "Magazine_Pellet_A" };
    public int vMagazineTypIndex;
    private string[] vModuleType = new string[] { "Module_Rotator_A" };
    public int vModuleTypIndex;
    private string[] vAccessoryType = new string[] { "Accessory_Scope_A"};
    public int vAccessoryTypIndex;
    private string[] vEmpty = new string[] { "" };
    public string vSubType = "A"; //"B"
    public float vCoolDown;

    [Header("Part Holder Settings")]
    public GameObject vPartHolderPrefab;
    public float vDistanceBetween = .3f;

    public GameObject[] vDisplayList;
    public bool vIsCreative = true;

    [System.Serializable]
    public class Parts
       {
        public string vSourceName;
        public GameObject vSourceObj;
        public int vCreditCost;
        public int vBoughtItems;
        }
    public Parts[] vPartList;


    // Use this for initialization
    private void Reset()
    {
        cSSL = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scr_System_SourceList>();
        int tIndex = 0;
        vPartList = new Parts[cSSL.PartList.Length];
        foreach (Scr_System_SourceList.Parts tPart in cSSL.PartList)
        {//if (tPart.vName == "")
           //     Debug.Log(tPart.vName);
            //Debug.Log(tPart.vSource);
            //Debug.Log(vPartList[0].vSourceName);
            vPartList[tIndex] = new Parts();
            vPartList[tIndex].vSourceName = tPart.vName;
            vPartList[tIndex].vSourceObj = tPart.vSource;
            vPartList[tIndex].vCreditCost = tPart.vCreditCost;
            vPartList[tIndex].vBoughtItems = 1;
            tIndex += 1;
        }
    }
    void Start () {
        RefillChoice();
        Reposition();


    }
    void Update()
    {
        Reposition();
    }

    void Reposition()
    {
        int tCount = vDisplayList.Length;
        float tOffSet = 0f;
        int tSign = 1;
        Vector3 tSpot = this.transform.position;
        for (int i = 0; i < tCount; i++)
        {
            tSpot.x += tOffSet* tSign;
            vDisplayList[i].transform.position = tSpot;
            tOffSet += vDistanceBetween;
            tSign *= -1;
        }
    }
	// Update is called once per frame
	void RefillChoice () {
		switch (vMainCategory)
        {   case "Handle":
                SetGuns(vHandleList);
                break;
            case "Base":
                SetGuns(vBaseType);
                break;
            case "Barrel":
                SetGuns(vBarrelType);
                break;
            case "Magazine":
                SetGuns(vMagazineType);
                break;
            case "Module":
                SetGuns(vModuleType);
                break;
            case "Accessory":
                SetGuns(vAccessoryType);
                break;
            case "Empty":
                SetGuns(vEmpty);
                break;
        }
	}
    void SetGuns(string[] tSourceArray)
    {
        for (int i = 0; i < vDisplayList.Length; i++)
        {
            if (vDisplayList[i] != null)
                Destroy(vDisplayList[i]);
        } 

        GameObject tTemp;
        int tIndex = 0;
        Scr_CreditSystem_PartHolder cCSPH;
        vDisplayList = new GameObject[tSourceArray.Length];
        foreach (string tSource in tSourceArray)
        {
            tTemp = Instantiate(vPartHolderPrefab);
            cCSPH = tTemp.GetComponent<Scr_CreditSystem_PartHolder>();
            Parts tParts = CheckGun(tSource);
            cCSPH.cCSM = this;
            cCSPH.vPartToCheck = tSource;
            cCSPH.vPartObject = tParts.vSourceObj;
            cCSPH.vPartsThatShouldExists = 1;
            cCSPH.CheckPart();
            vDisplayList[tIndex] = tTemp;
            tIndex += 1;
        }
    }
    Parts CheckGun(string tName)
    {
        //GameObject tSource = null;
        Parts tNull = new Parts();
        for (int i = 0; i < vPartList.Length; i++)
        {
            if (vPartList[i].vSourceName == tName)
                return vPartList[i];
        }
        Debug.Log("fCheckGun Not found");
        return tNull;
    }
    public void ChangeHandle()
    {if (vMainCategory != "Handle")
        {
            vMainCategory = "Handle";
            RefillChoice();
            Reposition();
        }
    }
    public void ChangeBase()
    {if (vMainCategory != "Base")
        {
            vMainCategory = "Base";
            RefillChoice();
            Reposition();
        }
    }
    public void ChangeBarrel()
    {
        if (vMainCategory != "Barrel")
        {
            vMainCategory = "Barrel";
            RefillChoice();
            Reposition();
        }
    }
    public void ChangeMagazine()
    {
        if (vMainCategory != "Magazine")
        {
            vMainCategory = "Magazine";
            RefillChoice();
            Reposition();
        }
    }
    public void ChangeModule()
    {
        if (vMainCategory != "Module")
        {
            vMainCategory = "Module";
            RefillChoice();
            Reposition();
        }
    }
    public void ChangeAccessory()
    {
        if (vMainCategory != "Accessory")
        {
            vMainCategory = "Accessory";
            RefillChoice();
            Reposition();
        }
    }
    public void ChangeEmpty()
    {
        if (vMainCategory != "Empty")
        {
            vMainCategory = "Empty";
            RefillChoice();
            Reposition();
        }
    }
}
