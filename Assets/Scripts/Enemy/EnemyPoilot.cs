using UnityEngine;

public class EnemyPoilot : MonoBehaviour
{

    public GameObject playerTarget;
    private float walk_Speed = 1.5f;
    private Rigidbody body;

    private float attackCooldown = 0.0f;

    private PlayerStats stats;

    public int attack;
    public int defense;

    public int maxHP;
    public float HP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HP = maxHP;
        playerTarget = GameObject.Find("Player Temp");
        body = GetComponent<Rigidbody>();
        stats = playerTarget.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(playerTarget.transform.position, transform.position); 
        if  ( (dist <  5))
        {
            transform.LookAt(playerTarget.transform);
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                if (hit.collider.gameObject.name == playerTarget.name)
                {
                    if (dist > 1)
                    {
                        transform.position += (transform.forward * Time.deltaTime);

                        transform.position = new Vector3(transform.position.x,0.4f,transform.position.z);
                    }
                    else
                    {
                        if (attackCooldown <= 0.0f)
                        {
                            attackCooldown = 1.0f;
                            stats.health -= 0.5f * attack; 
                            playerTarget.transform.position += (transform.forward / 5);
                        }
                        else
                        {
                            //Debug.Log(Time.deltaTime * 2);
                            attackCooldown -= Time.deltaTime * 2f;
                        }
                    }
                }
            }
        }
        
    }
}
