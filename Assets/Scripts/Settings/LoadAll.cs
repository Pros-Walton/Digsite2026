using UnityEngine;
using UnityEngine.Audio;
using System.IO;
using System.Collections.Generic;


public class LoadAll : MonoBehaviour
{
    public AudioMixer mixer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        States.audioMixer = mixer;
        States.audioMixer.SetFloat("MasterVol", PlayerPrefs.GetInt("MasterVol", 0));
        States.audioMixer.SetFloat("MusicVol", PlayerPrefs.GetInt("MusicVol", 0));
        States.audioMixer.SetFloat("SFXVol", PlayerPrefs.GetInt("SFXVol", 0));
        States.audioMixer.SetFloat("UIVol", PlayerPrefs.GetInt("UIVol", 0));
    
        States.saveName = "Default";

        string path = Application.persistentDataPath + "/Save/meta.json";

        if (!File.Exists(path))
        {
            Debug.Log("Making meta.json");
            File.Create(path).Close();
        }


        string text = File.ReadAllText(path);
        Debug.Log(text);
        States.saveList = JsonUtility.FromJson<SaveMetaList>(text);

        States.saveList.convert();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
