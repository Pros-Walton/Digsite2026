using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    public InputActionAsset InputActions;

    private InputAction actionUse;

    public LayerMask objectMask;

    private AudioSource[] sounds;

    public GameObject mdl;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
        animator = mdl.GetComponent<Animator>();

        actionUse = InputSystem.actions.FindAction("Use");

        sounds = GetComponents<AudioSource>();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }


    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Grab", false);
        if (actionUse.WasPressedThisFrame())
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, 0.75f, objectMask);

            if (targets.Length > 0)
            {
                animator.SetBool("Grab", true);
            }

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
}
