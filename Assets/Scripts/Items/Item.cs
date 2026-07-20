using UnityEngine;

public class Item
{
    public string name;
    private int id;
    public string desc;
    public string affect;
    public int count;
    public Sprite icon;


    public Item(int itemID)
    {
        id = itemID;
        switch(id)
        {
            case 0:
                name = "Item 0";
                desc = "Have you ever heard about Five Nights at Freddy's 2?";
                affect = "+ 10HP";
                count = 1;
                icon = Resources.Load<Sprite>("Textures/Items/item_0") as Sprite;
                break;
            case 1:
                name = "Bread";
                desc = "Basic Bread";
                affect = "+ 10 Stamina";
                count = 1;
                icon = Resources.Load<Sprite>("Textures/Items/item_bread") as Sprite;
                break;
        }

    }

    public void use()
    {
        switch(id)
        {
            case 0:
                States.stats.health = Mathf.Min((States.stats.health + 10), States.stats.health_max);
                break;
            case 1:
                States.stats.stamina = Mathf.Min((States.stats.stamina + 10), States.stats.stamina_max);
                break;

        }
        count -= 1;
        States.itemCount = count;
    }
}