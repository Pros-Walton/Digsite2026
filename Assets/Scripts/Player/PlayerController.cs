using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset InputActions;

    private InputAction actionNorth;
    private InputAction actionEast;
    private InputAction actionSouth;
    private InputAction actionWest;

    private InputAction actionStand;

    private InputAction actionAttack;
    private InputAction actionUse;
    private InputAction actionInventory;

    public float walkSpeed = 3.0f;
    private float playerSpeed;
    private bool playerIsWalk;

    private int hungerOdds = 100;

    private Vector3 playerWalk;

    private PlayerStats stats;
    private Weapon weapon;
    private Armour armour;

    private LayerMask targetMask;

    private Inventory inventory;

    private GameObject inventoryScreen;
    private Canvas inventoryCanvas;
    private Manager uiManager;
    private ReadoutPanel readout;

    private void OnEnable()
    {
        stats = this.GetComponent<PlayerStats>();
         Debug.Log(stats.stamina);
        InputActions.FindActionMap("Player").Enable();

        if (States.LoadData)
        {
            Load();
        }
        else
        {
            Debug.Log("New data!");
            stats.PopulateNew();
            inventory = new Inventory();
        }

        inventoryScreen = GameObject.Find("InventoryScreen");
        inventoryCanvas = inventoryScreen.GetComponent<Canvas>();
        uiManager = GameObject.Find("EventSystem").GetComponent<Manager>();
        readout = GameObject.Find("Inventory Readout").GetComponent<ReadoutPanel>();

        uiManager.giveInventory(inventory);


    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        actionNorth = InputSystem.actions.FindAction("North");
        actionEast = InputSystem.actions.FindAction("East");
        actionSouth = InputSystem.actions.FindAction("South");
        actionWest = InputSystem.actions.FindAction("West");
        actionStand = InputSystem.actions.FindAction("Stand");
        actionAttack = InputSystem.actions.FindAction("Attack");
        actionUse = InputSystem.actions.FindAction("Use");
        actionInventory = InputSystem.actions.FindAction("Inventory");

    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            walk();
        }
        interact();
    }

    private void walk()
    {
        if (actionStand.IsPressed())
        {
            playerSpeed = 0;
            playerIsWalk = false;
        }
        else
        {
            playerSpeed = walkSpeed;
            playerIsWalk = true;
        }

        if (actionNorth.IsPressed())
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (playerIsWalk)
            {
                transform.position += new Vector3(0,0,walkSpeed) * Time.deltaTime;
            }
            if (UnityEngine.Random.Range(0,hungerOdds) == 0)
            {
                stats.stamina -= 0.1f;
            }
        }
        else if (actionEast.IsPressed())
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            if (playerIsWalk)
            {
                transform.position += new Vector3(walkSpeed,0,0) * Time.deltaTime;
            }
            if (UnityEngine.Random.Range(0,hungerOdds) == 0)
            {
                stats.stamina -= 0.1f;
            }
        }
        else if (actionSouth.IsPressed())
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (playerIsWalk)
            {
                transform.position += new Vector3(0,0,-walkSpeed) * Time.deltaTime;
            }
            if (UnityEngine.Random.Range(0,hungerOdds) == 0)
            {
                stats.stamina -= 0.1f;
            }
        }
        else if (actionWest.IsPressed())
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            if (playerIsWalk)
            {
                transform.position += new Vector3(-walkSpeed,0,0) * Time.deltaTime;
            }
            if (UnityEngine.Random.Range(0,hungerOdds) == 0)
            {
                stats.stamina -= 0.1f;
            }
        }
         transform.position = new Vector3(transform.position.x,0.4f,transform.position.z);
    }

    private void interact()
    {
        if (actionAttack.WasPressedThisFrame() && Time.timeScale == 1)
        {

            Collider[] targets = Physics.OverlapSphere(transform.position, 0.75f);

            if (UnityEngine.Random.Range(0,(hungerOdds/50)) == 0)
            {
                stats.stamina -= 0.3f;
            }

            foreach (Collider target in targets)
            {
                Vector3 targetAngle = (target.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, targetAngle) < 60)
                {
                    if (target.gameObject.name.Contains("Enemy"))
                    {
                        EnemyPoilot enemy = target.gameObject.GetComponent<EnemyPoilot>();
                        enemy.HP -= (stats.weapon.attack * (3.0f) / enemy.defense);
                        stats.weapon.use();
                        target.gameObject.transform.position -= target.gameObject.transform.forward;
                        if (enemy.HP <= 0)
                        {
                            Destroy(target.gameObject);
                            stats.score += 10;
                        }
                    }

                }
            }
        }

        if (actionUse.WasPressedThisFrame())
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, 0.75f);


            foreach (Collider target in targets)
            {
                Vector3 targetAngle = (target.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, targetAngle) < 60)
                {
                    if (target.gameObject.name.Contains("Item"))
                    {
                        collectToInventory();
                        Destroy(target.gameObject);
                    }

                }
            }           
        }

        if (actionInventory.WasPressedThisFrame())
        {
            if (Time.timeScale == 1)
            {
                readout.clearDetails();
                uiManager.clearButtons();
                uiManager.doArmour();
                inventoryCanvas.enabled = true;
                Time.timeScale = 0;
            }
            else if (Time.timeScale == 0)
            {
                readout.clearDetails();
                uiManager.clearButtons();
                inventoryCanvas.enabled = false;
                Time.timeScale = 1;
            }
        }

    }

    private void Save()
    {
        // stats.weapon = JsonUtility.ToJson(weapon);
        // stats.armour = JsonUtility.ToJson(armour);
        string statString = JsonUtility.ToJson(stats);

        string path = Application.persistentDataPath + "/player.json";

        if(!File.Exists(path))
        {
            File.Create(path);
        }

        File.WriteAllText(path,statString);

    }

    private void Load()
    {
        string statPath = Application.persistentDataPath + "/player.json";
        string invenPath = Application.persistentDataPath + "/inventory.json";

        if (File.Exists(statPath))
        {

            string statString = File.ReadAllText(statPath);
            stats = JsonUtility.FromJson<PlayerStats>(statString);
        }
        else
        {
            File.Create(statPath);
            File.WriteAllText(statPath,"");
            stats.PopulateNew();
        }
        if (File.Exists(invenPath))
        {
            inventory = JsonUtility.FromJson<Inventory>(File.ReadAllText(invenPath));   
        }
        else
        {
            File.Create(invenPath);
            File.WriteAllText(invenPath,"");
            inventory = new Inventory();
        }
    }

    private void collectToInventory()
    {
        for (int i = 0; i < UnityEngine.Random.Range(5,10); i++)
        {
            int typeSelector = UnityEngine.Random.Range(4,10);

            switch(typeSelector)
            {
                case 3:
                    if (inventory.weapons.Count < 15)
                    {
                        inventory.weapons.Add(new Weapon(UnityEngine.Random.Range(0,0)));
                    }
                    //Debug.Log("New Weapon!");
                    break;
                case 4:
                    if (inventory.armours.Count < 15)
                    {
                        inventory.armours.Add(new Armour(UnityEngine.Random.Range(0,0)));
                    }
                    //Debug.Log("New Armour!");
                    break;
                default:
                    int itemType = UnityEngine.Random.Range(0,3);
                     bool isHere = false;
                    switch(itemType)
                    {    
                        case 0:
                            Artifact art = new Artifact(UnityEngine.Random.Range(0,0));
                            stats.score += art.score;
                            foreach (Artifact arti in inventory.artifacts)
                            {
                                if (arti.name == art.name)
                                {
                                    isHere = true;
                                    arti.count ++;
                                }
                            }
                            if (isHere)
                            {
                                continue;
                            }
                            else
                            {
                                inventory.artifacts.Add(art);
                            }
                            break;
                        case 1:
                            Item item = new Item(UnityEngine.Random.Range(0,2));
                            foreach (Item items in inventory.items)
                            {
                                Debug.Log(items.name + ", " + item.name);
                                if (items.name == item.name)
                                {
                                    isHere = true;
                                    items.count ++;
                                }
                            }
                            if (isHere)
                            {
                                continue;
                            }
                            else
                            {
                                inventory.items.Add(item);
                            }
                            break;
                    }
                    //Debug.Log("New Artifact!");
                    break;
            }
            //Debug.Log("Add inventory lol");
            }
            uiManager.giveInventory(inventory);
    }
    

}
