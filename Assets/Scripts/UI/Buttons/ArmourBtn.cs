using UnityEngine;
using UnityEngine.UI;

public class ArmourBtn : MonoBehaviour
{
    public Armour armour;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject details;
    private Canvas canvas;
    private Canvas readOut;
    private Button button;
    private int pointer;

    private Manager manager;

    private UISounds audio;


    public void Start()
    {
        details = GameObject.Find("ArmourDetails");
        button = GameObject.Find("ArmourDetailButton").GetComponent<Button>();
        canvas = details.GetComponent<Canvas>();
        readOut = GameObject.Find("Inventory Readout").GetComponent<Canvas>();
        manager = GameObject.Find("EventSystem").GetComponent<Manager>();
        audio = GameObject.Find("UI Sounds").GetComponent<UISounds>();

    }

    public void setArmour(Armour giveArmour, int point)
    {
        armour = giveArmour;
        pointer = point;
    }

    public void CurrentItem()
    {
        button.onClick.RemoveAllListeners();
        setDetails();
        audio.click();
        button.onClick.AddListener(equipArmour);
        button.onClick.AddListener(audio.click);

        readOut.enabled = true;
        canvas.enabled = true;
    }

    public void equipArmour()
    {
        Armour current = States.stats.armour;
        States.stats.armour = armour;
        armour = current;
        States.inventory.armours[pointer] = armour;
        setDetails();
        manager.clearButtons();
        manager.doArmour();
        readOut.enabled = false;
    }

    private void setDetails()
    {
        States.itemName = armour.name;
        States.itemDesc = armour.desc;
        States.itemIcon = armour.icon;
        States.armDef = armour.defense;
        States.armCurUse = (int)armour.used;
        States.armDur = armour.durability;
    }

    public void discard()
    {
            manager.clearButtons();
            manager.doArmour();
            States.itemName = null;
            States.itemDesc = null;
            States.itemIcon = null;
            readOut.enabled = false;
            canvas.enabled = false;
    }

}
