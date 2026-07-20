using UnityEngine;
using static Inventory;

public static class States
{
    public static bool LoadData {get; set;}

    public static Inventory inventory {get; set;}
    public static PlayerStats stats {get; set;}

    public static string itemName {get; set;}
    public static string itemDesc {get; set;}
    public static Sprite itemIcon {get; set;}

    public static int armDef {get; set;}
    public static int armCurUse {get; set;}
    public static int armDur {get;set;}

    public static int wepAtk {get; set;}
    public static int wepCurUse {get; set;}
    public static int wepDur {get;set;}

    public static string artLore {get; set;}
    public static int artCount {get; set;}

    public static string itemAffect {get; set;}
    public static int itemCount {get; set;}
}
