using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SweetTooth : Perk
{
    public long limitIncrease;
    public long numberOfFans;
    public long trsBoost;

    public override void ApplyPerk()
    {
        ApplyInitialBuffs();
        ShopItem item = Global.itemManager.FindShopItem("Parfait");
        item.IncrementItemCount(numberOfFans);
    }

    private void ApplyInitialBuffs()
    {
        ShopItem item = Global.itemManager.FindShopItem("Parfait");
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
        SetPerkDescription($"Increase [Parfait] amount by {numberOfFans}, increase limit by {limitIncrease}, increase tr/s by {trsBoost}");
    }
}
