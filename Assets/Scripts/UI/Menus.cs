using UnityEngine;
using UnityEngine.SceneManagement;


public class Menus : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        States.LoadData = false;
        SceneManager.LoadScene("Scenes/Loading");
    }

    public void Load()
    {
        States.LoadData = true;
        SceneManager.LoadScene("Scenes/Loading");
    }
}
