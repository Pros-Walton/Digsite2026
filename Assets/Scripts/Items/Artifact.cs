using UnityEngine;

public class Artifact
{
    public string name;
    public string desc;
    public int count;
    public int score;
    public string lore;
    public Sprite icon;


    public Artifact(int itemID)
    {
        string dir = "Textures/Items/art/";
        switch(itemID)
        {
            case 0:
                name = "Pottery shard";
                desc = "A shard of a piece of pottery. It's surface implies a wide and shallow shape.";
                lore = "It seems it was part of a bowel, likely used for eating.";
                score = 100;
                count = 1;
                icon = Resources.Load<Sprite>(dir + "shard") as Sprite;
                break;
            case 1:
                name = "Strange coin";
                desc = "A strange, metallic, branded coin of some kind.";
                lore = "For some reason, you are reminded of a puzzle.";
                score = 150;
                count = 1;
                icon = Resources.Load<Sprite>(dir + "hintCoin") as Sprite;
                break;
        }

    }
}

