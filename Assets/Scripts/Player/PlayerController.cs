using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using UnityEngine.SceneManagement;


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
        else if (States.NextLevel)
        {
            States.NextLevel = false;
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

        if (States.stats.health <= 0)
        {
            States.stats.GameOver();
        }

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
           walkHunger();
        }
        else if (actionEast.IsPressed())
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            if (playerIsWalk)
            {
                transform.position += new Vector3(walkSpeed,0,0) * Time.deltaTime;
            }
            walkHunger();
        }
        else if (actionSouth.IsPressed())
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (playerIsWalk)
            {
                transform.position += new Vector3(0,0,-walkSpeed) * Time.deltaTime;
            }
            walkHunger();
        }
        else if (actionWest.IsPressed())
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            if (playerIsWalk)
            {
                transform.position += new Vector3(-walkSpeed,0,0) * Time.deltaTime;
            }
            walkHunger();
        }
         transform.position = new Vector3(transform.position.x,0.4f,transform.position.z);
    }

    private void walkHunger()
    {
        if (UnityEngine.Random.Range(0,hungerOdds) == 0)
        {
            if (States.stats.stamina > 0)
            {
                States.stats.stamina -= 0.1f;
            }
            else
            {
                States.stats.health -= 0.5f;
            }
        }

    }

    private void interact()
    {
        attack();
        use();
    }

    private void attack()
    {
        if (actionAttack.WasPressedThisFrame() && Time.timeScale == 1 && States.stats.stamina > 0)
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
                        EnemyMount mount = targetObject.GetComponent<EnemyMount>();
                        Enemy enemy = mount.enemy;
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
                            States.tiles[enemy.locID].ent = "NULL";
                            Destroy(targetObject);
                            States.stats.score += 10;
                        }
                        else
                        {
                            States.tiles[enemy.locID].ent = JsonUtility.ToJson(enemy);
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

                    if (targetObject.name.Contains("Hole"))
                    {
                        Debug.Log("NEXT LEVEL!");
                        States.stats.depth ++;
                        Time.timeScale = 1;
                        States.NextLevel = true;
                        SceneManager.LoadScene("Scenes/Loading");
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
