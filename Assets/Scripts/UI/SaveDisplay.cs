using UnityEngine;
using UnityEngine.Audio;
using System.IO;
using System;
using TMPro;

public class SaveDisplay : MonoBehaviour
{
    public GameObject displayList;
    public GameObject entry;
    public TMP_InputField newSaveName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(States.saveList.saveMeta[0].name);
        display();
    }

    // Update is called once per frame
    public void display()
    {
        while (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        
        foreach (SaveMeta meta in States.saveList.saveMeta)
        {
            GameObject current = Instantiate(entry);
            SaveButton currentMeta = current.GetComponent<SaveButton>();
            currentMeta.name = meta.name;
            currentMeta.date = meta.date;
            current.transform.SetParent(displayList.transform);
            current.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
        }
    }

    
    public void Save(bool isNew)
    {
        string masterPath = Application.persistentDataPath + "/Save/";
        if (isNew)
        {
            States.saveName = newSaveName.text;
            DateTime now = DateTime.Now;
            SaveMeta newSave = new SaveMeta();
            newSave.name = States.saveName;
            newSave.date = now.ToString("yyyy-MM-dd hh:mm");
            States.saveList.saveMeta.Add(newSave);
        }
        else
        {
            foreach (SaveMeta meta in States.saveList.saveMeta)
            {
                if (meta.name == States.saveName)
                {
                    DateTime now = DateTime.Now;
                    meta.date = now.ToString("yyyy-MM-dd hh:mm");
                }
            }
        }

        States.stats.applyTo();
        States.inventory.applyTo();
        States.stats.pos = transform.position;

        States.tileData.Clear();
        foreach (Tile curTile in States.tiles)
        {
            string serial = JsonUtility.ToJson(curTile);
            States.tileData.Add(serial);
        }
        States.leveldata.data = States.tileData;

        string statString = JsonUtility.ToJson(States.stats);
        string inventoryString = JsonUtility.ToJson(States.inventory);
        string levelString = JsonUtility.ToJson(States.leveldata);

        string playerPath = masterPath + States.saveName + "/player.json";
        string invenPath = masterPath + States.saveName + "/inventory.json";
        string levelPath = masterPath + States.saveName + "/level.json";

        File.WriteAllText(playerPath,statString);
        File.WriteAllText(invenPath,inventoryString);
        File.WriteAllText(levelPath,levelString);

        File.WriteAllText(masterPath + "meta.json", JsonUtility.ToJson(States.saveList));

    }

    public void fileCheck()
    {

        string saveMaster = Application.persistentDataPath + "/Save/" + States.saveName;
        if(!Directory.Exists(saveMaster))
        {
            Directory.CreateDirectory(saveMaster);
        }

        string playerPath = saveMaster + "/player.json";
        string invenPath = saveMaster + "/inventory.json";
        string levelPath = saveMaster + "/level.json";

        if(!File.Exists(levelPath))
        {
            File.Create(levelPath).Close();
        }

        if(!File.Exists(playerPath))
        {
            File.Create(playerPath).Close();
        }

        if(!File.Exists(invenPath))
        {
            File.Create(invenPath).Close();
        }

    }

}
