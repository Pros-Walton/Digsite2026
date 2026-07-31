using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Audio;

public class Menus : MonoBehaviour
{
    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #endif
            Application.Quit();

    }

    public void Menu()
    {
        States.audioMixer.SetFloat("SFXVol", States.sfxVol);
        StartCoroutine(Hold("Main Menu"));
    }

    public void New()
    {
        States.audioMixer.SetFloat("SFXVol", States.sfxVol);
        States.LoadData = false;
        StartCoroutine(Hold("Loading"));
    }

    public void Load()
    {
        States.audioMixer.SetFloat("SFXVol", States.sfxVol);
        States.LoadData = true;
        StartCoroutine(Hold("Loading"));
    }

    IEnumerator Hold(string scene)
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("Scenes/" + scene);
    }
}
