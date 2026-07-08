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

    private int hungerOdds = 1000;

    private Vector3 playerWalk;

    private PlayerStats stats;
    private Weapon weapon;
    private Armour armour;

    private LayerMask targetMask;

    private void start()
    {
        stats = this.GetComponent<PlayerStats>();
        Debug.Log(stats.stamina);
    }

    private void OnEnable()
    {
        stats = this.GetComponent<PlayerStats>();
         Debug.Log(stats.stamina);
        InputActions.FindActionMap("Player").Enable();
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

    }

    private void Update()
    {
        walk();
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
        if (actionAttack.WasPressedThisFrame())
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
        string path = Application.persistentDataPath + "/player.json";

        if (File.Exists(path))
        {

            string statString = File.ReadAllText(path);
            stats = JsonUtility.FromJson<PlayerStats>(statString);
        }
        else
        {
            stats = new PlayerStats();
        }
    }

    private void collectToInventory()
    {
        int typeSelector = UnityEngine.Random.Range(0,8);
        if (typeSelector == 5)
        {
            Debug.Log("New Weapon!");
        }
        else if (typeSelector == 7)
        {
            Debug.Log("New Armour!");
        }
        else
        {
            Debug.Log("New Artifact!");
        }
        Debug.Log("Add inventory lol");
    }

}
