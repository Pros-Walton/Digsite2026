using UnityEngine;
using UnityEngine.Audio;

public class LoadAll : MonoBehaviour
{
    public AudioMixer mixer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        States.audioMixer = mixer;
        Debug.Log(PlayerPrefs.GetInt("MasterVol", 0));
        States.audioMixer.SetFloat("MasterVol", PlayerPrefs.GetInt("MasterVol", 0));
        States.audioMixer.SetFloat("MusicVol", PlayerPrefs.GetInt("MusicVol", 0));
        States.audioMixer.SetFloat("SFXVol", PlayerPrefs.GetInt("SFXVol", 0));
        States.audioMixer.SetFloat("UIVol", PlayerPrefs.GetInt("UIVol", 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
