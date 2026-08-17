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
        //Debug.Log(States.saveList.saveMeta[0].name);
        display();
    }

    // Update is called once per frame
    public void display()
    {
        foreach (Transform child in displayList.transform)
        {
            Destroy(child.gameObject);
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

}
