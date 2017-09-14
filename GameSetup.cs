using UnityEngine;

public class GameSetup : MyMonoBehaviour
{
    public GameObject Inventory;
    public GameObject Party;
    public GameObject Hero;

    public void Start()
    {
        if (GameObject.Find("Hero") == null)
            Hero = (GameObject)Instantiate(Hero);
        if (GameObject.Find("Party") == null)
            Party = (GameObject)Instantiate(Party);
        if (GameObject.Find("Inventory") == null)
            Inventory = (GameObject)Instantiate(Inventory);
        Hero.name = "Hero";
        Party.name = "Party";
        Inventory.name = "Inventory";
    }
}
