using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostChancePerk : Perk
{
    public int boostChance;
    public override void ApplyPerk()
    {
        Global.ringClicker.boostChancePercent += boostChance;
    }

    private void Start()
    {
        SetPerkDescription($"Increases chance for boost time by {boostChance}% on click.");
    }
}
