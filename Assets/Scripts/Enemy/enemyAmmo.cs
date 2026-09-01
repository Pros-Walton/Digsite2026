using UnityEngine;

public class enemyAmmo : MonoBehaviour
{
    public LayerMask worldMask;
    public LayerMask playerMask;
    public GameObject enemyObject;
    public Animator playerAnimator;
    public Enemy enemy;
    private float lifetime = 1.0f;
    private GameObject playerTarget;
    public bool hasHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {  
        playerTarget = GameObject.Find("Player Temp");
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        transform.position += transform.forward * 0.5f;
        if (Physics.Linecast(transform.position, enemyObject.transform.position, 
        out RaycastHit hitInfo, playerMask, QueryTriggerInteraction.Ignore))
        {
            playerAnimator.SetBool("Hit", true);
            States.playerEnemyHit = true;
            States.stats.health -= ((float)enemy.attack * States.diffMult) * 3.0f / (States.stats.armour.defense / States.diffMult);
            damageOffset();
            playerTarget.transform.forward = -transform.forward;
            Destroy(this.gameObject);
        }
        else if (Physics.Linecast(transform.position, playerTarget.transform.position, worldMask))
        {
            Destroy(this.gameObject);
        }
        else if (lifetime <= 0.0f)
        {
            Destroy(this.gameObject);
        }  
    }

    void damageOffset()
    {
        Vector3 movedPos = (playerTarget.transform.position + (transform.forward / 5));
        if (Physics.Linecast(transform.position, movedPos, 
        out RaycastHit hitInfo, worldMask, QueryTriggerInteraction.Ignore))
        {
            movedPos = hitInfo.point -(transform.forward / 10);
        }
        playerTarget.transform.position = movedPos;
    }
}
