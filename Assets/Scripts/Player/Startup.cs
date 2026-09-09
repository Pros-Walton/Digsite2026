using UnityEngine;
using System.IO;

public class Startup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartUp()
    {
        States.hungerOdds = 100;

        if (!States.LoadData && !States.NextLevel)
        {
             Debug.Log("CREATE NEW DATA");
            States.stats = new PlayerStats();
            States.inventory = new Inventory();           
        }
        if (States.LoadData)
        {
            Debug.Log("LOAD DATA");
            Load();
        }
        else if (States.NextLevel)
        {
            Debug.Log("NEXT LEVEL GEN!");
            States.stats.pos = new Vector3 ( 0.0f,0.4f, 0.0f);
            States.NextLevel = false;
        }
        
    }

    private void Load()
    {
        Time.timeScale = 1;
        string statPath = Application.persistentDataPath + "/Save/" + States.saveName + "/player.json";
        string invenPath = Application.persistentDataPath + "/Save/" + States.saveName + "/inventory.json";

        if (File.Exists(statPath))
        {

            string statString = File.ReadAllText(statPath);
            States.stats = JsonUtility.FromJson<PlayerStats>(statString);
            States.stats.applyBack();
        }
        else
        {
            File.Create(statPath);
            States.stats = new PlayerStats();
        }
        if (File.Exists(invenPath))
        {
            string inventoryString = File.ReadAllText(invenPath);
            States.inventory = JsonUtility.FromJson<Inventory>(inventoryString);
            States.inventory.applyBack();   
        }
        else
        {
            File.Create(invenPath);
            States.inventory = new Inventory();
        }
        States.diffMult = States.stats.diffMult;
        transform.position = States.stats.pos; 
        
    } 
}
