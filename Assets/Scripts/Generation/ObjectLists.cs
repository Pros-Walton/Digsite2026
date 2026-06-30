using UnityEngine;

public class ObjectLists : MonoBehaviour
{

    public GameObject[] entities = new GameObject[1];

    public GameObject placeholder_Enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entities[0] = placeholder_Enemy;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
