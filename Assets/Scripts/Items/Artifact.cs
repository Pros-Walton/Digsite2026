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
        switch(itemID)
        {
            case 0:
                name = "Artifact 0";
                desc = "Have you ever heard about Five Nights at Freddy's?";
                lore = "It was a favourite of mine growing up.";
                score = 100;
                count = 1;
                icon = Resources.Load<Sprite>("Textures/Items/art_0") as Sprite;
                break;
        }

    }
}

