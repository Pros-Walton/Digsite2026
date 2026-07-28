using UnityEngine;
using UnityEngine.UI;

public class WeaponBtn : MonoBehaviour
{
    public Weapon weapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject details;
    private Canvas canvas;
    private Button button;

    private int pointer;

    private Manager manager;

    private UISounds audio;


    public void Start()
    {
        details = GameObject.Find("WeaponDetails");
        canvas = details.GetComponent<Canvas>();
        button = GameObject.Find("WeaponDetailButton").GetComponent<Button>();
        manager = GameObject.Find("EventSystem").GetComponent<Manager>();
        audio = GameObject.Find("UI Sounds").GetComponent<UISounds>();

    }

    public void setWeapon(Weapon giveWeapon, int point)
    {
        weapon = giveWeapon;
        pointer = point;
    }

    public void CurrentItem()
    {
        button.onClick.RemoveAllListeners();
        setDetails();
        audio.click();
        button.onClick.AddListener(equipWeapon);
        button.onClick.AddListener(audio.click);

        canvas.enabled = true;
    }

    public void equipWeapon()
    {
        Weapon current = States.stats.weapon;
        States.stats.weapon = weapon;
        weapon = current;
        States.inventory.weapons[pointer] = weapon;
        setDetails();
        manager.clearButtons();
        manager.doWeapon();
    }

    private void setDetails()
    {
        States.itemName = weapon.name;
        States.itemDesc = weapon.desc;
        States.itemIcon = weapon.icon;
        States.wepAtk = weapon.attack;
        States.wepCurUse = (int)weapon.used;
        States.wepDur = weapon.durability;
    }

}
