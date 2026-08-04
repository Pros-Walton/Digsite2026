using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Canvas canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GameObject.Find("TutorialCanvas").GetComponent<Canvas>();
    }
    public void Toggle()
    {
        canvas.enabled = !canvas.enabled;
    }
}
