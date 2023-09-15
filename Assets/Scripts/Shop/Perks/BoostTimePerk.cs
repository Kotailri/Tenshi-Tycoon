using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostTimePerk : Perk
{
    public int boostTime;
    public override void ApplyPerk()
    {
        Global.incomeManager.boostTime += boostTime;
    }

    private void Start()
    {
        SetPerkDescription($"Increases boost time length by {boostTime} seconds.");
    }
}
