using UnityEngine;

public class Artifact
{
    public string name;
    public string desc;
    public string icon_path;


    public Artifact(int itemID)
    {
        switch(itemID)
        {
            case 0:
                name = "Artifact 0";
                desc = "Have you ever heard about Five Nights at Freddy's?";
                break;
        }

    }
}

