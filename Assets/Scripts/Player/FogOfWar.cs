using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public LayerMask enemyMask;
    public LayerMask objectMask;
    public LayerMask worldMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject enemy in States.allEnemies)
        {

            enemy.GetComponent<Renderer>().enabled = false;
            enemy.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
            if (enemy.name.Contains("Zombie"))
            {
                for(int i = 0; i <=5; i++)
                {
                    enemy.transform.GetChild(0).GetChild(i).GetComponent<Renderer>().enabled = false;
                }
            }
        }
        Collider[] targetsEnemy = Physics.OverlapSphere(transform.position, 10.0f, enemyMask);
        foreach (Collider target in targetsEnemy)
        {
            GameObject enemy = target.gameObject;
            if (!Physics.Linecast(transform.position, enemy.transform.position, worldMask))
            {
                enemy.GetComponent<Renderer>().enabled = true;
                enemy.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
                if (enemy.name.Contains("Zombie"))
                {
                    for(int i = 0; i <=5; i++)
                    {
                        enemy.transform.GetChild(0).GetChild(i).GetComponent<Renderer>().enabled = true;
                    }
                }
            }
        }

        foreach (GameObject item in States.allItems)
        {
            item.GetComponent<Renderer>().enabled = false;
            item.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        }
        Collider[] targetsItem = Physics.OverlapSphere(transform.position, 10.0f, objectMask);
        foreach (Collider target in targetsItem)
        {
            GameObject item = target.gameObject;
            if (!Physics.Linecast(transform.position, item.transform.position, worldMask))
            {
                item.GetComponent<Renderer>().enabled = true;
                item.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
            }
        }
        
    }
}
