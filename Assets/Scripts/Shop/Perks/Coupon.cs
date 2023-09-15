using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coupon : Perk
{
    public int discountPercent;

    public override void ApplyPerk()
    {
        foreach (ShopItem item in Global.itemManager.shopItems) 
        {
            item.AddSharedTempDiscount(discountPercent / 100.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PerkName == "")
            SetPerkName($"{discountPercent}% Item Coupon");
        SetPerkDescription($"{discountPercent}% off the next shop item you buy.");
    }
}
