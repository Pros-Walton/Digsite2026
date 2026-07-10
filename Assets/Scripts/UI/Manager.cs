using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    public GameObject player;
    private PlayerStats stats;

    public TMP_Text ui_health;
    public TMP_Text ui_stamina;
    public TMP_Text ui_score;
    public TMP_Text ui_gold;
    public TMP_Text ui_depth;

    public Button btn_armour;

    private Inventory inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats = player.GetComponent<PlayerStats>();
        ui_depth.text = stats.depth.ToString();
        btn_armour.Select();
    }

    // Update is called once per frame
    void Update()
    {
        ui_health.text = (((int)stats.health).ToString() + "/" + stats.health_max.ToString());
        ui_stamina.text = (((int)stats.stamina).ToString() + "/" + stats.stamina_max.ToString());
        ui_gold.text = stats.gold.ToString();
        ui_score.text = stats.score.ToString();
    }

    public void giveInventory(Inventory inventoryGive)
    {
        inventory = inventoryGive;
    }

    public void doArmour()
    {
        Debug.Log("now show armour!");
        foreach (Armour arm in inventory.armours)
        {
            Debug.Log(arm.name);
        }
    }

}
