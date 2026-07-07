using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerStats : MonoBehaviour
{
    public int health_max;
    public float health;

    public int stamina_max;
    public float stamina;

    public int gold;
    public int score;
    public int depth;
    public Weapon weapon;
    public Armour armour;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        Debug.Log("Am I even here?");
        weapon = new Weapon();
        armour = new Armour();
        armour.isCloth();
        weapon.isTrowel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        
        SceneManager.LoadScene("Scenes/GameOver");
    }
}
