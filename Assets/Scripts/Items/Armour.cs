using UnityEngine;

public class Armour
{
    public string name;
    public int defense;
    public int durability;
    public float used;
    public float damageRate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    public void use()
    {
        used += damageRate;
    }

    public bool shouldBreak()
    {
        return used >= durability;
    }
}

public class cloth : Armour
{
        public string name = "Cloth";
        public int defense = 1;
        public int durability = 0;
        public float used = -1.0f;
        public float damageRate = 0.0f;

}
