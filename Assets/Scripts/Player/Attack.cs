using UnityEngine;
using UnityEngine.InputSystem;


public class Attack : MonoBehaviour
{
    public InputActionAsset InputActions;
    private InputAction actionAttack;

    private AudioSource[] sounds;

    public GameObject ammo;

    public LayerMask enemyMask;
    public LayerMask worldMask;

    public GameObject mdl;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
        animator = mdl.GetComponent<Animator>();

        actionAttack = InputSystem.actions.FindAction("Attack");

        sounds = GetComponents<AudioSource>();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Strike", false);
        animator.SetBool("Shoot", false);
        if (actionAttack.WasPressedThisFrame() && Time.timeScale == 1 && States.stats.stamina > 0)
        {

            if (UnityEngine.Random.Range(0,(States.hungerOdds/50)) == 0)
            {
                States.stats.stamina -= 0.3f;
            }

            if (States.stats.weapon.type == Weapon.weaponType.melee)
            {
                melee();
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

    private void melee()
    {
        animator.SetBool("Strike", true);
        sounds[1].Play();
        Collider[] targets = Physics.OverlapSphere(transform.position, 0.75f, enemyMask);

        foreach (Collider target in targets)
        {
            GameObject targetObject = target.gameObject;
            Vector3 targetAngle = (target.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, targetAngle) < 90)
            {
                EnemyMount mount = targetObject.GetComponent<EnemyMount>();
                Enemy enemy = mount.enemy;
                mount.sounds[2].Play();
                enemy.HP -= ((float)(States.stats.weapon.attack / States.diffMult) * (3.0f) / (enemy.defense * States.diffMult));
                States.stats.weapon.use();
                breakCheck();
                if (mount.hasAnimator)
                {
                    mount.animator.SetBool("Hit", true);
                }
                damageOffset(targetObject);
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

    void damageOffset(GameObject targetObject)
    {
        Vector3 movedPos = (targetObject.transform.position + transform.forward);
        if (Physics.Linecast(targetObject.transform.position, movedPos, 
        out RaycastHit hitInfo, worldMask, QueryTriggerInteraction.Ignore))
        {
            movedPos = hitInfo.point -(transform.forward / 10);
        }
        targetObject.transform.position = movedPos;
    }

    private void ranged()
    {
        animator.SetBool("Shoot", true);
        States.stats.weapon.use();
        States.stats.ammo -= 1;
        breakCheck();
        Vector3 pos = transform.position + transform.forward;
        GameObject ammunition = Instantiate(ammo, pos, Quaternion.identity);
        float offset = UnityEngine.Random.Range(-0.2f,0.2f);
        ammunition.transform.forward = transform.forward + new Vector3(0.0f, 0.0f, offset);
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
}
