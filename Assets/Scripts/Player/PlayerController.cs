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

    public float walkSpeed = 3.0f;
    private float playerSpeed;
    private bool playerIsWalk;

    private int hungerOdds = 100;

    private Vector3 playerWalk;

    private Weapon weapon;
    private Armour armour;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();

        if (States.LoadData)
        {
            Load();
        }
        else
        {
            States.stats = new PlayerStats();
            States.inventory = new Inventory();
        }

        actionNorth = InputSystem.actions.FindAction("North");
        actionEast = InputSystem.actions.FindAction("East");
        actionSouth = InputSystem.actions.FindAction("South");
        actionWest = InputSystem.actions.FindAction("West");
        actionStand = InputSystem.actions.FindAction("Stand");
        actionAttack = InputSystem.actions.FindAction("Attack");
        actionUse = InputSystem.actions.FindAction("Use");
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
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
                States.stats.stamina -= 0.1f;
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
                States.stats.stamina -= 0.1f;
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
                States.stats.stamina -= 0.1f;
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
                States.stats.stamina -= 0.1f;
            }
        }
         transform.position = new Vector3(transform.position.x,0.4f,transform.position.z);
    }

    private void interact()
    {
        attack();
        use();
    }

    private void attack()
    {
        if (actionAttack.WasPressedThisFrame() && Time.timeScale == 1)
        {

            Collider[] targets = Physics.OverlapSphere(transform.position, 0.75f);

            if (UnityEngine.Random.Range(0,(hungerOdds/50)) == 0)
            {
                States.stats.stamina -= 0.3f;
            }

            foreach (Collider target in targets)
            {
                GameObject targetObject = target.gameObject;
                Vector3 targetAngle = (target.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, targetAngle) < 60)
                {
                    if (targetObject.name.Contains("Enemy"))
                    {
                        EnemyPilot enemy = targetObject.GetComponent<EnemyPilot>();
                        enemy.HP -= ((float)States.stats.weapon.attack * (3.0f) / enemy.defense);
                        States.stats.weapon.use();
                        if (States.stats.weapon.shouldBreak())
                            {
                                States.stats.weapon = States.inventory.weapons[0];
                                States.inventory.weapons.RemoveAt(0);
                            }
                        targetObject.transform.position -= targetObject.transform.forward;
                        if (enemy.HP <= 0)
                        {
                            Destroy(targetObject);
                            States.stats.score += 10;
                        }
                    }

                }
            }
        }

    }

    private void use()
    {
        if (actionUse.WasPressedThisFrame())
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, 0.75f);


            foreach (Collider target in targets)
            {
                GameObject targetObject = target.gameObject;
                Vector3 targetAngle = (target.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, targetAngle) < 60)
                {
                    if (targetObject.name.Contains("Item"))
                    {
                        PickupMount mount = targetObject.GetComponent<PickupMount>();
                        mount.pickup.GiveLoot();
                        States.tiles[mount.pickup.loc].obj = "NULL";
                        Destroy(targetObject);
                    }

                }
            }           
        }

    }

    private void Load()
    {
        Time.timeScale = 1;
        string statPath = Application.persistentDataPath + "/Save/player.json";
        string invenPath = Application.persistentDataPath + "/Save/inventory.json";

        if (File.Exists(statPath))
        {

            string statString = File.ReadAllText(statPath);
            States.stats = JsonUtility.FromJson<PlayerStats>(statString);
            States.stats.applyBack();
        }
        else
        {
            File.Create(statPath);
            States.stats = new PlayerStats();
        }
        if (File.Exists(invenPath))
        {
            string inventoryString = File.ReadAllText(invenPath);
            States.inventory = JsonUtility.FromJson<Inventory>(inventoryString);
            States.inventory.applyBack();   
        }
        else
        {
            File.Create(invenPath);
            States.inventory = new Inventory();
        }
        transform.position = States.stats.pos; 
        
    }  
}
