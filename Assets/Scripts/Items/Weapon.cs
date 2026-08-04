using UnityEngine;

public class Weapon
{
    public string name;
    public int attack;
    public int durability;
    public float used;
    public float damageRate;
    public string desc;
    public Sprite icon;
    public enum weaponType {meele, ranged};
    public weaponType type;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void use()
    {
        used += damageRate;
    }

    public bool shouldBreak()
    {
        return used >= durability;
    }

    public Weapon(int itemID)
    {
        switch(itemID)
        {
            case 0:
                name = "Trowel";
                attack = 1;
                durability = 0;
                used = -1.0f;
                damageRate = 0.0f;
                desc = "Trusty old  trowel, used for digging holes. Typically.";
                icon = Resources.Load<Sprite>("Textures/Items/wep_trowel") as Sprite;
                type = weaponType.meele;
                break;
            case 1:
                name = "Knife";
                attack = 3;
                durability = 64;
                used = 0.0f;
                damageRate = 1.0f;
                desc = "Stabby stabby!";
                icon = Resources.Load<Sprite>("Textures/Items/wep_knife") as Sprite;
                type = weaponType.meele;
                break;
            case 2:
                name = "Bow";
                attack = 1;
                durability = 0;
                used = -1.0f;
                damageRate = 0.0f;
                desc = "A basic bow, good for picking off enemies at a distance.";
                icon = Resources.Load<Sprite>("Textures/Items/wep_bow") as Sprite;
                type = weaponType.ranged;
                break;
        }

    }
}

