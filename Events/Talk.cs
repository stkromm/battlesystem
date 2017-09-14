using UnityEngine;

public class Talk : ActivateEvent
{
    public string Message;
    public bool ItemsReceived = false;

    override public void ActivatedGui()
    {
        GUI.Label(new Rect(0, 0, 1920, 320), "<size=50>" + Message + "</size>");
        if (!Input.GetButtonDown("Interact"))
        {
            return;
        }
        var o = GameObject.Find("Inventory");
        var inv = o.GetComponent<Inventory>();
        var ann = o.GetComponent<Announcer>();
        o = GameObject.Find("Party");
        var party = o.GetComponent<Party>();
        if (ItemsReceived)
        {
            return;
        }
        inv.AddAmountofItem(1, 1);
        ann.AddAnnouncement("Received Item 1");
        inv.AddAmountofItem(2, 1);
        inv.AddAmountofItem(3, 1);
        inv.AddAmountofItem(0, 1);
        inv.AddAmountofMoney(500);
        party.GetCharacterInParty(party.ActiveChar).BuffCharacter(BuffDatabase.Buffs()[0]);
        ItemsReceived = true;
    }
}
