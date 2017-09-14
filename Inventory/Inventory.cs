using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Inventory : MyMonoBehaviour, IInventoryInterface
{
    //Inventory size
    private const int AmountOfItemsCap = 99;
    private int _gold;
    private const int GoldCap = 1000000;
    private readonly Dictionary<int, int> _itemList = new Dictionary<int, int>();
    public int AmountOfItems { get { return _itemList.Count; } }
    public int[] GetItemListKeys
    {
        get
        {
            var a = new int[AmountOfItems];
            _itemList.Keys.CopyTo(a, 0);
            return a;
        }
    }
    public int gold
    {
        get
        {
            return _gold;
        }
    }
    //
    public bool AddAmountofMoney(int i)
    {
        try
        {
            if (_gold + i > GoldCap || _gold + i < 0)
            {
                return false;
            }
            _gold += i;
            return true;
        }
        catch (OverflowException)
        {
            return false;
        }
    }

    public bool AddAmountofItem(int item, int number)
    {
        if (!ContainsItem(item) && AmountOfItems == AmountOfItemsCap)
        {
            return false;
        }
        int a;
        _itemList.TryGetValue(item, out a);
        if (a + number < 0 || (a + number >= ItemDatabase.GetItemStackSize(item)))
        {
            return false;
        }
        _itemList.Remove(item);
        if (a + number != 0)
        {
            _itemList.Add(item, a + number);
        }
        return true;
    }

    public bool ContainsItem(int i)
    {
        return _itemList.ContainsKey(i);
    }

    public int GetItemCount(int i)
    {
        int a;
        _itemList.TryGetValue(i, out a);
        return a;
    }

    public bool UseItem(int a, int index)
    {
        var o = GameObject.Find("Party");
        if (o == null) return false;
        var p = o.GetSafeComponent<Party>();
        var ch = p.GetCharacterInParty(index);
        if (!ContainsItem(a) || ItemDatabase.GetTypeOfItem(a) != typeof(UsableItem) || ch.BuffCharacter(ItemDatabase.GetItemBuff(a)) ||
              ch.ChangeHealth(ItemDatabase.GetHeal(a)) || ch.ChangeMana(ItemDatabase.GetRefresh(a)))
        {
        }
        if (ItemDatabase.GetTypeOfItem(a) == typeof(EquipableItem))
        {
            ch.EquipSlotWithItem(ItemDatabase.GetItemSlot(a), a);
            return true;
        }
        AddAmountofItem(a, -1);
        return true;
    }
}