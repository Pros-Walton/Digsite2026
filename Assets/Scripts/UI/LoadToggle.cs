using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;


public class LoadToggle : MonoBehaviour
{
    public Button btn;
    public TMP_Text text;
    public bool check;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (check)
        {
            Check();
        }
    }

    // Update is called once per frame
    public void Check()
    {

        bool exists = File.Exists(Application.persistentDataPath + "_level.json");
        btn.enabled = exists;
        btn.GetComponent<Image>().enabled = exists;
        text.enabled = exists;
        
    }
}
