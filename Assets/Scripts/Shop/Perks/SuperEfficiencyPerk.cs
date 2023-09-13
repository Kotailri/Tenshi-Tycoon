using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperEfficiencyPerk : Perk
{
    public double trsPercent;

    private void Start()
    {
        SetPerkDescription($"Increase tr/s of all store items by {trsPercent}%");
    }

    public override void ApplyPerk()
    {
        foreach (ShopItem item in Global.itemManager.shopItems)
        {
            long currRate = item.GetRate();
            item.SetShopItemRate(currRate + (long)(currRate * (trsPercent / 100)));
        }
    }
}
