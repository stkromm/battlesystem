using System;
using System.Linq;
using UnityEngine;

public class BattleGui : MyMonoBehaviour
{
    enum SelectionState
    {
        ActionMenu,
        TacticSelection,
        SkillSelection,
        ItemSelection,
        TargetSelection
    }
    private SelectionState _selection = SelectionState.ActionMenu;
    private readonly int _scHi = Screen.height;
    private readonly int _scWi = Screen.width;
    //
    public Inventory Inventory;
    public Party Party;
    public BattleEngine BattleEngine;
    public int Target;
    public int Skill;
    private readonly string[] _menu = { "Attack", "Taktik", "Skills", "Items" };
    // Use this for initialization
    void Start()
    {
        var o = GameObject.Find("Inventory");
        Inventory = o.GetComponent<Inventory>();
        BattleEngine = GameObject.Find("BattleEngine").GetComponent<BattleEngine>();
    }

    void Update()
    {
        if (Input.GetButtonDown("GameMenu"))
        {
            _selection = SelectionState.ActionMenu;
        }
    }
    void OnGUI()
    {
        switch (BattleEngine.Mode)
        {
            case BattleMode.Installed:
                BattleMenu();

                break;
            case BattleMode.Won:
                GUI.Label(new Rect(0, 0, _scWi, _scHi), "<size=50>WON</size>");
                break;
            case BattleMode.Lose:
                GUI.Label(new Rect(0, 0, _scWi, _scHi), "<size=50>LOSE</size>");
                break;
        }
    }

    private void TargetSelection()
    {
        for (var i = 0; i < BattleEngine.EnemyTeam.ToArray().Length; i++)
        {
            if (!GUI.Button(new Rect(100, 100 + 100 * i, 500, 80), BattleEngine.EnemyTeam.ToArray()[i].Name)) continue;
            Target = i;
            var x = new BattleAction { Defender = BattleEngine.AlliedTeam.ToArray().Length + Target, Attacker = Array.IndexOf(BattleEngine.AlliedTeam.ToArray(), BattleEngine.ActiveHero), SkillId = Skill };
            // CHECK, IF ACTION IS POSSIBLE
            BattleEngine.Enqueue(x);
            BattleEngine.ActiveHero.DidAction = true;
            BattleEngine.ActiveHero = null;
            _selection = SelectionState.ActionMenu;
        }
    }

    void BattleMenu()
    {
        CharacterStatus();
        MonsterStatus();
        if (BattleEngine.ActiveHero == null) return;
        switch (_selection)
        {
            case SelectionState.TargetSelection:
                TargetSelection();
                break;
            default:
                ActionMenu();
                break;

        }

    }
    void MonsterStatus()
    {
        var s = BattleEngine.EnemyTeam.Aggregate("", (current, c) => current + (c.Name + " HP:" + c.Health + " //MP:" + c.Mana + " //COND:" + c.ParticipantCondition + " AP:" + c.ActionPoints + "\n"));
        GUI.Label(new Rect(0, _scHi / 15f, _scWi / 2f, _scHi / 15f), "<size=25>" + s + "</size>");
    }
    void CharacterStatus()
    {
        var s = BattleEngine.AlliedTeam.Aggregate("", (current, c) => current + (c.Name + " HP:" + c.Health + " //MP:" + c.Mana + " //COND:" + c.ParticipantCondition + " AP:" + c.ActionPoints + "\n"));
        GUI.Label(new Rect(_scWi / 2f, _scHi / 15 * 12, _scWi / 2f, _scHi / 15f), "<size=25>" + s + "</size>");

    }
    void ActionMenu()
    {
        if (_selection == SelectionState.ActionMenu)
        {
            for (var i = 0; i < _menu.Length; i++)
            {
                if (GUI.Button(new Rect(_scWi / 40f, _scHi / 15 * (15 - _menu.Length + i), _scWi / 5f, _scHi / 15f), _menu[i]))
                {
                    if (i == 0)
                    {
                        Skill = 0;
                        _selection = SelectionState.TargetSelection;
                    }
                    else if (i == 2)
                    {
                        _selection = SelectionState.SkillSelection;
                    }
                }
            }
        }
        if (_selection == SelectionState.SkillSelection)
        {
            var index = 0;
            var ch = BattleEngine.ActiveHero.Ch;
            for (var i = 0; i < ch.Skills.Length; i++)
            {
                if (!ch.Learned[i]) continue;
                if (GUI.Button(new Rect(_scWi / 40f, _scHi / 15 * (15 - _menu.Length + index), _scWi / 5f, _scHi / 15f), SkillDatabase.Skills()[ch.Skills[i]].Name))
                {
                    Skill = ch.Skills[i];
                    _selection = SelectionState.TargetSelection;
                }
                index++;
            }

        }
        if (_selection == SelectionState.TacticSelection)
        {
        }
        if (_selection == SelectionState.ItemSelection)
        {
        }
    }
}

