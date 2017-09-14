
[System.Serializable]
public class UsableItem : Item
{
    public int Heal;
    public int Refresh;
    public Condition Condition;
    public override string ToString()
    {
        var s = "\n";
        if (Heal != 0)
            s += "Heals" + Heal + "\n";
        if (Refresh != 0)
            s += "Refreshs" + Refresh + "\n";
        if (Condition != 0)
            s += "Cure" + Condition + "\n";
        return s;
    }
}
