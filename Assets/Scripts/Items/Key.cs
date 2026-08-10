using UnityEngine;

public class Key
{
    public string name;
    public string desc;
    private int id;
    public Sprite icon;


    public Key(int itemID)
    {
        id = itemID;
        string dir = "Textures/Items/key/";
        switch(id)
        {
            case 0:
                name = "Key 0";
                desc = "Have you ever heard about Five Nights at Freddy's 3?";
                icon = Resources.Load<Sprite>(dir + "0") as Sprite;
                break;
        }
    }

    public void use()
    {
        switch(id)
        {
            case 0:
                Debug.Log("Case 0");
                break;
        }
    }
}