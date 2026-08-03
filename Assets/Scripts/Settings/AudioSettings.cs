using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Unity.Mathematics;
using TMPro;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer mixer;

    public GameObject Master;
    public AudioMixerGroup MasterGroup;
    private Slider MasterSlider;
    private TMP_InputField MasterInput;
    private int MasterVal;

    public GameObject Music;
    public AudioMixerGroup MusicGroup;
    private Slider MusicSlider;
    private TMP_InputField MusicInput;
    private int MusicVal;

    public GameObject SFX;
    public AudioMixerGroup SFXGroup;
    private Slider SFXSlider;
    private TMP_InputField SFXInput;
    private int SFXVal;

    public GameObject UI;
    public AudioMixerGroup UIGroup;
    private Slider UISlider;
    private TMP_InputField UIInput;
    private int UIVal;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        States.audioMixer = mixer;
        MasterSlider = Master.transform.GetChild(0).gameObject.GetComponent<Slider>();
        MasterInput = Master.transform.GetChild(1).gameObject.GetComponent<TMP_InputField>();
        
        MusicSlider = Music.transform.GetChild(0).gameObject.GetComponent<Slider>();
        MusicInput = Music.transform.GetChild(1).gameObject.GetComponent<TMP_InputField>();

        SFXSlider = SFX.transform.GetChild(0).gameObject.GetComponent<Slider>();
        SFXInput = SFX.transform.GetChild(1).gameObject.GetComponent<TMP_InputField>();

        UISlider = UI.transform.GetChild(0).gameObject.GetComponent<Slider>();
        UIInput = UI.transform.GetChild(1).gameObject.GetComponent<TMP_InputField>();     

        Load();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SliderChangeMstr()
    {
        SliderChange(MasterSlider,MasterInput);
    }

    public void InputChangeMstr()
    {
        InputChange(MasterSlider,MasterInput);
    }

    public void SliderChangeMus()
    {
        SliderChange(MusicSlider,MusicInput);
    }

    public void InputChangeMus()
    {
        InputChange(MusicSlider,MusicInput);
    }

    public void SliderChangeSFX()
    {
        SliderChange(SFXSlider,SFXInput);
    }

    public void InputChangeSFX()
    {
        InputChange(SFXSlider,SFXInput);
    }

    public void SliderChangeUI()
    {
        SliderChange(UISlider,UIInput);
    }

    public void InputChangeUI()
    {
        InputChange(UISlider,UIInput);
    }


    private void SliderChange(Slider slider, TMP_InputField input)
    {
        input.text = slider.value.ToString();
    }

    private void InputChange(Slider slider, TMP_InputField input)
    {
        int value = 0;
        int.TryParse(input.text, out value);
        value = Mathf.Max(0, value);
        value = Mathf.Min(120,value);
        input.text = value.ToString();
        slider.value = value;
    }

    private void MapValues()
    {
        MasterVal = (int)math.remap(0,120,-80,20,MasterSlider.value);
        MusicVal = (int)math.remap(0,120,-80,20,MusicSlider.value);
        SFXVal = (int)math.remap(0,120,-80,20,SFXSlider.value);
        UIVal =  (int)math.remap(0,120,-80,20,UISlider.value);
    }

    private void DeMapValues()
    {
        MasterSlider.value = (int)math.remap(-80,20,0,120,MasterVal);
        MusicSlider.value = (int)math.remap(-80,20,0,120,MusicVal);
        SFXSlider.value = (int)math.remap(-80,20,0,120,SFXVal);
        UISlider.value = (int)math.remap(-80,20,0,120,UIVal);
    }

    public void Save()
    {
        MapValues();
        PlayerPrefs.SetInt("MasterVol", MasterVal);
        PlayerPrefs.SetInt("MusicVol", MusicVal);
        PlayerPrefs.SetInt("SFXVol", SFXVal);
        PlayerPrefs.SetInt("UIVol", UIVal);
    }

    public void Apply()
    {
        MapValues();
        States.audioMixer.SetFloat("MasterVol", MasterVal);
        States.audioMixer.SetFloat("MusicVol", MusicVal);
        States.audioMixer.SetFloat("SFXVol", SFXVal);
        States.audioMixer.SetFloat("UIVol", UIVal);
        
    }

    public void Load()
    {
        MasterVal = PlayerPrefs.GetInt("MasterVol", 80);
        MusicVal = PlayerPrefs.GetInt("MusicVol", 80);
        SFXVal = PlayerPrefs.GetInt("SFXVol", 80);
        UIVal = PlayerPrefs.GetInt("UIVol", 80);
        DeMapValues();
        Apply();
        
    }
}
