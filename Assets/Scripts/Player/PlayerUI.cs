using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class PlayerUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public InputActionAsset InputActionGroups;

    private InputAction actionInventory;
    private InputAction actionPause;

    private Canvas inventoryCanvas;
    private Manager uiManager;
    private ReadoutPanel readout;
    private Canvas pauseMenu;

    private bool inPause;
    private bool inInventory;

    public AudioMixer audioMixer;
    public AudioMixerGroup sfx;

    private void OnEnable()
    {
        InputActionGroups.FindActionMap("Screens").Enable();

        inventoryCanvas = GameObject.Find("InventoryScreen").GetComponent<Canvas>();
        pauseMenu = GameObject.Find("PauseMenu").GetComponent<Canvas>();
        uiManager = GameObject.Find("EventSystem").GetComponent<Manager>();
        readout = GameObject.Find("Inventory Readout").GetComponent<ReadoutPanel>();

        actionInventory = InputSystem.actions.FindAction("Inventory");
        actionPause = InputSystem.actions.FindAction("Pause");
        States.audioMixer = audioMixer;
        float sfxVol = 0;
        States.audioMixer.GetFloat("SFXVol", out sfxVol);
        States.sfxVol = sfxVol;


    }

    private void OnDisable()
    {
        InputActionGroups.FindActionMap("Screens").Disable();
    }

    // Update is called once per frame
    void Update()
    {
        inventoryCheck();

        if (actionPause.WasPressedThisFrame() && !inInventory)
        {
            pause();
        }  
    }

    private void inventoryCheck()
    {
        if (actionInventory.WasPressedThisFrame() && !inPause)
        {
            if (!inInventory)
            {
                States.audioMixer.SetFloat("SFXVol", -80.0f);
                readout.clearDetails();
                uiManager.clearButtons();
                uiManager.doArmour();
                inventoryCanvas.enabled = true;
                Time.timeScale = 0;
            }
            else
            {
                States.audioMixer.SetFloat("SFXVol", States.sfxVol);
                readout.clearDetails();
                uiManager.clearButtons();
                inventoryCanvas.enabled = false;
                Time.timeScale = 1;
            }
            inInventory = !inInventory;
        }
    }

    public void pause()
    {
            if (!inPause)
            {
                States.audioMixer.SetFloat("SFXVol", -80.0f);
                pauseMenu.enabled = true;
                Time.timeScale = 0;
            }
            else
            {
                States.audioMixer.SetFloat("SFXVol", States.sfxVol);
                pauseMenu.enabled = false;
                Time.timeScale = 1;
            }
            inPause = !inPause;

    }
}
