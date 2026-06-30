using UnityEngine;

public class EnemyPoilot : MonoBehaviour
{

    public GameObject playerTarget;
    private float walk_Speed = 0.5f;
    private Rigidbody body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTarget = GameObject.Find("Player Temp");
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(playerTarget.transform.position, transform.position); 
        if  ( (dist <  5) && (dist > 1))
        {
            transform.LookAt(playerTarget.transform);
            transform.position += transform.forward / 100;
         
           transform.position = new Vector3(transform.position.x,0.4f,transform.position.z);
        }
        
    }
}
