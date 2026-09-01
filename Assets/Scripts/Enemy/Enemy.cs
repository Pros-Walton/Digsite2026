using UnityEngine;

public class Enemy
{

    public GameObject playerTarget;
    private Rigidbody body;
    public GameObject enemyObj;

    public float attackCooldown = 0.0f;
    public float cooldownMax;

    public int attack;
    public int defense;

    public int maxHP;
    public float HP;

    public int locID;
    public int id;

    public enum weaponType {melee, ranged};
    public weaponType type;

    public Enemy()
    {
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void populate(int loc, int ID)
    {
        locID = loc;
        id = ID;

        switch(id)
        {
            case 0:
                maxHP = 10;
                attack = 1;
                defense = 1;
                type = weaponType.melee;
                cooldownMax = 3.0f;
                break;
            case 1:
                maxHP = 15;
                attack = 2;
                defense = 1;
                type = weaponType.melee;
                cooldownMax = 3.0f;
                break;
            case 2:
                maxHP = 15;
                attack = 3;
                defense = 2;
                type = weaponType.ranged;
                cooldownMax = 5.0f;
                break;
        }

        HP = maxHP;
    }

    public void getBody()
    {
        body = enemyObj.GetComponent<Rigidbody>();
    }
}
