using UnityEngine;
using UnityEngine.UI;


public class ItemBtn : MonoBehaviour
{
    public Item item;
    private Button btn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject details;
    private Canvas canvas;

    private Manager manager;

    private int pointer;

    private UISounds audio;


    public void Start()
    {
        GameObject btnObject = GameObject.Find("ItemDetailButton");
        btn = btnObject.GetComponent<Button>();
        details = GameObject.Find("ItemDetails");
        canvas = details.GetComponent<Canvas>();
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

        canvas.enabled = true;
    }

    public void use()
    {
        Debug.Log(item.itemId);
        item.use();
        depleatCheck();
    }

    public void depleatCheck()
    {
        manager.clearButtons();
        manager.doItem();
        if (States.inventory.items[States.itemPoint].count <= 0)
        {
            States.itemName = null;
            States.itemDesc = null;
            States.itemIcon = null;
            canvas.enabled = false;
            States.inventory.items.RemoveAt(pointer);
        }  
    }

    public void hide()
    {
        //int count = States.inventory.items[States.itemPoint].count;
        //Debug.Log("COUNT: " + count);
        if (States.hideCanvas == true)
        {
            canvas.enabled = false;
            States.hideCanvas = false;
        }
    }

}
