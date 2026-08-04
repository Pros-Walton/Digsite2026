using UnityEngine;
using System.Collections.Generic;


public class Pickup
{
    int lootSize;

    public int loc;

    public int id;

    public int gold;

    List<Weapon> weapons = new List<Weapon>();
    List<Armour> armours = new List<Armour>();
    List<Artifact> artifacts = new List<Artifact>();
    List<Key> keys = new List<Key>();
    List <Item> items = new List<Item>();

    public List<string> weaponsStr = new List<string>();
    public List<string> armoursStr = new List<string>();
    public List<string> artifactsStr = new List<string>();
    public List<string> keysStr = new List<string>();
    public List<string> itemsStr = new List<string>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Pickup()
    {
    }

    public void populate(int location, int ID)
    {
        loc = location;
        lootSize = UnityEngine.Random.Range(5,10);
        id = ID;

        gold = UnityEngine.Random.Range(5,100);

        switch (id)
        {
            case 0:
                for (int i = 0; i < lootSize; i++)
                {
                    int typeSelector = UnityEngine.Random.Range(0,10);

                    switch(typeSelector)
                    {
                        case 3:
                            weapons.Add(new Weapon(UnityEngine.Random.Range(1,3)));
                            break;
                        case 4:
                            armours.Add(new Armour(UnityEngine.Random.Range(1,2)));
                            break;
                        default:
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
                            break;
                    }
                }
                break;
        }
    }

    public void GiveLoot()
    {
        States.stats.gold += gold;

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

        public void applyTo()
    {
        weaponsStr.Clear();
        armoursStr.Clear();
        artifactsStr.Clear();
        keysStr.Clear();
        itemsStr.Clear();

        foreach (Weapon weapon in weapons)
        {
            string serialisedWep = JsonUtility.ToJson(weapon);
            weaponsStr.Add(serialisedWep);
        }
        foreach (Armour armour in armours)
        {
            string serialisedArm = JsonUtility.ToJson(armour);
            armoursStr.Add(serialisedArm);
        }
        foreach (Artifact artifact in artifacts)
        {
            string serialisedArt = JsonUtility.ToJson(artifact);
            artifactsStr.Add(serialisedArt);
        }
        foreach (Key key in keys)
        {
            string serialisedKey = JsonUtility.ToJson(key);
            keysStr.Add(serialisedKey);
        }
        foreach (Item item in items)
        {
            string serialisedItem = JsonUtility.ToJson(item);
            itemsStr.Add(serialisedItem);
        }
    }

    public void applyBack()
    {
        foreach (string strWeapon in weaponsStr)
        {
            weapons.Add(JsonUtility.FromJson<Weapon>(strWeapon));
        }
        foreach (string strArmour in armoursStr)
        {
            armours.Add(JsonUtility.FromJson<Armour>(strArmour));
        }
        foreach (string strArtifact in artifactsStr)
        {
            artifacts.Add(JsonUtility.FromJson<Artifact>(strArtifact));
        }
        foreach (string strKey in keysStr)
        {
            keys.Add(JsonUtility.FromJson<Key>(strKey));
        }
        foreach (string strItem in itemsStr)
        {
            items.Add(JsonUtility.FromJson<Item>(strItem));
        }

        weaponsStr.Clear();
        armoursStr.Clear();
        artifactsStr.Clear();
        keysStr.Clear();
        itemsStr.Clear();
    }
}
