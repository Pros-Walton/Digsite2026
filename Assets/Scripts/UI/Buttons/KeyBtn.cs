using UnityEngine;

public class KeyBtn : MonoBehaviour
{
    public Key key;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject details;
    private Canvas canvas;


    public void Start()
    {
        details = GameObject.Find("KeyDetails");
        canvas = details.GetComponent<Canvas>();

    }

    public void setKey(Key giveKey)
    {
        key = giveKey;
    }

    public void CurrentItem()
    {
        States.itemName = key.name;
        States.itemDesc = key.desc;
        States.itemIcon = key.icon;

        canvas.enabled = true;
    }

    public void use()
    {
        key.use();
    }

}
