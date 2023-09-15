using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryAutomation : Perk
{
    public override void ApplyPerk()
    {
        ShopItem item = Global.itemManager.FindShopItem("Onion Ring Factory");
        item.SetShopItemRate(item.GetRate() * 3);
    }

    void Start()
    {
        SetPerkPrice(25_000_000_000_000_000);
        SetPerkDescription($"Triple the rate of [Onion Ring Factory]");
    }
}
