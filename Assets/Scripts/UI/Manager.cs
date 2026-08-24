using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class Manager : MonoBehaviour
{
    public GameObject player;
    private PlayerStats stats;

    public TMP_Text uiHealth;
    public TMP_Text uiStamina;
    public TMP_Text uiScore;
    public TMP_Text uiGold;
    public TMP_Text uiDepth;
    public TMP_Text uiArmour;
    public TMP_Text uiWeapon;
    public TMP_Text uiAmmo;

    public Button btnArmour;

    public Button[] inventoryButtons;

    private List<Button> buttonList = new List<Button>();

    public GameObject inventoryGrid;

    private Vector3 gridMount; 

    private float resMult;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resMult = (Screen.height/1080.0f);

        uiDepth.text = States.stats.depth.ToString();
        btnArmour.Select();

    }

    // Update is called once per frame
    void Update()
    {
        gridMount = new Vector3 (Screen.width/10.5f, Screen.height/1.57f, 0);
        uiHealth.text = (((int)States.stats.health).ToString() + "/" + States.stats.health_max.ToString());
        uiStamina.text = (((int)States.stats.stamina).ToString() + "/" + States.stats.stamina_max.ToString());
        uiGold.text = States.stats.gold.ToString();
        uiScore.text = States.stats.score.ToString();
        uiAmmo.text = States.stats.ammo.ToString();

        if (States.stats.armour.durability == 0)
        {
            uiArmour.text = (States.stats.armour.defense + ",INF");
        }
        else
        {
            uiArmour.text = (States.stats.armour.defense + 
            "," + 
            (States.stats.armour.durability - States.stats.armour.used) + 
            "/" + 
            States.stats.armour.durability);
        }


        if (States.stats.weapon.durability == 0)
        {
            uiWeapon.text = (States.stats.weapon.attack + ",INF");
        }
        else
        {
            uiWeapon.text = (States.stats.weapon.attack + 
            "," + 
            (States.stats.weapon.durability - States.stats.weapon.used) + 
            "/" + 
            States.stats.weapon.durability);
        }

    }

    public void doArmour()
    {
        int width = 0;
        int height = 0;
        int current = 0;

        foreach (Armour arm in States.inventory.armours)
        {
            width = current % 5;
            height = (int)(current / 5);
            Vector3 offset = new Vector3((150*width)*resMult, (-150*height)*resMult, 0);

            Button btn = Instantiate(
                inventoryButtons[0],
                (gridMount + offset), 
                Quaternion.identity);
            btn.transform.SetParent(inventoryGrid.transform);
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
