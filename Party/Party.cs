public class Party : MyMonoBehaviour
{
    public Character[] _groupParty = new Character[1];
    public int ActiveChar = 0;

    public int GroupLength()
    {
        return _groupParty != null ? _groupParty.Length : 0;
    }

    public void SetParty(Character[] newParty)
    {
        _groupParty = newParty;
    }
    public Character GetCharacterInParty(int index)
    {
        if (index >= 0 && index < _groupParty.Length)
        {
            return _groupParty[index];
        }
        return null;
    }
    public void ChangeActiveCharacter()
    {
        do
        {
            ActiveChar = ActiveChar < _groupParty.Length - 1 ? ActiveChar + 1 : 0;
        } while (!_groupParty[ActiveChar].InParty && _groupParty != null);
    }
}
