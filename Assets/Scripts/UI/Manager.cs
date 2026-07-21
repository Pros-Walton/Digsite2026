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
    public TMP_Text ui_armour;
    public TMP_Text ui_weapon;

    public Button btn_armour;

    public Button[] inventoryButtons;

    private List<Button> buttonList = new List<Button>();

    public GameObject inventoryGrid;

    private Vector3 gridMount; 

    private float resMult;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resMult = (Screen.height/1080.0f);

        ui_depth.text = States.stats.depth.ToString();
        btn_armour.Select();

        gridMount = new Vector3 (Screen.width/10.5f, Screen.height/1.57f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        ui_health.text = (((int)States.stats.health).ToString() + "/" + States.stats.health_max.ToString());
        ui_stamina.text = (((int)States.stats.stamina).ToString() + "/" + States.stats.stamina_max.ToString());
        ui_gold.text = States.stats.gold.ToString();
        ui_score.text = States.stats.score.ToString();

        if (States.stats.armour.durability == 0)
        {
            ui_armour.text = (States.stats.armour.defense + ",INF");
        }
        else
        {
            ui_armour.text = (States.stats.armour.defense + 
            "," + 
            (States.stats.armour.durability - States.stats.armour.used) + 
            "/" + 
            States.stats.armour.durability);
        }


        if (States.stats.weapon.durability == 0)
        {
            ui_weapon.text = (States.stats.weapon.attack + ",INF");
        }
        else
        {
            ui_weapon.text = (States.stats.weapon.attack + 
            "," + 
            (States.stats.weapon.durability - States.stats.weapon.used) + 
            "/" + 
            States.stats.weapon.durability);
        }

    }

    public void doArmour()
    {
        Debug.Log("now show armour!");
        int width = 0;
        int height = 0;
        int current = 0;

        Debug.Log(States.inventory);
        foreach (Armour arm in States.inventory.armours)
        {
            width = current % 5;
            height = (int)(current / 5);
            Vector3 offset = new Vector3((150*width)*resMult, (-150*height)*resMult, 0);

            Button btn = Instantiate(
                inventoryButtons[0],
                (gridMount + offset), 
                Quaternion.identity);
            btn.transform.parent = inventoryGrid.transform;
            btn.transform.localScale = new Vector3(resMult*1.25f,resMult*1.25f,1);

            GameObject image = btn.transform.GetChild(0).gameObject;
            Image imageProper = image.GetComponent<Image>();
            imageProper.sprite = arm.icon;

            ArmourBtn amrBtn = btn.GetComponent<ArmourBtn>();

            amrBtn.setArmour(arm, current);

            current ++;
        }
    }

    public void doWeapon()
    {
        Debug.Log("now show weapon!");
        int width = 0;
        int height = 0;
        int current = 0;

        foreach (Weapon wep in States.inventory.weapons)
        {
            width = current % 5;
            height = (int)(current / 5);
            Vector3 offset = new Vector3((150*width)*resMult, (-150*height)*resMult, 0);
            Button btn = Instantiate(
                inventoryButtons[1],
                (gridMount + offset), 
                Quaternion.identity);
            btn.transform.parent = inventoryGrid.transform;
            btn.transform.localScale = new Vector3(resMult*1.25f,resMult*1.25f,1);

            GameObject image = btn.transform.GetChild(0).gameObject;
            Image imageProper = image.GetComponent<Image>();
            imageProper.sprite = wep.icon;

            WeaponBtn wepBtn = btn.GetComponent<WeaponBtn>();
            
            wepBtn.setWeapon(wep, current);

            current ++;
        }
    }

    public void doItem()
    {
        Debug.Log("now show item!");
        int width = 0;
        int height = 0;
        int current = 0;

        foreach (Item item in States.inventory.items)
        {
            width = current % 5;
            height = (int)(current / 5);
            Vector3 offset = new Vector3((150*width)*resMult, (-150*height)*resMult, 0);
            Button btn = Instantiate(
                inventoryButtons[2],
                (gridMount + offset), 
                Quaternion.identity);
            btn.transform.parent = inventoryGrid.transform;
            btn.transform.localScale = new Vector3(resMult*1.25f,resMult*1.25f,1);

            GameObject image = btn.transform.GetChild(0).gameObject;
            Image imageProper = image.GetComponent<Image>();
            imageProper.sprite = item.icon;

            ItemBtn itemBtn = btn.GetComponent<ItemBtn>();
            
            itemBtn.setItem(item, current);
            current ++;
        }
    }

    public void doKey()
    {
        Debug.Log("now show key!");
        int width = 0;
        int height = 0;
        int current = 0;

        foreach (Key key in States.inventory.keys)
        {
            width = current % 5;
            height = (int)(current / 5);
            Vector3 offset = new Vector3((150*width)*resMult, (-150*height)*resMult, 0);
            Button btn = Instantiate(
                inventoryButtons[3],
                (gridMount + offset), 
                Quaternion.identity);
            btn.transform.parent = inventoryGrid.transform;
            btn.transform.localScale = new Vector3(resMult*1.25f,resMult*1.25f,1);
            
            
            GameObject image = btn.transform.GetChild(0).gameObject;
            Image imageProper = image.GetComponent<Image>();
            imageProper.sprite = key.icon;

            KeyBtn keyBtn = btn.GetComponent<KeyBtn>();
            
            keyBtn.setKey(key);

            current ++;
        }
    }

    public void doArtifact()
    {
        Debug.Log("now show artifcat!");
        int width = 0;
        int height = 0;
        int current = 0;
        foreach (Artifact art in States.inventory.artifacts)
        {
            width = current % 5;
            height = (int)(current / 5);
           Vector3 offset = new Vector3((150*width)*resMult, (-150*height)*resMult, 0);
            Button btn = Instantiate(
                inventoryButtons[4],
                (gridMount + offset), 
                Quaternion.identity);
            btn.transform.parent = inventoryGrid.transform;
            btn.transform.localScale = new Vector3(resMult*1.25f,resMult*1.25f,1);

            GameObject image = btn.transform.GetChild(0).gameObject;
            Image imageProper = image.GetComponent<Image>();
            imageProper.sprite = art.icon;

            ArtifactBtn artBtn = btn.GetComponent<ArtifactBtn>();
            
            artBtn.setArtifact(art);

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
