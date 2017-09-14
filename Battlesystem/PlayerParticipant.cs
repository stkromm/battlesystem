public class PlayerParticipant : BattleParticipant
{
    public Character Ch;
    public PlayerParticipant(Character c)
    {
        Health = (int)c.Attributes[0].GetActualValue(c.Level);
        Mana = (int)c.Attributes[1].GetActualValue(c.Level);
        Attack = (int)c.Attributes[2].GetActualValue(c.Level);
        ActionPoints = 900;
        ParticipantCondition = (Condition)c.Condition;
        IsReady = false;
        Name = c.CharacterName;
        Ch = c;
    }
}
