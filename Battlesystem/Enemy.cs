using System;
using UnityEngine;
[Serializable]
public class Enemy : BattleParticipant
{
    public int[] SkillList;
    public override void Update()
    {
        if (!IsReady) return;
        GameObject.Find("BattleEngine").GetComponent<BattleEngine>().Enqueue(DoSomethin());
        IsReady = false;
        DidAction = true;
    }

    private BattleAction DoSomethin()
    {
        var x = new BattleAction { Attacker = 1, Defender = 0, SkillId = 0 };
        return x;
    }
}
