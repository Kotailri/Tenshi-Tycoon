using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickUpPerk : Perk
{
    public long clickIncreaseAmount;

    public void Start()
    {
        SetPerkDescription($"Increases amount of rings gained per click by {clickIncreaseAmount}");
    }

    public override void ApplyPerk()
    {
        Global.ringClicker.AddClickAmount(clickIncreaseAmount);
    }
}
