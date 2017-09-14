using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum MenuState
{
    Character,
    Achievments,
    Settings,
    Tutorial,
    Exit,
    Nothing,
    Main
}
public enum ItemFilter
{
    None,
    Head,
    Chest,
    Shoes,
    Back,
    Trinket,
    Waist,
    Weapon,

    Equip,
    Usable,
    Misc
}
public class MenuGui : MyMonoBehaviour
{
    //GameObjects
    private Party _party;
    private Inventory _inv;
    // Inspector assigned
    public Texture ArmorSkin;
    public Texture2D CursorTex;
    public Announcer Announcer;
    //Selection State of the menu
    private MenuState _menuState = MenuState.Nothing;
    private int _equipSlot;
    private int _characterToolBarInt;
    private float _verticalSliderValue;
    //screen extends variables  
    private readonly int _scWi = Screen.width;
    private readonly int _scHi = Screen.height;
    //
    private readonly Language _lang = new Language();
    private readonly Vector2 _vec2 = Vector2.zero;
    private const CursorMode CursorMode = UnityEngine.CursorMode.Auto;

    void Start()
    {
        var o = GameObject.Find("Party");
        _party = o.GetComponent<Party>();
        o = GameObject.Find("Inventory");
        _inv = o.GetComponent<Inventory>();
        if (_party == null || _inv == null)
            enabled = false;
    }

    void OnGUI()
    {
        switch (_menuState)
        {
            case MenuState.Main:
                MainMenu();
                break;
            case MenuState.Character:
                CharacterMenu();
                break;
            case MenuState.Achievments:
                break;
            case MenuState.Tutorial:
                SaveMenu();
                break;
            case MenuState.Settings:
                SettingsMenu();
                break;
            case MenuState.Exit:
                // Back to Title
                if (GUI.Button(new Rect(_scWi / 2 - 200, _scHi / 7 * 2, 400, 80), "<size=40>Back to Title?</size>"))
                {
                    // Reset cursor
                    Cursor.SetCursor(null, _vec2, CursorMode);
                    // Resume the game				
                    Time.timeScale = 1;
                    // Destroy all objects, that wont be destroyed through Scene change
                    var o = GameObject.Find("Inventory");
                    Destroy(o);
                    o = GameObject.Find("GroupParty");
                    Destroy(o);
                    o = GameObject.Find("Hero");
                    Destroy(o);
                    o = GameObject.Find("Main Camera");
                    Destroy(o);
                    o = GameObject.Find("Game Starter");
                    Destroy(o);
                    // Destroy this object ( otherwise some strange gui overlapping happens)
                    Destroy(this);
                    // Load the Title Scene
                    Application.LoadLevel(1);
                }
                break;
        }


    }

    void Update()
    {
        if (!Input.GetButtonDown("GameMenu"))
        {
            return;
        }
        switch (_menuState)
        {
            // Menu decall
            case MenuState.Main:
                _menuState = MenuState.Nothing;
                Cursor.SetCursor(null, _vec2, CursorMode);
                Time.timeScale = 1;
                break;
            // Menu call
            default:
                _menuState = MenuState.Main;
                Cursor.SetCursor(CursorTex, _vec2, CursorMode);
                Time.timeScale = 0;
                break;
        }
    }

    void MainMenu()
    {
        foreach (var i in _lang.Menu1Items.Where(i => GUI.Button(new Rect(_scWi / 10 * 4, _scHi / 7 * (Array.IndexOf(_lang.Menu1Items, i) + 1), _scWi / 5f, _scHi / 12f), "<size=40>" + i + "</size>")))
        {
            _menuState = (MenuState)Array.IndexOf(_lang.Menu1Items, i);
        }
    }

    void SettingsMenu()
    {
    }

    void CharacterMenu()
    {
        _characterToolBarInt = GUI.Toolbar(new Rect(0f, 0f, _scWi, _scHi / 20f), _characterToolBarInt, _lang.Menu2Items);
        CharacterMenuBottom();
        switch (_characterToolBarInt)
        {
            case 1:
                CharacterViewUi();
                break;
            case 2:
                SkillViewUi();
                break;
            case 3:
                BuffViewUi();
                break;
            case 0:
                InventoryUi();
                break;
        }
    }

    void CharacterMenuBottom()
    {
        if (_party == null)
        {
            _party = GameObject.Find("Party").GetComponent<Party>();
        }
        GUI.Label(new Rect(0f, _scHi / 40f * 39f, _scWi, _scHi / 40f),
           "<Size=20> Played:" + Mathf.Ceil(Time.realtimeSinceStartup) + "\tGold:" + _inv.gold + "\t" + _party.GetCharacterInParty(_party.ActiveChar).CharacterName + "</size>");
        if (GUI.Button(new Rect(_scWi / 2f, _scHi / 40f * 39f, _scWi / 15f, _scHi / 40f), "change target")) { _party.ChangeActiveCharacter(); }
    }

    void MenuItemListUi(IEnumerable<ListableMenuItem> list, float startX, float startY, int columnOffSet)
    {
        var buttonWidth = (3f / 10f) * _scWi;
        var buttonHeight = _scHi / 12f;
        var column = columnOffSet;
        var row = 0;
        foreach (var i in list)
        {
            var buttonRect = new Rect(startX + column * buttonWidth, startY + row * buttonHeight, buttonWidth,
                buttonHeight);
            if (GUI.Button(buttonRect,
                "<size=20>" + i.Name + i.AdditionalNameInfo + "</size>") && !i.Use())
            {
                Announcer.AddAnnouncement("It's not the right time for this.");
            }
            if (buttonRect.Contains(new Vector2(Input.mousePosition.x, _scHi - Input.mousePosition.y)))
            {
                var generator = new ScreenFittedElements();
                GUI.Label(generator.ScreenFittedRect(startX + column * buttonWidth, startY + row * buttonHeight, buttonWidth,
                buttonHeight), i.Info);
            }
            // Calculate the column and row of the next button
            column = (column + 1) % 3;
            row = column == 0 ? row + 1 : row;
        }
    }

