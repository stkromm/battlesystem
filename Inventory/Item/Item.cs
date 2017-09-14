using System;
using UnityEngine;
public enum Rarity { Common, Rare, Epic, Legendary, Artifical };

[Serializable]
public class Item
{
    // Should be short, doesnt work in unity however
    public int Stack = 99;
    public bool Unique;
    public int Value;
    public string Name;
    public string Description;
    public int buffId;
}
