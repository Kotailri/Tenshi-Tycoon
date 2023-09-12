using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitBreakPerk : Perk
{
    public long limitIncrease;

    public override void ApplyPerk()
    {
        foreach (ShopItem item in Global.itemManager.shopItems) 
        {
            item.UpdateItemLimit(limitIncrease, true);
        }
    }

    public void Start()
    {
        SetPerkName("Limit Break " + limitIncrease.ToString());
        SetPerkDescription("Increases shop limit of all items by " + limitIncrease.ToString());
    }
}
