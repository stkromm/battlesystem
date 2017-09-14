using System;
using UnityEngine;

public class SaveStateManager : MyMonoBehaviour
{
    public static void SaveGameState(int slot)
    {
        var o = GameObject.Find("Inventory");
        var inv = o.GetComponent<Inventory>();
        o = GameObject.Find("Party");
        var party = o.GetComponent<Party>();
        // Inventory
        // Money
        PlayerPrefs.SetInt(slot + "x" + "money", inv.gold);
        // Item Counter
        // Usable Items
        for (var i = 0; i < inv.AmountOfItems; i++)
        {
            PlayerPrefs.SetInt(slot + "x" + i + "u", inv.GetItemCount(i));
        }
        // GroupParty
        for (var p = 0; p < party.GroupLength(); p++)
        {
            // Armor
            for (var i = 0; i < Enum.GetNames((typeof(ArmorSlot))).Length; i++)
            {
                PlayerPrefs.SetInt(slot + "x" + p + "n" + i + "Armor", party.GetCharacterInParty(p).Armor[i]);
            }
            // Is in GroupParty
            var inParty = (party.GetCharacterInParty(p).InParty) ? 1 : 0;
            PlayerPrefs.SetInt(slot + "x" + p + "GroupParty", inParty);
            // Level
            PlayerPrefs.SetInt(slot + "x" + p + "Level", party.GetCharacterInParty(p).Level);
            // Exp
            PlayerPrefs.SetInt(slot + "x" + p + "Exp", party.GetCharacterInParty(p).Exp);
            // Exp 2 Level up	
            PlayerPrefs.SetInt(slot + "x" + p + "exp2l", party.GetCharacterInParty(p).ExpToLevelUp);
            // ParticipantCondition
            PlayerPrefs.SetInt(slot + "x" + p + "ParticipantCondition", party.GetCharacterInParty(p).Condition);
        }
        // GameSettings
        // Application Infos
        PlayerPrefs.SetString(slot + "CurrentLevel", Application.loadedLevelName);
        // Player Position
        o = GameObject.Find("Hero");
        if (o != null)
        {
            var transform = o.GetComponent<Transform>();
            PlayerPrefs.SetFloat(slot + "xCo", transform.transform.position.x);
            PlayerPrefs.SetFloat(slot + "yCo", transform.transform.position.y);
            PlayerPrefs.SetFloat(slot + "zCo", transform.transform.position.z);
            // Player Sorting Layer
            var sprite = o.GetComponent<SpriteRenderer>();
            PlayerPrefs.SetString(slot + "sortingLayer", sprite.sortingLayerName);
        }
        //
        PlayerPrefs.Save();
    }

    public static void LoadGameState(int slot)
    {
        var o = GameObject.Find("Inventory");
        var inv = o.GetComponent<Inventory>();
        o = GameObject.Find("Party");
        var party = o.GetComponent<Party>();
        // Inventory
        // Money
        inv.AddAmountofMoney(PlayerPrefs.GetInt(slot + "x" + "money"));
        // Item Counter
        // Usable Items
        for (var i = 0; i < inv.AmountOfItems; i++)
        {
            inv.AddAmountofItem(i, (short)PlayerPrefs.GetInt(slot + "x" + i + "u"));
        }
        // GroupParty
        for (var p = 0; p < party.GroupLength(); p++)
        {
            for (var i = 0; i < party.GetCharacterInParty(p).Armor.Length; i++)
            {
                inv.UseItem(PlayerPrefs.GetInt(slot + "x" + p + "n" + i + "Armor"), p);
            }
            // Is in GroupParty
            if (PlayerPrefs.GetInt(slot + "x" + p + "GroupParty") == 1)
            {
                party.GetCharacterInParty(p).InParty = true;
            }
            party.GetCharacterInParty(p).InParty = false;

            // Level
            party.GetCharacterInParty(p).Level = PlayerPrefs.GetInt(slot + "x" + p + "Level");
            // Exp
            party.GetCharacterInParty(p).Exp = PlayerPrefs.GetInt(slot + "x" + p + "Exp");
            // Exp 2 Level up	
            party.GetCharacterInParty(p).ExpToLevelUp = PlayerPrefs.GetInt(slot + "x" + p + "exp2l");
            // ParticipantCondition
            party.GetCharacterInParty(p).Condition = PlayerPrefs.GetInt(slot + "x" + p + "ParticipantCondition");
        }
        // GameSettings
        // Application Infos

        // Player Position
        o = GameObject.Find("Hero");
        var transform = o.GetComponent<Transform>();
        transform.position = new Vector3(PlayerPrefs.GetFloat(slot + "xCo"),
        PlayerPrefs.GetFloat(slot + "yCo"),
        PlayerPrefs.GetFloat(slot + "zCo"));
        // Player Sorting Layer
        var sprite = o.GetComponent<SpriteRenderer>();
        sprite.sortingLayerName = PlayerPrefs.GetString(slot + "sortingLayer");
        //
        Application.LoadLevel(PlayerPrefs.GetString(slot + "CurrentLevel"));
    }

}
