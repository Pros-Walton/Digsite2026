using UnityEngine;
using UnityEngine.UI;

public class SettingsTabs : MonoBehaviour
{
    public GameObject canvasAudio;
    public GameObject canvasVideo;
    private Canvas audioCanvas;
    private Canvas videoCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioCanvas = canvasAudio.GetComponent<Canvas>();
        videoCanvas = canvasVideo.GetComponent<Canvas>();
        
    }

    public void doAudio()
    {
        audioCanvas.enabled = true;
        videoCanvas.enabled = false;
    }

    public void doVideo()
    {
        audioCanvas.enabled = false;
        videoCanvas.enabled = true;
    }
}
