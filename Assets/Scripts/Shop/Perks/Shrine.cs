using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : Perk
{
    public override void ApplyPerk()
    {
        ShopItem item = Global.itemManager.FindShopItem("Maki Figurine");
        item.IncrementItemCount(5);
        Global.itemManager.FindShopItem("Maki Figurine").AddTempDiscount(0.5f);

        if (!Global.reloading)
        {
            Global.incomeManager.StartBoost();
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPerkPrice(200_000_000_000);
        SetPerkDescription("Gain 5 [Maki Figurine]. The next [Maki Figurine] is 50% cheaper. Enter boost mode if not already in it.");
    }

    
}
