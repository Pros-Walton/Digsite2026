using UnityEngine;
using UnityEngine.UI;


public class ItemBtn : MonoBehaviour
{
    public Item item;
    private Button btn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject details;
    private Canvas canvas;


    public void Start()
    {
        GameObject btnObject = GameObject.Find("ItemDetailButton");
        btn = btnObject.GetComponent<Button>();
        details = GameObject.Find("ItemDetails");
        canvas = details.GetComponent<Canvas>();
        

    }

    public void setItem(Item giveItem)
    {
        item = giveItem;
    }

    public void CurrentItem()
    {
        btn.onClick.RemoveAllListeners();
        States.itemName = item.name;
        States.itemDesc = item.desc;
        States.itemIcon = item.icon;
        States.itemAffect = item.affect;
        States.itemCount = item.count;
        btn.onClick.AddListener(this.use);

        canvas.enabled = true;
    }

    public void use()
    {
        item.use();
    }

}
