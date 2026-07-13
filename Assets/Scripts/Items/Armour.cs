using UnityEngine;

public class Armour
{
    public string name;
    public int defense;
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
                icon = "amr_cloth";
                break;
        }

    }
}
