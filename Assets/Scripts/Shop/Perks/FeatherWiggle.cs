using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherWiggle : Perk
{
    public override void ApplyPerk()
    {
        ShopItem item = Global.itemManager.FindShopItem("Head Wing Feather");
        Global.incomeManager.AddRings(item.GetCount() * 100);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPerkDescription("Gain rings equal to the amount of [Head Wing Feather] x 100");
    }
}
