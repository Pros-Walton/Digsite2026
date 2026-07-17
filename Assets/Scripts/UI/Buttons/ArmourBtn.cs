using UnityEngine;
using UnityEngine.UI;

public class ArmourBtn : MonoBehaviour
{
    public Armour armour;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject details;
    private Canvas canvas;
    private Button button;
    private int pointer;

    private Manager manager;


    public void Start()
    {
        details = GameObject.Find("ArmourDetails");
        button = GameObject.Find("ArmourDetailButton").GetComponent<Button>();
        canvas = details.GetComponent<Canvas>();
        manager = GameObject.Find("EventSystem").GetComponent<Manager>();

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
        button.onClick.AddListener(equipArmour);

        canvas.enabled = true;
    }

    public void equipArmour()
    {
        PlayerStats stats = GameObject.Find("Player Temp").GetComponent<PlayerStats>();
        Armour current = stats.armour;
        stats.armour = armour;
        armour = current;
        States.inventory.armours[pointer] = armour;
        setDetails();
        manager.clearButtons();
        manager.doArmour();
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

}
