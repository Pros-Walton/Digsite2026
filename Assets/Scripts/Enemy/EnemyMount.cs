using UnityEngine;

public class EnemyMount : MonoBehaviour
{
    public Enemy enemy = null;
    public LayerMask worldMask;
    public AudioSource[] sounds;
    private bool isWalk;

    public void Mount(Enemy Enemy, int loc)
    {
        enemy = Enemy;
        enemy.enemyObj = this.gameObject;
        enemy.playerTarget = GameObject.Find("Player Temp");
        sounds = GetComponents<AudioSource>();
        enemy.getBody();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(enemy.playerTarget.transform.position, transform.position); 
        if  ( (dist <  5))
        {
            if (!Physics.Linecast(transform.position, enemy.playerTarget.transform.position, worldMask))
            {
                transform.LookAt(enemy.playerTarget.transform);
                if (dist > 1)
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
                        sounds[1].Play();
                        enemy.attackCooldown = enemy.cooldownMax;
                        float damageDone = ((float)enemy.attack / States.stats.armour.defense);
                        States.stats.health -= damageDone;
                        States.stats.armour.use();
                        if (States.stats.armour.shouldBreak())
                        {
                            States.stats.armour = States.inventory.armours[0];
                            States.inventory.armours.RemoveAt(0);
                        }
                        enemy.playerTarget.transform.position += (transform.forward / 5);
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
}
