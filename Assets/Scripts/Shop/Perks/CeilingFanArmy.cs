using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CeilingFanArmy : Perk
{
    public long limitIncrease;
    public long numberOfFans;
    public long trsBoost;

    public override void ApplyPerk()
    {
        ApplyInitialBuffs();
        ShopItem item = Global.itemManager.FindShopItem("Ceiling Fan");
        item.IncrementItemCount(numberOfFans);
    }

    private void ApplyInitialBuffs()
    {
        ShopItem item = Global.itemManager.FindShopItem("Ceiling Fan");
        item.UpdateItemLimit(limitIncrease, true);
        item.SetShopItemRate(item.GetRate() + trsBoost);
    }

    public override void LoadPerkFromSave()
    {
        purchased = true;
        GetComponent<Image>().color = Color.green;
        GetComponent<Button>().interactable = false;
        priceText.text = "Purchased!";
        Global.perkManager.CheckPerkRequirements();
        ApplyInitialBuffs();
        DisableLinkedPerks();
    }

    void Start()
    {
        SetPerkDescription($"Increase [Ceiling Fan] amount by {numberOfFans}, increase limit by {limitIncrease}, increase tr/s by {trsBoost}");
    }
}
