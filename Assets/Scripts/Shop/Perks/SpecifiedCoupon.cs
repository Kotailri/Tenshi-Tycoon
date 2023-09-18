using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecifiedCoupon : Perk
{
    public string shopItemName;
    public int discountPercent;

    public override void ApplyPerk()
    {
        if (Global.reloading) { return; }
        Global.itemManager.FindShopItem(shopItemName).AddTempDiscount(discountPercent / 100.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPerkName($"{discountPercent}% {shopItemName} Coupon");
        SetPerkDescription($"{discountPercent}% off the next [{shopItemName}] you buy.");
    }
}