    void InventoryUi()
    {
        _verticalSliderValue = _inv.AmountOfItems <= 50 ? 0 : GUI.VerticalSlider(new Rect(_scWi / 40f, _scHi / 12f, _scWi / 25f, _scHi / 12f * 10f), _verticalSliderValue, 0, _inv.AmountOfItems / 3f);
        var a = (Math.Ceiling(_verticalSliderValue) - 1) * 3;
        var displayedItems = (from item in _inv.GetItemListKeys let temp = Array.IndexOf(_inv.GetItemListKeys, item) where temp >= a && temp <= a + 50 select new ListableMenuItem(item)).ToList();
        if (displayedItems.Count != 0)
            MenuItemListUi(displayedItems, _scWi / 20f, _scHi / 12f, 0);
    }

    void BuffViewUi()
    {
        var cha = _party.GetCharacterInParty(_party.ActiveChar);
        var displayedItems = from b in cha.Buffs.Values where b != null select new ListableMenuItem(b);
        MenuItemListUi(displayedItems, _scWi / 20f, _scHi / 12f, 0);
    }

    void SkillViewUi()
    {
        var cha = _party.GetCharacterInParty(_party.ActiveChar);
        var query = cha.Skills.Where((t, i) => cha.Learned[i]).Select(t => new ListableMenuItem(SkillDatabase.Skills()[t])).ToList();
        var displayedItems = query.ToList();
        MenuItemListUi(displayedItems, _scWi / 20f, _scHi / 12f, 0);
    }

    void CharacterViewUi()
    {
        var cha = _party.GetCharacterInParty(_party.ActiveChar);
        if (cha == null)
        {
            _party.ChangeActiveCharacter();
            cha = _party.GetCharacterInParty(_party.ActiveChar);
            if (cha == null) return;
        }
        if (cha.FaceTex != null)
            GUI.DrawTexture(new Rect(_scWi / 20f, _scHi / 12f, _scWi / 8f, _scHi / 4f), cha.FaceTex);
        if (ArmorSkin != null)
            GUI.DrawTexture(new Rect(_scWi / 5 * 4, _scHi / 12f, _scWi / 8f, _scHi / 4f), ArmorSkin);
        // Attribute
        var s = "";
        s = cha.Attributes.Aggregate(s, (current, attribute) => current + _lang.StatusItems[Array.IndexOf(cha.Attributes, attribute)] + attribute.GetValue(cha.Level) + "\\" + attribute.GetActualValue(cha.Level) + "\n");
        GUI.Label(new Rect(_scWi / 3f, _scHi / 12f, _scWi / 5f, _scHi / 2f), "<size=25>" + s + "</size>");
        // For each equipment Slot
        foreach (var i in cha.Armor.Where(i => new Rect(_scWi / 2f, _scHi / 12f, _scWi / 4f, _scHi / 20f * Array.IndexOf(cha.Armor, i)).Contains(new Vector2(Input.mousePosition.x,
            _scHi - Input.mousePosition.y)) && i != -1 && name != ""))
        {
            char[] a = { '\\', 'n' };
            var str = ItemDatabase.GetItemInfo(i).Split(a);
            GUI.Box(new Rect(_scWi / 60 * 18, 0, _scWi / 10 * 1, _scHi / 90 * str.Length), ItemDatabase.GetItemInfo(i));
        }
        _equipSlot = GUI.SelectionGrid(new Rect(_scWi / 2f, _scHi / 12f, _scWi / 4f, _scHi / 20f * 9f), _equipSlot, cha.Armor.Select(i => ItemDatabase.GetItemName(i)).ToArray(), 1);
        EquipableItemOfSlotList(cha);
    }

    void EquipableItemOfSlotList(Character cha)
    {
        GUI.Label(new Rect(_scWi / 20f, _scHi / 11f + _scHi / 2f, _scWi * (3f / 10f), _scHi / 12f), "<size=20>" + _lang.ItemMenuCat[1] + "</size>");
        // Equip Nothing
        if (GUI.Button(new Rect(_scWi / 20f, _scHi * (4f / 6f), _scWi * (3f / 10f), _scHi / 12f), "<size=20> --- </size>"))
        {
            cha.EquipSlotWithItem(_equipSlot, -1);
        }
        // List of all equipable Slot Items
        var displayedItems = _inv.GetItemListKeys.Where(
            i => (FilterItem((ItemFilter)_equipSlot + 1, i))).Select(i => new ListableMenuItem(i)).ToList();
        if (displayedItems.Count != 0)
            MenuItemListUi(displayedItems, _scWi / 20f, _scHi * (4f / 6f), 1);
    }

    public bool FilterItem(ItemFilter filter, int i)
    {
        if ((int)filter >= 8) return filter == ItemFilter.None;
        return ItemDatabase.GetItemSlot(i) == (int)filter;
    }

    void SaveMenu()
    {
        if (GUI.Button(new Rect(_scWi / 3f, _scHi / 3 + _scHi / 10, Screen.width / 3f, Screen.height / 20f), "<size=20>Save State 1</size>"))
            SaveStateManager.SaveGameState(1);
    }
}