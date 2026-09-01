using UnityEngine;

public class ammo : MonoBehaviour
{
    public LayerMask worldMask;
    public LayerMask enemyMask;
    private float lifetime = 1.0f;
    private GameObject playerTarget;
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
        if (Physics.Linecast(transform.position, playerTarget.transform.position, 
        out RaycastHit hitInfo, enemyMask, QueryTriggerInteraction.Ignore))
        {
            Destroy(this.gameObject);
            enemyHit(hitInfo);
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

    private void enemyHit(RaycastHit hitInfo)
    {
        GameObject targetObject = hitInfo.transform.gameObject;
        EnemyMount mount = targetObject.GetComponent<EnemyMount>();
        Enemy enemy = mount.enemy;
        if (mount.hasAnimator)
        {
            mount.animator.SetBool("Hit", true);
        }
        enemy.HP -= ((float)(States.stats.weapon.attack / States.diffMult) * (3.0f) / (enemy.defense * States.diffMult));
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

    private void enemyKill(GameObject target, Enemy enemy, EnemyMount mount)
    {
        States.tiles[enemy.locID].ent = "NULL";
        mount.sounds[0].Stop();
        Destroy(target);
        States.stats.score += 10;

    }

    void damageOffset(GameObject targetObject)
    {
        Vector3 movedPos = (targetObject.transform.position + playerTarget.transform.forward);
        if (Physics.Linecast(targetObject.transform.position, movedPos, 
        out RaycastHit hitInfo, worldMask, QueryTriggerInteraction.Ignore))
        {
            movedPos = hitInfo.point -(playerTarget.transform.forward / 10);
        }
        targetObject.transform.position = movedPos;
    }
}
