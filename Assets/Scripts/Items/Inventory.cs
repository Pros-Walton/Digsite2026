using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Inventory
{
    public List<Weapon> weapons;
    public List<Armour> armours;
    public List<Artifact> artifacts;
    public List<Key> keys;
    public List<Item> items;

    public List<string> weaponsStr = new List<string>();
    public List<string> armoursStr = new List<string>();
    public List<string> artifactsStr = new List<string>();
    public List<string> keysStr = new List<string>();
    public List<string> itemsStr = new List<string>();
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Inventory()
    {
        armours = new List<Armour>();
        weapons = new List<Weapon>();
        artifacts = new List<Artifact>();
        keys = new List<Key>();
        items = new List<Item>();
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
    }

}