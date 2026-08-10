using UnityEngine;

public class Armour
{
    public string name;
    public int defense;
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

    public Armour(int itemID)
    {
        string dir = "Textures/Items/amr/";
        switch(itemID)
        {
            case 0:
                name = "Cloth";
                defense = 1;
                durability = 0;
                used = -1.0f;
                damageRate = 0.0f;
                desc = "Regular, every day work clothes.";
                icon = Resources.Load<Sprite>(dir + "cloth") as Sprite;
                break;
            case 1:
                name = "Chain";
                defense = 2;
                durability = 64;
                used = 0.0f;
                damageRate = 1.0f;
                desc = "Chain armour. Historically focused on preventing slashing attacks.";
                icon = Resources.Load<Sprite>(dir + "chain") as Sprite;
                break;
        }

    }
}
