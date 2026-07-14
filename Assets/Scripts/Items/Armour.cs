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
        switch(itemID)
        {
            case 0:
                name = "Cloth";
                defense = 1;
                durability = 0;
                used = -1.0f;
                damageRate = 0.0f;
                desc = "Have you ever heard of Five Nights at Freddy's Sister Location?";
                icon = Resources.Load<Sprite>("Textures/Items/amr_cloth") as Sprite;
                break;
        }

    }
}
