using UnityEngine;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats = player.GetComponent<PlayerStats>();
        ui_depth.text = stats.depth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        ui_health.text = (stats.health.ToString() + "/" + stats.health_max.ToString());
        ui_stamina.text = (stats.stamina.ToString() + "/" + stats.stamina_max.ToString());
        ui_gold.text = stats.gold.ToString();
        ui_score.text = stats.score.ToString();
    }
}
