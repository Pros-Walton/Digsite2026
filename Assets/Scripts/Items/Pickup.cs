using UnityEngine;
using System.Collections.Generic;


public class Pickup : MonoBehaviour
{
    int lootSize;

    List<Weapon> weapons = new List<Weapon>();
    List<Armour> armours = new List<Armour>();
    List<Artifact> artifacts = new List<Artifact>();
    List<Key> keys = new List<Key>();
    List <Item> items = new List<Item>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lootSize = UnityEngine.Random.Range(5,10);

        for (int i = 0; i < lootSize; i++)
        {
            int typeSelector = UnityEngine.Random.Range(0,10);

            switch(typeSelector)
            {
                case 3:
                    weapons.Add(new Weapon(UnityEngine.Random.Range(1,2)));
                    break;
                case 4:
                    armours.Add(new Armour(UnityEngine.Random.Range(1,2)));
                    break;
                default:
                    generateLoot();
                    break;
            }
        }
    }

    private void generateLoot()
    {
        int itemType = UnityEngine.Random.Range(0,3);
        switch(itemType)
        {    
            case 0:
                Artifact art = new Artifact(UnityEngine.Random.Range(0,0));
                artifacts.Add(art);
                break;
            case 1:
                Item item = new Item(UnityEngine.Random.Range(0,2));
                items.Add(item);
                break;
        }
    }

    public void GiveLoot()
    {

        foreach (Weapon weapon in weapons)
        {
            if (States.inventory.weapons.Count < 15)
            {
                States.inventory.weapons.Add(weapon);
            }
        }
        foreach (Armour armour in armours)
        {
            if (States.inventory.armours.Count < 15)
            {
                States.inventory.armours.Add(armour);
            }
        }


        foreach (Artifact artifact in artifacts)
        {
            States.stats.score += artifact.score;
            bool isHere = false;
            foreach(Artifact arti in States.inventory.artifacts)
            {
                if (artifact.name == arti.name)
                {
                    isHere = true;
                    arti.count ++;
                }
            }
            if (!isHere)
            {
                States.inventory.artifacts.Add(artifact);
            }
        }

        foreach (Item item in items)
        {
            bool isHere = false;
            foreach(Item tem in States.inventory.items)
            {
                if (item.name == tem.name)
                {
                    isHere = true;
                    tem.count ++;
                }
            }
            if (!isHere)
            {
                States.inventory.items.Add(item);
            }
        }

    }
}
