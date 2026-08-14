using UnityEngine;
using TMPro;
using System.Collections;


public class SaveButton : MonoBehaviour
{

    public AudioSource sound;

    public string name;
    public string date;

    private TMP_Text nameObj;
    private TMP_Text dateObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nameObj = transform.GetChild(0).GetComponent<TMP_Text>();
        dateObj = transform.GetChild(1).GetComponent<TMP_Text>();

        nameObj.text = name;
        dateObj.text = date;
    }

    // Update is called once per frame
    public void setSave()
    {
        States.saveName = name;
    }

    public void click()
    {
        StartCoroutine(playSound(sound));
    }

    IEnumerator playSound(AudioSource sound)
    {
        sound.Play(0);
        yield return new WaitWhile(() => sound.isPlaying);
        sound.Stop();
    }
}
