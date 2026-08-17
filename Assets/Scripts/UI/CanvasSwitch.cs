using UnityEngine;

public class CanvasSwitch : MonoBehaviour
{

    public GameObject Canvas1;
    public GameObject Canvas2;

    private Canvas canvasA;
    private Canvas canvasB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasA = Canvas1.GetComponent<Canvas>();
        canvasB = Canvas2.GetComponent<Canvas>();
        canvasA.enabled = !States.toggleSaveMenu;
        canvasB.enabled = States.toggleSaveMenu;
        
    }

    // Update is called once per frame
    public void toggle()
    {
        canvasA.enabled = !canvasA.enabled;
        canvasB.enabled = !canvasB.enabled;
    }
}
