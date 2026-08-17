using UnityEngine;

public class CanvasToggle : MonoBehaviour
{
    public GameObject toggle;
    private Canvas canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = toggle.GetComponent<Canvas>();
    }
    public void Toggle()
    {
        canvas.enabled = !canvas.enabled;
    }
}
