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

    private bool playSound;

    private int hungerOdds = 100;

    private Weapon weapon;
    private Armour armour;

    public GameObject ammo;

    public LayerMask enemyMask;
    public LayerMask objectMask;
    public LayerMask worldMask;

    private AudioSource[] sounds;

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

        sounds = GetComponents<AudioSource>();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Update()
    {
        States.allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        States.allItems = GameObject.FindGameObjectsWithTag("Item");
        if (States.stats.health <= 0)
        {
            States.stats.GameOver();
        }

        if (Time.timeScale == 1)
        {
            walk();
        }
        interact();
        FogOfWar();

        States.stats.pos = transform.position;
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
            playerWalking(Quaternion.Euler(0, 0, 0), new Vector3(0,0,walkSpeed));
        }
        else if (actionEast.IsPressed())
        {
            playerWalking(Quaternion.Euler(0, 90, 0), new Vector3(walkSpeed,0,0));
        }
        else if (actionSouth.IsPressed())
        {
            playerWalking(Quaternion.Euler(0, 180, 0), new Vector3(0,0,-walkSpeed));
        }
        else if (actionWest.IsPressed())
        {
            playerWalking(Quaternion.Euler(0,270,0), new Vector3(-walkSpeed,0,0));
        }
        else
        {
            playSound = false;
            sounds[0].Pause();
        }
         transform.position = new Vector3(transform.position.x,0.4f,transform.position.z);
    }

    private void playerWalking(Quaternion rotation, Vector3 vector)
    {
        transform.rotation = rotation;
        if (playerIsWalk)
        {
            transform.position += vector * Time.deltaTime;
            if (!playSound)
            {
                playSound = true;
                sounds[0].Play();
            }
            if (UnityEngine.Random.Range(0,(int)(hungerOdds / States.diffMult)) == 0)
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

            if (UnityEngine.Random.Range(0,(hungerOdds/50)) == 0)
            {
                States.stats.stamina -= 0.3f;
            }

            if (States.stats.weapon.type == Weapon.weaponType.meele)
            {
                meele();
            }
            else 
            {
                if (States.stats.ammo > 0)
                {
                    ranged();
                }
            }
        }

    }

    private void meele()
    {
        sounds[1].Play();
        Collider[] targets = Physics.OverlapSphere(transform.position, 0.75f, enemyMask);

        foreach (Collider target in targets)
        {
            GameObject targetObject = target.gameObject;
            Vector3 targetAngle = (target.transform.position - transform.position).normalized;
            if ((Vector3.Angle(transform.forward, targetAngle) < 90) &&
            !targetObject.name.Contains("Front"))
            {
                EnemyMount mount = targetObject.GetComponent<EnemyMount>();
                Enemy enemy = mount.enemy;
                mount.sounds[2].Play();
                enemy.HP -= ((float)(States.stats.weapon.attack / States.diffMult) * (3.0f) / (enemy.defense * States.diffMult));
                States.stats.weapon.use();
                breakCheck();
                targetObject.transform.position -= targetObject.transform.forward;
                if (enemy.HP <= 0)
                {
                    enemyKill(targetObject, enemy, mount);
                }
                else
                {
                    States.tiles[enemy.locID].ent = JsonUtility.ToJson(enemy);
                }
            }
        }
    }

    private void ranged()
    {
        States.stats.weapon.use();
        States.stats.ammo -= 1;
        breakCheck();
        Vector3 pos = transform.position + transform.forward;
        GameObject ammunition = Instantiate(ammo, pos, Quaternion.identity);
        ammunition.transform.forward = transform.forward;
    }

    private void enemyKill(GameObject target, Enemy enemy, EnemyMount mount)
    {
        States.tiles[enemy.locID].ent = "NULL";
        mount.sounds[0].Stop();
        Destroy(target);
        States.stats.score += 10;

    }

    private void breakCheck()
    {
        if (States.stats.weapon.shouldBreak())
        {
            States.stats.weapon = States.inventory.weapons[0];
            States.inventory.weapons.RemoveAt(0);
        }       
    }

    private void use()
    {
        if (actionUse.WasPressedThisFrame())
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, 0.75f, objectMask);


            foreach (Collider target in targets)
            {
                GameObject targetObject = target.gameObject;
                Vector3 targetAngle = (target.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, targetAngle) < 60)
                {
                    if (targetObject.name.Contains("Item"))
                    {
                        PickupMount mount = targetObject.GetComponent<PickupMount>();
                        mount.Sound();
                        mount.pickup.GiveLoot();
                        States.tiles[mount.pickup.loc].obj = "NULL";
                        Destroy(targetObject, mount.sound.clip.length);
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
        string statPath = Application.persistentDataPath + "/Save/" + States.saveName + "/player.json";
        string invenPath = Application.persistentDataPath + "/Save/" + States.saveName + "/inventory.json";

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
        States.diffMult = States.stats.diffMult;
        transform.position = States.stats.pos; 
        
    }

    private void FogOfWar()
    {
        foreach (GameObject enemy in States.allEnemies)
        {
            enemy.GetComponent<Renderer>().enabled = false;
            enemy.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        }
        Collider[] targetsEnemy = Physics.OverlapSphere(transform.position, 10.0f, enemyMask);
        foreach (Collider target in targetsEnemy)
        {
            GameObject enemy = target.gameObject;
            if (!Physics.Linecast(transform.position, enemy.transform.position, worldMask))
            {
                enemy.GetComponent<Renderer>().enabled = true;
                enemy.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
            }
        }

        foreach (GameObject item in States.allItems)
        {
            item.GetComponent<Renderer>().enabled = false;
            item.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        }
        Collider[] targetsItem = Physics.OverlapSphere(transform.position, 10.0f, objectMask);
        foreach (Collider target in targetsItem)
        {
            GameObject item = target.gameObject;
            if (!Physics.Linecast(transform.position, item.transform.position, worldMask))
            {
                item.GetComponent<Renderer>().enabled = true;
                item.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
            }
        }

    }  
}
