using UnityEngine;
using UnityEngine.SceneManagement;


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
        SceneManager.LoadScene("Scenes/Main Menu");
    }

    public void New()
    {
        Time.timeScale = 1;
        States.LoadData = false;
        SceneManager.LoadScene("Scenes/Loading");
    }

    public void Load()
    {
        Time.timeScale = 1;
        States.LoadData = true;
        SceneManager.LoadScene("Scenes/Loading");
    }
}
