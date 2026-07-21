using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

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

    private void OnEnable()
    {
        InputActionGroups.FindActionMap("Screens").Enable();

        inventoryCanvas = GameObject.Find("InventoryScreen").GetComponent<Canvas>();
        pauseMenu = GameObject.Find("PauseMenu").GetComponent<Canvas>();
        uiManager = GameObject.Find("EventSystem").GetComponent<Manager>();
        readout = GameObject.Find("Inventory Readout").GetComponent<ReadoutPanel>();

        actionInventory = InputSystem.actions.FindAction("Inventory");
        actionPause = InputSystem.actions.FindAction("Pause");
    }

    private void OnDisable()
    {
        InputActionGroups.FindActionMap("Screens").Disable();
    }

    // Update is called once per frame
    void Update()
    {
        inventoryCheck();

        if (actionPause.WasPressedThisFrame())
        {
            pause();
        }  
    }

    public void Save()
    {
        States.stats.applyTo();
        States.inventory.applyTo();
        States.stats.pos = transform.position;

        States.tileData.Clear();
        foreach (Tile curTile in States.tiles)
        {
            string serial = JsonUtility.ToJson(curTile);
            States.tileData.Add(serial);
        }
        States.leveldata.data = States.tileData;

        string statString = JsonUtility.ToJson(States.stats);
        string inventoryString = JsonUtility.ToJson(States.inventory);
        string levelString = JsonUtility.ToJson(States.leveldata);

        string playerPath = Application.persistentDataPath + "/Save/player.json";
        string invenPath = Application.persistentDataPath + "/Save/inventory.json";
        string levelPath = Application.persistentDataPath + "/Save/level.json";

        if(!File.Exists(playerPath))
        {
            File.Create(playerPath);
        }

        if(!File.Exists(invenPath))
        {
            File.Create(invenPath);
        }

        if(!File.Exists(levelPath))
        {
            File.Create(levelPath);
        }

        File.WriteAllText(playerPath,statString);
        File.WriteAllText(invenPath,inventoryString);
        File.WriteAllText(levelPath,levelString);

    }

    private void inventoryCheck()
    {
        if (actionInventory.WasPressedThisFrame())
        {
            if (Time.timeScale == 1)
            {
                readout.clearDetails();
                uiManager.clearButtons();
                uiManager.doArmour();
                inventoryCanvas.enabled = true;
                Time.timeScale = 0;
            }
            else if (Time.timeScale == 0)
            {
                readout.clearDetails();
                uiManager.clearButtons();
                inventoryCanvas.enabled = false;
                Time.timeScale = 1;
            }
        }
    }

    public void pause()
    {
            if (Time.timeScale == 1)
            {
                pauseMenu.enabled = true;
                Time.timeScale = 0;
            }
            else if (Time.timeScale == 0)
            {
                pauseMenu.enabled = false;
                Time.timeScale = 1;
            }

    }
}
