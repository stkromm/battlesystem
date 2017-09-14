using System;

public enum Elemental
{
    Dark, Fire, Water, Air, Earth, Sky, Hate
}
public enum AttackAttribute
{
    Peak, Smash, Burst, Various
}
public enum AttackTyp
{
    Soft, Hard, Magic
}
public enum AttackRange
{
    Area, Single, Side, Party, EnemyParty
}
[Serializable]
public class Skill
{
    public int Damage;
    public int Cost;
    public int Crit;
    public AttackAttribute Attribute;
    public Elemental Elemental;
    public Condition Condition;
    public AttackTyp Typ;
    public AttackRange Range;
    public int ConditionChangeRate;
    public string Animation;
    public string Name;
    public int Actioncost;
}
