using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyRing : Perk
{
    public int clickBonusIncreasePercent;

    public override void ApplyPerk()
    {
        Global.ringClicker.AddBonusRingChance(clickBonusIncreasePercent);
    }


    void Start()
    {
        if (PerkDescription == "")
            SetPerkDescription($"Increases chance for 10x bonus rings on click by {clickBonusIncreasePercent}%");
    }
}
