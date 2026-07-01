using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset InputActions;

    private InputAction action_North;
    private InputAction action_East;
    private InputAction action_South;
    private InputAction action_West;

    private InputAction action_Stand; 

    public float walk_Speed = 1.5f;
    private float player_Speed;
    private bool player_isWalk;

    private int hunger_odds = 1000;

    private Vector3 player_Walk;

    private PlayerStats stats;

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
        action_North = InputSystem.actions.FindAction("North");
        action_East = InputSystem.actions.FindAction("East");
        action_South = InputSystem.actions.FindAction("South");
        action_West = InputSystem.actions.FindAction("West");
        action_Stand = InputSystem.actions.FindAction("Stand");

    }

    private void Update()
    {
        walk();

    }

    private void walk()
    {
        if (action_Stand.IsPressed())
        {
            player_Speed = 0;
            player_isWalk = false;
        }
        else
        {
            player_Speed = walk_Speed;
            player_isWalk = true;
        }

        if (action_North.IsPressed())
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (player_isWalk)
            {
                transform.position += new Vector3(0,0,walk_Speed) * Time.deltaTime;
            }
            if (UnityEngine.Random.Range(0,hunger_odds) == 0)
            {
                stats.stamina -= 0.1f;
            }
        }
        else if (action_East.IsPressed())
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            if (player_isWalk)
            {
                transform.position += new Vector3(walk_Speed,0,0) * Time.deltaTime;
            }
            if (UnityEngine.Random.Range(0,hunger_odds) == 0)
            {
                stats.stamina -= 0.1f;
            }
        }
        else if (action_South.IsPressed())
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (player_isWalk)
            {
                transform.position += new Vector3(0,0,-walk_Speed) * Time.deltaTime;
            }
            if (UnityEngine.Random.Range(0,hunger_odds) == 0)
            {
                stats.stamina -= 0.1f;
            }
        }
        else if (action_West.IsPressed())
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            if (player_isWalk)
            {
                transform.position += new Vector3(-walk_Speed,0,0) * Time.deltaTime;
            }
            if (UnityEngine.Random.Range(0,hunger_odds) == 0)
            {
                stats.stamina -= 0.1f;
            }
        }
         transform.position = new Vector3(transform.position.x,0.4f,transform.position.z);
    }

    private void interact()
    {
        
    }

}
