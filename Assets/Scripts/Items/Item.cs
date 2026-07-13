using UnityEngine;

public class Item
{
    public string name;
    public string desc;
    public string icon;


    public Item(int itemID)
    {
        switch(itemID)
        {
            case 0:
                name = "Item 0";
                desc = "Have you ever heard about Five Nights at Freddy's 2?";
                icon = "item_0";
                break;
        }

    }
}