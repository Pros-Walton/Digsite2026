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

    public void New(float diffMult)
    {
        States.diffMult = diffMult;
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

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    public void CloseSettings()
    {
        SceneManager.UnloadSceneAsync("Scenes/Settings");
    }

    IEnumerator Hold(string scene)
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("Scenes/" + scene);
    }

    public void OpenSaves()
    {
        SceneManager.LoadScene("Saves", LoadSceneMode.Additive);
    }

    public void CloseSaves()
    {
        SceneManager.UnloadSceneAsync("Scenes/Saves");

    }
}
