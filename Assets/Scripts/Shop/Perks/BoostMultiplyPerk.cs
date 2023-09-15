using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostMultiplyPerk : Perk
{
    public int boostMultiplier;
    public override void ApplyPerk()
    {
        Global.incomeManager.boostMultiplier += boostMultiplier;
    }

    private void Start()
    {
        SetPerkDescription($"Increases boost mutiplier by {boostMultiplier}%");
    }
}
