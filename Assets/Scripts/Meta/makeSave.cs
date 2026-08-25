using UnityEngine;
using System.IO;
using System.Threading;
using System;
using TMPro;

public class makeSave : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TMP_InputField newSaveName;
    public TMP_Text DeleteName;
    void Start()
    {
        GameObject.Find("CanvasToggle").SetActive(States.toggleSaveMenu);

    }

    void Update()
    {
        DeleteName.text = States.saveName;
    }

    public void Save(bool isNew)
    {
        if (isNew)
        {
            States.saveName = newSaveName.text;
            DateTime now = DateTime.Now;
            SaveMeta newSave = new SaveMeta();
            newSave.name = States.saveName;
            newSave.date = now.ToString("yyyy-MM-dd HH:mm");
            States.saveList.saveMeta.Add(newSave);
        }
        else
        {
            foreach (SaveMeta meta in States.saveList.saveMeta)
            {
                if (meta.name == States.saveName)
                {
                    DateTime now = DateTime.Now;
                    meta.date = now.ToString("yyyy-MM-dd HH:mm");
                }
            }
        }

        if (States.saveName != null)
        {        

            States.stats.applyTo();
            States.inventory.applyTo();

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

            fileCheck(isNew);

            string masterPath = Application.persistentDataPath + "/Save/" + States.saveName;

            string playerPath = masterPath + "/player.json";
            string invenPath = masterPath +  "/inventory.json";
            string levelPath = masterPath +  "/level.json";

            File.WriteAllText(playerPath,statString);
            File.WriteAllText(invenPath,inventoryString);
            File.WriteAllText(levelPath,levelString);

            string metaString = JsonUtility.ToJson(States.saveList);
            File.WriteAllText((Application.persistentDataPath + "/Save/meta.json"), metaString);
        }
    }

    public void fileCheck(bool isNew)
    {
        Debug.Log("Start File Check.");
        string saveMaster = Application.persistentDataPath + "/Save/" + States.saveName;


        if (!Directory.Exists(saveMaster))
        {
            var dir = Directory.CreateDirectory(saveMaster);
            Debug.Log("Creating folder!");

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
        Debug.Log("Finished File Check!");

    }


    public void delete()
    {
        States.saveList.saveMeta.RemoveAll(static meta => meta.name == States.saveName);
        string metaString = JsonUtility.ToJson(States.saveList);
        File.WriteAllText((Application.persistentDataPath + "/Save/meta.json"), metaString);

        string path = Application.persistentDataPath + "/" + States.saveName;

        File.Delete(path + "/player.json");
        File.Delete(path + "/inventory.json");
        File.Delete(path + "/level.json");

        Directory.Delete(path);

        States.saveName = null;
    }
}
