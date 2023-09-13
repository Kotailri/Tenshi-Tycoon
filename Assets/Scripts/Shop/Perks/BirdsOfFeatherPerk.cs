using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsOfFeatherPerk : Perk
{
    private long trsBoost;

    public override void ApplyPerk()
    {
        trsBoost = Global.itemManager.FindShopItem("Ceiling Fan").GetCount() / 10;
        Global.itemManager.FindShopItem("Head Wing Feather").SetShopItemRate(trsBoost, true);
    }

    void Start()
    {
        SetPerkName("Aerodynamics");
        SetPerkDescription("Increase the tr/s of [Head Wing Feather] by 1 for every 10 [Ceiling Fan].");
    }

    public override void ApplyPerkUpdate()
    {
        if (IsPurchased()) 
        {
            ShopItem feather = Global.itemManager.FindShopItem("Head Wing Feather");
            feather.SetShopItemRate(feather.GetRate() - trsBoost);
            ApplyPerk();
        }
        
    }
}
