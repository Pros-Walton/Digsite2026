using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ReadoutPanel : MonoBehaviour
{

    public TMP_Text itemName;
    public TMP_Text desc;
    public Image image;

    public TMP_Text armDef;
    public TMP_Text armDur;

    public TMP_Text wepDef;
    public TMP_Text wepDur;

    public TMP_Text itemAffect;
    public TMP_Text itemCount;

    public TMP_Text artLore;
    public TMP_Text artCount;

    private GameObject armourDetails;
    private Canvas armourCanvas;
    private GameObject weaponDetails;
    private Canvas weaponCanvas;
    private GameObject itemDetails;
    private Canvas itemCanvas;
    private GameObject keyDetails;
    private Canvas keyCanvas;
    private GameObject artifactDetails;
    private Canvas artifactCanvas;
    private Canvas readOut;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        armourDetails = GameObject.Find("ArmourDetails");
        armourCanvas = armourDetails.GetComponent<Canvas>();
        weaponDetails = GameObject.Find("WeaponDetails");
        weaponCanvas = weaponDetails.GetComponent<Canvas>();
        itemDetails = GameObject.Find("ItemDetails");
        itemCanvas = itemDetails.GetComponent<Canvas>();
        keyDetails = GameObject.Find("KeyDetails");
        keyCanvas = keyDetails.GetComponent<Canvas>();
        artifactDetails = GameObject.Find("ArtifactDetails");
        artifactCanvas = artifactDetails.GetComponent<Canvas>();
        readOut = GameObject.Find("Inventory Readout").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        itemName.text = States.itemName;
        desc.text = States.itemDesc;
        image.sprite = States.itemIcon;

        armDef.text = ("DEFENSE: " + States.armDef);
        if (States.armCurUse == -1)
        {
            armDur.text = ("DURABILITY: INF");
        }
        else
        {
            armDur.text = ("DURABILITY: " + (States.armDur - States.armCurUse) + "/" + States.armDur);
        }
        
        wepDef.text = ("ATTACK: " + States.wepAtk);
        if (States.wepCurUse == -1)
        {
            wepDur.text = ("DURABILITY: INF");
        }
        else
        {
            wepDur.text = ("DURABILITY: " + (States.wepDur - States.wepCurUse) + "/" + States.wepDur);
        }

        itemAffect.text = States.itemAffect;
        itemCount.text = ("COUNT: " + States.itemCount);

        artLore.text = States.artLore;
        artCount.text = ("COUNT: " + States.artCount);
    }

    public void clearDetails()
    { 
        States.itemName = null;
        States.itemDesc = null;
        States.itemIcon = null;

        armourCanvas.enabled = false;
        weaponCanvas.enabled = false;
        itemCanvas.enabled = false;
        keyCanvas.enabled = false;
        artifactCanvas.enabled = false;
        readOut.enabled = false;
    }
}
