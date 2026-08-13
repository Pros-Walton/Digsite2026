using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class VideoSettings : MonoBehaviour
{
    public Toggle toggle;
    public TMP_Dropdown dropdown;
    private List<Resolution> ResSet = new List<Resolution>();
    private List<string> ResStr = new List<string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int value = 0;
        toggle.isOn =  Screen.fullScreen;
        Resolution[] resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions)
        {
            float aspect = (float)res.width/(float)res.height;
            if ((aspect >= 1.77f) && (aspect <= 1.78f))
            {
                if (res.width == Screen.width)
                {
                   value = ResSet.Count; 
                }
                ResSet.Add(res);
                ResStr.Add(res.width.ToString() + "x" + res.height.ToString());
            }
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(ResStr);
        dropdown.value = value;
        
    }

    public void Apply()
    {
        int target = dropdown.value;

        Screen.SetResolution(ResSet[target].width, ResSet[target].height, toggle.isOn);
        PlayerPrefs.SetInt("ResID", target);
    }
}
