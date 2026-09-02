using UnityEngine;

public class Item
{
    public string name;
    public int itemId;
    public string desc;
    public string affect;
    public int count;
    public Sprite icon;


    public Item(int itemID)
    {
        string dir = "Textures/Items/item/";
        itemId = itemID;
        switch(itemId)
        {
            case 0:
                name = "Healing potion";
                desc = "A green potion of some kind. It appears to be health related.";
                affect = "+ 10HP";
                count = 1;
                icon = Resources.Load<Sprite>(dir + "heal_potion01") as Sprite;
                break;
            case 1:
                name = "Bread loaf";
                desc = "A basic loaf of Bread. Its surface is crunchy.";
                affect = "+ 10 Stamina";
                count = 1;
                icon = Resources.Load<Sprite>(dir + "bread") as Sprite;
                break;
        }

    }

    public void use()
    {
        switch(itemId)
        {
            case 0:
                States.stats.health = Mathf.Min((States.stats.health + 10), States.stats.health_max);
                break;
            case 1:
                States.stats.stamina = Mathf.Min((States.stats.stamina + 10), States.stats.stamina_max);
                break;

        }
    }
}