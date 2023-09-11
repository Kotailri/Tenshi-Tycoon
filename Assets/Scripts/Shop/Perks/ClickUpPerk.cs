using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickUpPerk : Perk
{
    public int clickIncreaseAmount = 2;

    public void Start()
    {
        SetInfo("Click Efficiency I", $"Increases amount of rings gained per click by {clickIncreaseAmount}", 1500);
        UnhidePerk();
    }

    public override void ApplyPerk()
    {
        Global.ringClicker.AddClickAmount(clickIncreaseAmount);
    }
}
