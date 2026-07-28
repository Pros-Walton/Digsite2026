using UnityEngine;
using System.Collections;

public class UISounds : MonoBehaviour
{
    AudioSource[] Sounds;

    void Start()
    {
        Sounds = GetComponents<AudioSource>();
    }

    public void click()
    {
        StartCoroutine(playSound(Sounds[0]));
    }

    IEnumerator playSound(AudioSource sound)
    {
        sound.Play(0);
        yield return new WaitWhile(() => sound.isPlaying);
        sound.Stop();
    }
}