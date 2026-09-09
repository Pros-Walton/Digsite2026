using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public InputActionAsset InputActions;

    private InputAction actionNorth;
    private InputAction actionEast;
    private InputAction actionSouth;
    private InputAction actionWest;

    private InputAction actionStand;

    public float walkSpeed = 3.0f;
    private float playerSpeed;
    private bool playerIsWalk;

    private bool playSound;

    private AudioSource[] sounds;

    public GameObject mdl;
    private Animator animator;


    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
        animator = mdl.GetComponent<Animator>();

        actionNorth = InputSystem.actions.FindAction("North");
        actionEast = InputSystem.actions.FindAction("East");
        actionSouth = InputSystem.actions.FindAction("South");
        actionWest = InputSystem.actions.FindAction("West");
        actionStand = InputSystem.actions.FindAction("Stand");

        sounds = GetComponents<AudioSource>();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Update()
    {
        animator.SetBool("Hit", false);
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

        States.stats.pos = transform.position;
    }

    private void walk()
    {
        if (actionStand.IsPressed())
        {
            playerSpeed = 0;
            playerIsWalk = false;
            animator.SetBool("Walking", false);
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
            animator.SetBool("Walking", false);
        }
        if (animator.GetBool("Walking") == true && playerIsWalk == false)
        {
            animator.SetBool("Walking", false);
        }
         transform.position = new Vector3(transform.position.x,0.4f,transform.position.z);
         
    }

    private void playerWalking(Quaternion rotation, Vector3 vector)
    {
        animator.SetBool("Walking", true);
        transform.rotation = rotation;
        if (playerIsWalk)
        {
            transform.position += vector * Time.deltaTime;
            if (!playSound)
            {
                playSound = true;
                sounds[0].Play();
            }
            if (UnityEngine.Random.Range(0,(int)(States.hungerOdds / States.diffMult)) == 0)
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
}
