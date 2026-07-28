using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


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
        StartCoroutine(Hold("Main Menu"));
    }

    public void New()
    {
        States.LoadData = false;
        StartCoroutine(Hold("Loading"));
    }

    public void Load()
    {
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
