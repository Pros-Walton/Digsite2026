using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoSettings : MonoBehaviour
{
    public Toggle toggle;
    public TMP_Dropdown dropdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toggle.isOn =  Screen.fullScreen;
        dropdown.value = PlayerPrefs.GetInt("ResID", 0);
        
    }

    public void Apply()
    {
        Screen.fullScreen = toggle.isOn;
        switch (dropdown.value)
        {
            case 0:
                Screen.SetResolution(1920,1080, toggle.isOn);
                PlayerPrefs.SetInt("ResID", 0);
                break;
            case 1:
                Screen.SetResolution(2560,1440, toggle.isOn);
                PlayerPrefs.SetInt("ResID", 1);
                break;
            case 2:
                Screen.SetResolution(3840,2160, toggle.isOn);
                PlayerPrefs.SetInt("ResID", 2);
                break;
                
        }
    }
}
