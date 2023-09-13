using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceReductionPerk : Perk
{
    public double reductionPercent;

    private void Start()
    {
        SetPerkDescription($"Reduces the price of all store items by {reductionPercent}%");
    }

    public override void ApplyPerk()
    {
        foreach (ShopItem item in Global.itemManager.shopItems)
        {
            long currPrice = item.GetPrice();
            item.SetShopItemPrice(currPrice - (long)(currPrice * (reductionPercent/100)));
        }
    }
}
