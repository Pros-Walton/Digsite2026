using UnityEngine;
using System.Collections.Generic;


public class Inventory
{
    public List<Weapon> weapons;
    public List<Armour> armours;
    public List<Artifact> artifacts;
    public List<Key> keys;
    public List<Item> items;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Inventory()
    {
        armours = new List<Armour>();
        weapons = new List<Weapon>();
        artifacts = new List<Artifact>();
        keys = new List<Key>();
        items = new List<Item>();
    }

}