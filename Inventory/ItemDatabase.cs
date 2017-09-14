using System;
using UnityEngine;
public static class ItemDatabase
{
    private static Item[] _instance;

    public static Item[] Items()
    {
        return _instance ?? (_instance = XmlManager.LoadXmlItemDatabase());
    }

    public static string GetItemName(int i)
    {
        if (Items().Length > i && i >= 0)
        {
            return Items()[i].Name;
        }
        return "-";
    }
    public static int GetHeal(int i)
    {
        if (!(Items().Length > i && i >= 0 && Items()[i] != null && (Items()[i].GetType() == typeof(UsableItem))))
        {
            return 0;
        }
        var e = (UsableItem)Items()[i];
        return e.Heal;
    }
    public static int GetRefresh(int i)
    {
        if (!(Items().Length > i && i >= 0 && Items()[i] != null && (Items()[i].GetType() == typeof(UsableItem))))
        {
            return 0;
        }
        var e = (UsableItem)Items()[i];
        return e.Refresh;
    }
    public static int GetItemSlot(int i)
    {
        if (!(Items().Length > i && i >= 0 && Items()[i] != null && (Items()[i].GetType() == typeof(EquipableItem))))
        {
            return (int)ArmorSlot.None;
        }
        var e = (EquipableItem)Items()[i];
        return (int)e.Slot;
    }
    public static int GetItemStackSize(int i)
    {
        if (i >= 0 && i < Items().Length)
            return Items()[i].Stack;
        return 0;
    }
    public static System.Type GetTypeOfItem(int a)
    {
        return Items()[a].GetType();
    }

    public static Buff GetItemBuff(int a)
    {
        return null;
    }
    public static string GetItemInfo(int a)
    {
        if (a >= 0 && a < Items().Length && Items()[a] != null)
        {
            return Items()[a].ToString();
        }
        return "";
    }

    public static int[] GetStats(int i)
    {
        if (!(Items().Length > i && i >= 0 && Items()[i] != null && (Items()[i].GetType() == typeof(EquipableItem))))
        {
            return new int[9];
        }
        var e = (EquipableItem)Items()[i];
        return e.Stats;
    }
}
