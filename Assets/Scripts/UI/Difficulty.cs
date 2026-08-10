using UnityEngine;

public class Difficulty : MonoBehaviour
{
    private Canvas canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GameObject.Find("DifficultySelector").GetComponent<Canvas>();
    }
    public void Toggle()
    {
        canvas.enabled = !canvas.enabled;
    }
}
