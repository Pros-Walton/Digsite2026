using UnityEngine;

public class KeyItems
{
    public string name;
    public string desc;
    public Sprite icon;


    public KeyItems(int itemID)
    {
        switch(itemID)
        {
            case 0:
                name = "Key 0";
                desc = "Have you ever heard about Five Nights at Freddy's 3?";
                icon = Resources.Load<Sprite>("Textures/Items/key_0") as Sprite;
                break;
        }

    }
}