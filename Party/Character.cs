using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public enum ArmorSlot
{
    None,
    Head,
    Chest,
    Shoes,
    Back,
    Trinket,
    Waist,
    Weapon
}
public enum Condition { Normal, Poision, Cold, Heat, Stunned, Blind, Cursed, Paralysed, Wounded, Dead }
public class Character : MyMonoBehaviour
{
    // All public, because they are assigned through the inspector.
    public Attribute[] Attributes;
    public int Exp = 0;
    public int ExpToLevelUp = 100;
    private const int ExpCap = 1000000;
    private const int NoneArmor = -1;
    private const int MaxLevel = 100;
    public int Level = 1;
    public string CharacterName = "";
    public int[] Skills;
    public Dictionary<int, bool> _skills = new Dictionary<int, bool>();
    public bool[] Learned;
    public readonly Dictionary<string, Buff> Buffs = new Dictionary<string, Buff>();
    public Texture FaceTex;
    // Length is the length of ArmorSlot enum.
    public int[] Armor = { NoneArmor, NoneArmor, NoneArmor, NoneArmor, NoneArmor, NoneArmor, NoneArmor, NoneArmor };
    public int ArmorSlots = Enum.GetNames((typeof(ArmorSlot))).Length;
    public int Condition;
    // Determines if the Character is playable atm.
    public bool InParty;
    public bool InCombatParty;

    public bool RefreshBuffCounter(int i)
    {
        var changed = false;
        if (i <= 0)
        {
            return false;
        }
        foreach (var b in Buffs.Values)
        {
            b.Duration = b.Duration - i;
            if (b.Duration <= 0)
                Buffs.Remove(b.Name);
            UpdateBuffStats();
            changed = true;
        }
        return changed;
    }

    public void AddExp(int e)
    {
        try
        {
            Exp = Exp + e;
        }
        catch (OverflowException)
        {
            Exp = ExpCap;
        }
        Exp = Exp > ExpCap ? ExpCap : Exp;
        while (IsLevelingUp())
        {
            // Level Up Announcement
        }
    }

    public bool ChangeCondition(int i)
    {
        if (Condition == i)
        {
            return false;
        }
        Condition = i;
        return true;
    }

    public bool ChangeHealth(int i)
    {
        return Attributes[0].TakeDamage(i, Level);
    }

    public bool ChangeMana(int i)
    {
        return Attributes[1].TakeDamage(i, Level);
    }

    public bool BuffCharacter(Buff buff)
    {
        if (buff == null)
        {
            return false;
        }

        if (Buffs.ContainsKey(buff.Name))
        {
            Buffs.Remove(buff.Name);
        }
        Buffs.Add(buff.Name, buff);
        UpdateBuffStats();
        return true;
    }

    public void EquipSlotWithItem(int slot, int e)
    {
        var inv = GameObject.Find("Inventory").GetSafeComponent<Inventory>();
        if (DeequipItemInSlot(slot) && e != 0 && inv.AddAmountofItem(e, -1))
            Armor[ItemDatabase.GetItemSlot(e)] = e;
        UpdateEquipmentStats();
    }

    private bool DeequipItemInSlot(int piece)
    {
        var o = GameObject.Find("Inventory");
        if (o == null)
        {
            return false;
        }
        var inv = o.GetSafeComponent<Inventory>();
        if (Armor[piece] != NoneArmor && !inv.AddAmountofItem(Armor[piece], 1))
        {
            return false;
        }
        Armor[piece] = NoneArmor;
        return true;
    }

    private bool IsLevelingUp()
    {
        if (Exp < ExpToLevelUp)
        {
            return false;
        }
        LevelUp();
        return true;
    }

    private void LevelUp()
    {
        if (Level >= MaxLevel)
        {
            return;
        }
        Level++;
        Exp = Exp - ExpToLevelUp;
        // Calculate the needed experience
        ExpToLevelUp = Mathf.CeilToInt(Mathf.Exp(Level * 2) * Mathf.Exp(1f / Level));
    }

    private void UpdateEquipmentStats()
    {
        var inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        for (var u = 0; u < Attributes.Length; u++)
        {
            Attributes[u].AdditionalArmorStats = (from i in Armor where i != 0 select i).Sum(i => ItemDatabase.GetStats(i)[u]);
        }
    }

    private void UpdateBuffStats()
    {
        for (var u = 0; u < Attributes.Length; u++)
        {
            Attributes[u].AdditionalBuffStats = (from i in Buffs.Values where (u < i.Stats.Length && i.Stats[u] != 0) select i).Sum(b => b.Stats[u]);
        }
    }
}