using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greenhouse : Perk
{
    public override void ApplyPerk()
    {
        ShopItem cloud = Global.itemManager.FindShopItem("Poofy Cloud");
        ShopItem hydrangea = Global.itemManager.FindShopItem("Hydrangea Bouquet");

        cloud.UpdateItemLimit(50, true);
        cloud.IncrementItemCount(50);

        hydrangea.UpdateItemLimit(15, true);
        hydrangea.IncrementItemCount(15);
    }

    void Start()
    {
        SetPerkPrice(25_000_000_000);
        SetPerkDescription($"Increase [Poofy Cloud] amount and limit by 50. Increase [Hydrangea Bouquet] amount and limit by 15");
    }
}
