using UnityEngine;

public class Weapon
{
    public string name;
    public int attack;
    public int durability;
    public float used;
    public float damageRate;
    public string icon;
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
                icon = "wep_trowel";
                break;
        }

    }
}

