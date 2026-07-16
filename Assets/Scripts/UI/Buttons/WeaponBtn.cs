using UnityEngine;

public class WeaponBtn : MonoBehaviour
{
    public Weapon weapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject details;
    private Canvas canvas;


    public void Start()
    {
        details = GameObject.Find("WeaponDetails");
        canvas = details.GetComponent<Canvas>();

    }

    public void setWeapon(Weapon giveWeapon)
    {
        weapon = giveWeapon;
    }

    public void CurrentItem()
    {
        States.itemName = weapon.name;
        States.itemDesc = weapon.desc;
        States.itemIcon = weapon.icon;
        States.wepAtk = weapon.attack;
        States.armCurUse = (int)weapon.used;
        States.armDur = weapon.durability;

        canvas.enabled = true;
    }

}
