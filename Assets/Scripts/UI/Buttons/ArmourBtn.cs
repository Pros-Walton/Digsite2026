using UnityEngine;

public class ArmourBtn : MonoBehaviour
{
    public Armour armour;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject details;
    private Canvas canvas;


    public void Start()
    {
        details = GameObject.Find("ArmourDetails");
        canvas = details.GetComponent<Canvas>();

    }

    public void setArmour(Armour giveArmour)
    {
        armour = giveArmour;
    }

    public void CurrentItem()
    {
        States.itemName = armour.name;
        States.itemDesc = armour.desc;
        States.itemIcon = armour.icon;
        States.armDef = armour.defense;
        States.armCurUse = (int)armour.used;
        States.armDur = armour.durability;

        canvas.enabled = true;
    }

}
