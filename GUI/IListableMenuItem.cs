
using UnityEngine;
enum ListableMenuItemType { Skill, Buff, Item }
public class ListableMenuItem
{
    public string Info;
    public string Name;
    public string AdditionalNameInfo;
    private ListableMenuItemType _type;
    private readonly int _id;

    public ListableMenuItem(Skill s)
    {
        Info = s.ToString();
        Name = s.Name;
    }

    public ListableMenuItem(Buff b)
    {
        Info = b.ToString();
        Name = b.Name;
    }

    public ListableMenuItem(int item)
    {
        var o = GameObject.Find("Inventory");
        _id = item;
        if (o == null) return;
        Info = ItemDatabase.GetItemInfo(item);
        Name = ItemDatabase.GetItemName(item);
        _type = ListableMenuItemType.Item;
    }

    public bool Use()
    {
        switch (_type)
        {
            case ListableMenuItemType.Item:
                Debug.Log("Use Item");
                var o = GameObject.Find("Inventory");
                var p = GameObject.Find("Party");
                if (o == null && p == null)
                    return false;
                var party = p.GetComponent<Party>();
                Debug.Log("Use Item Real");
                return o.GetComponent<Inventory>()
                    .UseItem(_id, party.ActiveChar);

            default:
                return false;
        }
    }
}
