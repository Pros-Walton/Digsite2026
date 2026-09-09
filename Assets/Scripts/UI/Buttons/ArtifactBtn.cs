using UnityEngine;

public class ArtifactBtn : MonoBehaviour
{
    public Artifact artifact;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject details;
    private Canvas canvas;
    private Canvas readOut;


    public void Start()
    {
        details = GameObject.Find("ArtifactDetails");
        canvas = details.GetComponent<Canvas>();
        readOut = GameObject.Find("Inventory Readout").GetComponent<Canvas>();

    }

    public void setArtifact(Artifact giveArtifact)
    {
        artifact = giveArtifact;
    }

    public void CurrentItem()
    {
        States.itemName = artifact.name;
        States.itemDesc = artifact.desc;
        States.itemIcon = artifact.icon;
        States.artLore = artifact.lore;
        States.artCount = artifact.count;

        readOut.enabled = true;
        canvas.enabled = true;
    }

}
