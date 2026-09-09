using UnityEngine;
using UnityEngine.UI;


public class ItemBtn : MonoBehaviour
{
    public Item item;
    private Button btn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject details;
    private Canvas canvas;
    private Canvas readOut;

    private Manager manager;

    private int pointer;

    private UISounds audio;


    public void Start()
    {
        GameObject btnObject = GameObject.Find("ItemDetailButton");
        btn = btnObject.GetComponent<Button>();
        details = GameObject.Find("ItemDetails");
        canvas = details.GetComponent<Canvas>();
        readOut = GameObject.Find("Inventory Readout").GetComponent<Canvas>();
        manager = GameObject.Find("EventSystem").GetComponent<Manager>();
        audio = GameObject.Find("UI Sounds").GetComponent<UISounds>();
        

    }

    public void setItem(Item giveItem, int point)
    {
        item = giveItem;
        pointer = point;
    }

    public void CurrentItem()
    {
        btn.onClick.RemoveAllListeners();
        States.itemName = item.name;
        States.itemDesc = item.desc;
        States.itemIcon = item.icon;
        States.itemAffect = item.affect;
        States.itemCount = item.count;
        States.itemPoint = pointer;
        btn.onClick.AddListener(this.use);
        btn.onClick.AddListener(audio.click);
        audio.click();
        Debug.Log("ITEM POINTER: " + States.itemPoint);
        readOut.enabled = true;
        canvas.enabled = true;
    }

    public void use()
    {
        Debug.Log("ITEM COUNT BEFORE: " + States.itemCount);
        States.itemCount--;
        States.inventory.items[States.itemPoint].count = States.itemCount;
        int counter = States.itemCount;
        States.inventory.items[States.itemPoint].use();
        if (counter <= 0)
        {
            States.itemName = null;
            States.itemDesc = null;
            States.itemIcon = null;
            readOut.enabled = false;
            canvas.enabled = false;
            States.inventory.items.RemoveAt(pointer);
            manager.clearButtons();
            manager.doItem();
        }
        Debug.Log("ITEM COUNT AFTER: " + States.itemCount);  
    }

    public void hide()
    {

        if (States.hideCanvas == true)
        {
            readOut.enabled = false;
            canvas.enabled = false;
            States.hideCanvas = false;
        }
    }

}
