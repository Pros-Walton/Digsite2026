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
                Debug.Log("Try to set weapon!");
                name = "Trowel";
                attack = 1;
                durability = 0;
                used = -1.0f;
                damageRate = 0.0f;
                desc = "Have you ever heard of Five Nights at Freddy's 4?";
                icon = Resources.Load<Sprite>("Textures/Items/wep_trowel") as Sprite;
                break;
        }

    }
}

