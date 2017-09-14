[System.Serializable]
public class EquipableItem : Item
{
    public int[] Stats = new int[9];
    public ArmorSlot Slot;
    public override string ToString()
    {
        var lang = new Language();
        var s = "";
        s += Name + "\n"; if (Description != "")
            s += "'" + Description + "'\n";
        for (var i = 0; i < Stats.Length; i++)
        {
            if (i < lang.PropItems.Length)
                s += lang.PropItems[i] + "\t" + Stats[i] + "\n";
        }
        s += lang.PropItems[10] + Slot + "\n";
        return s;
    }
}
