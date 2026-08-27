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
    private Animator animator;

    public void Mount(Enemy Enemy, int loc)
    {
        enemy = Enemy;
        enemy.enemyObj = this.gameObject;
        enemy.playerTarget = GameObject.Find("Player Temp");
        animator = enemy.playerTarget.transform.GetChild(0).GetComponent<Animator>();
        sounds = GetComponents<AudioSource>();
        render = GetComponent<Renderer>();
        enemy.getBody();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAttacked && States.playerEnemyHit)
        {
            animator.SetBool("Hit", false);
            hasAttacked = false;
            States.playerEnemyHit = false;
        }
        sounds[0].mute = !render.enabled;

        float dist = Vector3.Distance(enemy.playerTarget.transform.position, transform.position); 
        if  ( (dist <  5))
        {
            if (!Physics.Linecast(transform.position, enemy.playerTarget.transform.position, worldMask))
            {
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
                    0.4f,
                    transform.position.z);
                }
                else
                {
                    isWalk = false;
                    sounds[0].Pause();
                    if (enemy.attackCooldown <= 0.0f)
                    {
                        hasAttacked = true;
                        if (enemy.type == Enemy.weaponType.melee)
                        {
                            States.playerEnemyHit = true;
                            melee();
                        }
                        else
                        {
                            ranged();
                        }
                    }
                    else
                    {
                        isWalk = false;
                        sounds[0].Pause();
                        //Debug.Log(Time.deltaTime * 2);
                        enemy.attackCooldown -= Time.deltaTime * 2.0f;
                    }
                }
            }
        }  
    }

    void melee()
    {
        animator.SetBool("Hit", true);
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
        enemy.playerTarget.transform.position += (transform.forward / 5);
        enemy.playerTarget.transform.forward = -transform.forward;
    }

    void ranged()
    {
        Vector3 pos = transform.position + transform.forward;
        GameObject ammunition = Instantiate(ammo, pos, Quaternion.identity);
        ammunition.transform.forward = transform.forward + new Vector3(0.0f, (float)UnityEngine.Random.Range(-0.01f,0.01f),0.0f);
        enemyAmmo ammoLink = ammunition.GetComponent<enemyAmmo>();
        ammoLink.enemyObject = this.gameObject;
        ammoLink.animator = animator;
        ammoLink.enemy = enemy;
    }
}
