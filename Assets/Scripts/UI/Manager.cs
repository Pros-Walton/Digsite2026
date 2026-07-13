using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class Manager : MonoBehaviour
{
    public GameObject player;
    private PlayerStats stats;

    public TMP_Text ui_health;
    public TMP_Text ui_stamina;
    public TMP_Text ui_score;
    public TMP_Text ui_gold;
    public TMP_Text ui_depth;

    public Button btn_armour;

    public Button inventoryButton;

    private List<Button> buttonList = new List<Button>();

    public GameObject inventoryGrid;

    private Vector3 gridMount; 

    private Inventory inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats = player.GetComponent<PlayerStats>();
        ui_depth.text = stats.depth.ToString();
        btn_armour.Select();

        gridMount = new Vector3 (Screen.width/8.5f, Screen.height/1.57f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        ui_health.text = (((int)stats.health).ToString() + "/" + stats.health_max.ToString());
        ui_stamina.text = (((int)stats.stamina).ToString() + "/" + stats.stamina_max.ToString());
        ui_gold.text = stats.gold.ToString();
        ui_score.text = stats.score.ToString();


    }

    public void giveInventory(Inventory inventoryGive)
    {
        inventory = inventoryGive;
    }

    public void doArmour()
    {
        foreach (Button btn in buttonList)
        {
            Debug.Log("DESTROY!!");
            Destroy(btn);
        }
        buttonList.RemoveAll( s => s == null);
        Debug.Log("now show armour!");
        foreach (Armour arm in inventory.armours)
        {
            Button btn = Instantiate(
                inventoryButton,
                gridMount, 
                Quaternion.identity);
            btn.transform.parent = inventoryGrid.transform;
            buttonList.Add(btn);
            Destroy(btn);
            Debug.Log(arm.name);
        }
        Debug.Log(buttonList.Count);
    }

    public void doWeapon()
    {
        foreach (Button btn in buttonList)
        {
            Destroy(btn);
        }
        buttonList.RemoveAll( s => s == null);
        Debug.Log("now show weapon!");
        foreach (Weapon wep in inventory.weapons)
        {
            Debug.Log(wep.name);
        }
    }

    public void doItem()
    {
        foreach (Button btn in buttonList)
        {
            Destroy(btn);
        }
        buttonList.RemoveAll( s => s == null);
        Debug.Log("now show item!");
        foreach (Item item in inventory.items)
        {
            Debug.Log(item.name);
        }
    }

    public void doKey()
    {
        foreach (Button btn in buttonList)
        {
            Destroy(btn);
        }
        buttonList.RemoveAll( s => s == null);
        Debug.Log("now show key!");
        foreach (KeyItems key in inventory.keys)
        {
            Debug.Log(key.name);
        }
    }

    public void doArtifact()
    {
        foreach (Button btn in buttonList)
        {
            Destroy(btn);
        }
        buttonList.RemoveAll( s => s == null);
        Debug.Log("now show artifcat!");
        foreach (Artifact art in inventory.artifacts)
        {
            Debug.Log(art.name);
        }
    }
}
