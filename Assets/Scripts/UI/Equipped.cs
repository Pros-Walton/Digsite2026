using UnityEngine;
using UnityEngine.UI;

public class Equipped : MonoBehaviour
{
    public Image armour;
    public Image weapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        armour.sprite = States.stats.armour.icon;
        weapon.sprite = States.stats.weapon.icon;
    }
}
