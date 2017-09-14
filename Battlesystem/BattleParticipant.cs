using System;

public enum BattlePosition
{
    Middle,
    LeftTop,
    LeftBottom,
    RightTop,
    RightBottom
}
[Serializable]
public class BattleParticipant
{
    public int Attack;
    public int Defense;
    public int Speed;
    public int Intellect;
    public int Health;
    public int Mana;
    public int Parry;
    public int Luck;
    public int Reflect;
    public int[] Resistance;
    public int ActionPoints;
    public Condition ParticipantCondition;
    public bool IsReady;
    public BattlePosition Position;
    public string Name;
    public bool DidAction = false;
    public virtual void Update() { }
    public void DealDamage(int damage)
    {
        Health -= damage;
        if (Health > 0) return;
        Health = 0;
        ParticipantCondition = Condition.Dead;
    }

    public bool DrainMana(int mana)
    {
        if (Mana - mana < 0) return false;
        Mana -= mana;
        return true;
    }

    public bool BuffParticipant(Buff b)
    {
        return true;
    }

    public void RefreshBuffs(int i)
    {

    }
}
