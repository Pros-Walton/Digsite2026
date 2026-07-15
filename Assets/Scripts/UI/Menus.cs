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
        if(Application.isEditor)
        {
            //UnityEditor.EditorApplication.ExitPlaymode();
        }
        else
        {
            Application.Quit();
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("Scenes/Main Menu");
    }

    public void New()
    {
        SceneStates.LoadData = false;
        SceneManager.LoadScene("Scenes/Loading");
    }

    public void Load()
    {
        SceneStates.LoadData = true;
        SceneManager.LoadScene("Scenes/Loading");
    }
}
