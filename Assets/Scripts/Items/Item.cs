using UnityEngine;

public class Item
{
    public string name;
    public string desc;
    public int count;
    public Sprite icon;


    public Item(int itemID)
    {
        switch(itemID)
        {
            case 0:
                name = "Item 0";
                desc = "Have you ever heard about Five Nights at Freddy's 2?";
                count = 1;
                icon = Resources.Load<Sprite>("Textures/Items/item_0") as Sprite;
                break;
        }

    }
}