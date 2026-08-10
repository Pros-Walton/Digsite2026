using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerStats
{
    public Vector3 pos;

    public int health_max;
    public float health;

    public int stamina_max;
    public float stamina;

    public int ammo;

    public int gold;
    public int score;
    public int depth;
    public string weapon_str;
    public string armour_str;
    public Weapon weapon;
    public Armour armour;

    public float diffMult;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public PlayerStats()
    {
        pos = new Vector3(0,0.4f,0);
        health = 30;
        health_max = 30;
        stamina = 30;
        stamina_max = 30;
        ammo = 0;
        weapon = new Weapon(0);
        armour = new Armour(0);
        diffMult = States.diffMult;
    }

    public void GameOver()
    {
        
        SceneManager.LoadScene("Scenes/GameOver");
    }

    public void applyTo()
    {
        weapon_str = JsonUtility.ToJson(weapon);
        armour_str = JsonUtility.ToJson(armour);
    }

    public void applyBack()
    {
        weapon = JsonUtility.FromJson<Weapon>(weapon_str);
        armour = JsonUtility.FromJson<Armour>(armour_str);
    }
}
