using UnityEngine;
using TMPro;

public class FinalTally : MonoBehaviour
{

    public TMP_Text depth;
    public TMP_Text gold;
    public TMP_Text score;
    public TMP_Text total;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        depth.text = "DEPTH: " + States.stats.depth;
        gold.text = "GOLD: " + States.stats.gold;
        score.text = "SCORE: " + States.stats.score;
        total.text = "TOTAL: " + ((int)((((float)(States.stats.depth)/3.0f) + 1) * ((States.stats.gold / 5) + States.stats.score)));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
