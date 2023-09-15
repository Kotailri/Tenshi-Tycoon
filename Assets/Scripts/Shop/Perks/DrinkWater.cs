using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWater : Perk
{
    public override void ApplyPerk()
    {
        ShopItem item = Global.itemManager.FindShopItem("Amelia Watson Hat");
        item.IncrementItemCount(1);
        Global.incomeManager.boostTime += 60;
    }

    void Start()
    {
        SetPerkPrice(5_000_000_000_000_000);
        SetPerkDescription($"Gain 1 [Amelia Watson Hat]. Increase boost time by 60 seconds.");
    }
}
