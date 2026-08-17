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
        if (States.saveName != null)
        {
            string masterPath = Application.persistentDataPath + "/";
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

            string playerPath = masterPath + States.saveName + "_player.json";
            string invenPath = masterPath + States.saveName + "_inventory.json";
            string levelPath = masterPath + States.saveName + "_level.json";

            File.WriteAllText(playerPath,statString);
            File.WriteAllText(invenPath,inventoryString);
            File.WriteAllText(levelPath,levelString);

            string metaString = JsonUtility.ToJson(States.saveList);
            File.WriteAllText((masterPath + "meta.json"), metaString);
        }
    }

    public void fileCheck(bool isNew)
    {

        string saveMaster = Application.persistentDataPath + "/" + States.saveName;

        string playerPath = saveMaster + "_player.json";
        string invenPath = saveMaster + "_inventory.json";
        string levelPath = saveMaster + "_level.json";

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
        File.WriteAllText((Application.persistentDataPath + "/meta.json"), metaString);

        string path = Application.persistentDataPath + "/" + States.saveName;

        File.Delete(path + "_player.json");
        File.Delete(path + "_inventory.json");
        File.Delete(path + "_level.json");

        States.saveName = null;
    }
}
