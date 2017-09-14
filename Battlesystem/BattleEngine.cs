using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public enum BattleMode
{
    Installed,
    Started,
    Won,
    Lose
}

public class BattleEngine : MyMonoBehaviour
{

    private string _message;
    public BattleMode Mode { private set; get; }
    public List<BattleParticipant> Participants { private set; get; }
    public PlayerParticipant ActiveHero {set; get; }
    public IEnumerable<BattleParticipant> AlliedTeam
    {
        get
        {
            return Participants == null ? null : Participants.Where(p => p.GetType() == typeof(PlayerParticipant)).ToArray();
        }
    }

    public IEnumerable<BattleParticipant> EnemyTeam
    {
        get
        {
            return Participants == null ? null : Participants.Where(p => p.GetType() == typeof(Enemy)).ToArray();
        }
    }

    private readonly Queue _actionList = new Queue();
    private bool _stopped = true;

    public void Enqueue(System.Object o)
    {
        _actionList.Enqueue(o);
    }

    public void Initialize()
    {
        SetUpData();
        SetUpSound();
    }

    public bool SetUpSound()
    {

        return false;
    }

    public bool SetUpData()
    {
        Participants = new List<BattleParticipant>();
        _actionList.Clear();
        Mode = BattleMode.Installed;
        ActiveHero = null;
        var o = GameObject.Find("Party");
        var p = o.GetSafeComponent<Party>();
        for (var i = 0; i < p.GroupLength(); i++)
        {
            if (p.GetCharacterInParty(i).InParty && p.GetCharacterInParty(i).InCombatParty)
            {
                Participants.Add(new PlayerParticipant(p.GetCharacterInParty(i)));
            }
        }
        foreach (var t in GameObject.Find("BattleEngine").GetComponent<EnemyParty>().group)
        {
            Participants.Add(t);
        }
        return true;
    }

    public void Start()
    {
        Initialize();
        _stopped = false;
    }
    public void Update()
    {
        FillActionBars();
        if (_stopped)
        {
            return;
        }
        _stopped = true;
        foreach (var e in Participants)
        {
            e.Update();
        }
        switch (Mode)
        {
            case BattleMode.Installed:
                if (CheckEnd() == 1)
                {
                    Mode = BattleMode.Won;
                }
                if (CheckEnd() == 2)
                {
                    Mode = BattleMode.Lose;
                }
                else
                {
                    BattleAction b = null;
                    while (b == null && _actionList.Count != 0)
                    {
                        var o = _actionList.Dequeue();
                        if (o.GetType() != typeof(BattleAction)) continue;
                        b = (BattleAction)o;
                    }
                    if (b != null)
                    {
                        StartCoroutine(DoAction(b));
                    }
                    else
                    {
                        _stopped = false;
                    }
                }
                break;
            default:
                EndBattle();
                _stopped = false;
                break;
        }

    }

    public void FillActionBars()
    {
        if (Participants == null) return;
        foreach (var p in Participants)
        {
            var x = Random.Range(1, 3);
            p.ActionPoints = (p.ActionPoints + x) > 1000 ? 1000 : p.ActionPoints + x;
            if (p.ActionPoints != 1000 || p.DidAction) continue;
            p.IsReady = true;
            if (ActiveHero == null && p.GetType() == typeof(PlayerParticipant))
            {
                ActiveHero = (PlayerParticipant)p;
            }
        }
    }

    public int CheckEnd()
    {
        if (EnemyTeam == null) return 0;
        var x = EnemyTeam.Aggregate(0, (current, p) => p.Health > 0 ? current + 1 : current);
        if (x == 0)
        {
            return 1;
        }
        x = AlliedTeam.Aggregate(0, (current, p) => p.Health > 0 ? current + 1 : current);
        return x == 0 ? 2 : 0;
    }

    public IEnumerator DoAction(BattleAction a)
    {

        _message = "FÜHRE AKTION AUS" + a.Attacker + " " + a.Defender + " " + a.SkillId + " ";
        if (Participants != null)
        {
            yield return new WaitForSeconds(3);
            var attacker = Participants.ToArray()[a.Attacker];
            var defender = Participants.ToArray()[a.Defender];
            var skill = SkillDatabase.Skills()[a.SkillId];
            //if (!attacker.DrainMana(skill.Cost)) return;
            var damage = skill.Damage + attacker.Attack - defender.Defense;
            defender.DealDamage(damage);
            attacker.IsReady = false;
            ActiveHero = null;
            attacker.RefreshBuffs(1);
            attacker.DidAction = false;
            attacker.ActionPoints -= skill.Actioncost;
           
        }
        _stopped = false;
        _message = "NICHTS ZU TUN";
    }

    public void EndBattle()
    {
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "</size=50>" + _message + "</size>");
    }
}


