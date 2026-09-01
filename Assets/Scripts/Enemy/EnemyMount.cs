using UnityEngine;

public class EnemyMount : MonoBehaviour
{
    public Enemy enemy = null;
    public GameObject ammo;
    public LayerMask worldMask;
    public AudioSource[] sounds;
    private Renderer render;
    private bool isWalk;
    private bool hasAttacked = false;
    public bool hasAnimator = false;
    private Animator playerAnimator;
    public Animator animator;

    public void Mount(Enemy Enemy, int loc)
    {
        enemy = Enemy;
        enemy.enemyObj = this.gameObject;
        enemy.playerTarget = GameObject.Find("Player Temp");
        playerAnimator = enemy.playerTarget.transform.GetChild(0).GetComponent<Animator>();
        sounds = GetComponents<AudioSource>();
        render = GetComponent<Renderer>();

        enemy.getBody();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAnimator)
        {
            animator.SetBool("Strike", false);
            animator.SetBool("Shoot", false);
            animator.SetBool("Hit", false);
        }
        if (hasAttacked && States.playerEnemyHit)
        {
            playerAnimator.SetBool("Hit", false);
            hasAttacked = false;
            States.playerEnemyHit = false;
        }
        sounds[0].mute = !render.enabled;

        float dist = Vector3.Distance(enemy.playerTarget.transform.position, transform.position); 
        if  ( (dist <  5))
        {
            if (!Physics.Linecast(transform.position, enemy.playerTarget.transform.position, worldMask))
            {
                if (hasAnimator)
                    {
                        animator.SetBool("Walking", true);
                    }
                if (enemy.type == Enemy.weaponType.ranged)
                {
                    if (enemy.attackCooldown <= 0.0f)
                    {
                        ranged();
                    }
                    else 
                    {
                        enemy.attackCooldown -= Time.deltaTime * 2.0f;
                    }
                }
                transform.LookAt(enemy.playerTarget.transform);
                if (dist > 0.75f)
                {
                    if (!isWalk)
                    {
                        isWalk = true;
                        sounds[0].Play();
                    }
                    transform.position += (transform.forward * Time.deltaTime);

                    transform.position = new Vector3(transform.position.x,
                    transform.localScale.y,
                    transform.position.z);
                }
                else
                {
                    if (hasAnimator)
                    {
                        animator.SetBool("Walking", false);
                    }
                    isWalk = false;
                    sounds[0].Pause();
                    if (enemy.type == Enemy.weaponType.melee)
                    {
                        if (enemy.attackCooldown <= 0.0f)
                        {
                            hasAttacked = true;

                            States.playerEnemyHit = true;
                            melee();
                        }
                        else
                        {
                            if (hasAnimator)
                            {
                                animator.SetBool("Walking", false);
                            }
                            isWalk = false;
                            sounds[0].Pause();
                            //Debug.Log(Time.deltaTime * 2);
                            enemy.attackCooldown -= Time.deltaTime * 2.0f;
                        }
                    }
                }
            }
            else
            {
                if (hasAnimator)
                {
                    animator.SetBool("Walking", false);
                }
            }
        }  
    }

    void melee()
    {
        if (hasAnimator)
        {
            animator.SetBool("Strike", true);
        }
        playerAnimator.SetBool("Hit", true);
        sounds[1].Play();
        enemy.attackCooldown = enemy.cooldownMax;
        float damageDone = ((float)(enemy.attack * States.diffMult) / (States.stats.armour.defense / States.diffMult));
        States.stats.health -= damageDone;
        States.stats.armour.use();
        if (States.stats.armour.shouldBreak())
        {
            States.stats.armour = States.inventory.armours[0];
            States.inventory.armours.RemoveAt(0);
        }
        damageOffset();
        enemy.playerTarget.transform.forward = -transform.forward;
    }

    void ranged()
    {
        if (hasAnimator)
        {
            animator.SetBool("Shoot", true);
        }
        enemy.attackCooldown = enemy.cooldownMax;
        Vector3 pos = transform.position + transform.forward;
        GameObject ammunition = Instantiate(ammo, pos, Quaternion.identity);
        ammunition.transform.forward = transform.forward + new Vector3(0.0f, (float)UnityEngine.Random.Range(-0.01f,0.01f),0.0f);
        enemyAmmo ammoLink = ammunition.GetComponent<enemyAmmo>();
        ammoLink.enemyObject = this.gameObject;
        ammoLink.playerAnimator = playerAnimator;
        ammoLink.enemy = enemy;
    }

    void damageOffset()
    {
        Vector3 movedPos = (enemy.playerTarget.transform.position + (transform.forward / 5));
        if (Physics.Linecast(transform.position, movedPos, 
        out RaycastHit hitInfo, worldMask, QueryTriggerInteraction.Ignore))
        {
            movedPos = hitInfo.point -(transform.forward / 10);
        }
        enemy.playerTarget.transform.position = movedPos;
    }
}
