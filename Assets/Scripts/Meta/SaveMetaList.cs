using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveMetaList
{
    public List<string> saveMetaStr = new List<string>();
    public List<SaveMeta> saveMeta = new List<SaveMeta>();

    public void convert()
    {
        saveMeta.Clear();
        foreach (string str in saveMetaStr)
        {
            saveMeta.Add(JsonUtility.FromJson<SaveMeta>(str));
        }
    }

    public void convertBack()
    {
        saveMetaStr.Clear();
        foreach (SaveMeta meta in saveMeta)
        {
            saveMetaStr.Add(JsonUtility.ToJson(meta));
        }

    }
}
