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
        Debug.Log("now show armour!");
        int width = 0;
        int height = 0;
        int current = 0;
        foreach (Armour arm in inventory.armours)
        {
            width = current % 5;
            height = (int)(current / 5);
            Vector3 offset = new Vector3(150*width, -150*height, 0);

            Button btn = Instantiate(
                inventoryButton,
                (gridMount + offset), 
                Quaternion.identity);
            btn.transform.parent = inventoryGrid.transform;
            GameObject image = btn.transform.GetChild(0).gameObject;
            Image imageProper = image.GetComponent<Image>();
            imageProper.sprite = arm.icon;
            current ++;
        }
    }

    public void doWeapon()
    {
        Debug.Log("now show weapon!");
        int width = 0;
        int height = 0;
        int current = 0;
        foreach (Weapon wep in inventory.weapons)
        {
            width = current % 5;
            height = (int)(current / 5);
            Vector3 offset = new Vector3(150*width, -150*height, 0);
            Button btn = Instantiate(
                inventoryButton,
                (gridMount + offset), 
                Quaternion.identity);
            btn.transform.parent = inventoryGrid.transform;
            GameObject image = btn.transform.GetChild(0).gameObject;
            Image imageProper = image.GetComponent<Image>();
            imageProper.sprite = wep.icon;
            current ++;
        }
    }

    public void doItem()
    {
        Debug.Log("now show item!");
        int width = 0;
        int height = 0;
        int current = 0;
        foreach (Item item in inventory.items)
        {
            width = current % 5;
            height = (int)(current / 5);
            Vector3 offset = new Vector3(150*width, -150*height, 0);
            Button btn = Instantiate(
                inventoryButton,
                (gridMount + offset), 
                Quaternion.identity);
            btn.transform.parent = inventoryGrid.transform;
            GameObject image = btn.transform.GetChild(0).gameObject;
            Image imageProper = image.GetComponent<Image>();
            imageProper.sprite = item.icon;
            current ++;
        }
    }

    public void doKey()
    {
        Debug.Log("now show key!");
        int width = 0;
        int height = 0;
        int current = 0;
        foreach (KeyItems key in inventory.keys)
        {
            width = current % 5;
            height = (int)(current / 5);
            Vector3 offset = new Vector3(150*width, -150*height, 0);
            Button btn = Instantiate(
                inventoryButton,
                (gridMount + offset), 
                Quaternion.identity);
            btn.transform.parent = inventoryGrid.transform;
            GameObject image = btn.transform.GetChild(0).gameObject;
            Image imageProper = image.GetComponent<Image>();
            imageProper.sprite = key.icon;
            current ++;
        }
    }

    public void doArtifact()
    {
        Debug.Log("now show artifcat!");
        int width = 0;
        int height = 0;
        int current = 0;
        foreach (Artifact art in inventory.artifacts)
        {
            width = current % 5;
            height = (int)(current / 5);
            Vector3 offset = new Vector3(150*width, -150*height, 0);
            Button btn = Instantiate(
                inventoryButton,
                (gridMount + offset), 
                Quaternion.identity);
            btn.transform.parent = inventoryGrid.transform;
            GameObject image = btn.transform.GetChild(0).gameObject;
            Image imageProper = image.GetComponent<Image>();
            imageProper.sprite = art.icon;
            current ++;
        }
    }

    public void clearButtons()
    {
        foreach(Transform child in inventoryGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
