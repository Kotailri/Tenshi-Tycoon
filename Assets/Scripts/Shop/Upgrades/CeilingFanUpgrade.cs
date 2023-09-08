using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingFanUpgrade : ShopUpgrade
{
    private void Start()
    {
        targetItemName = "Ceiling Fan";
        upgradeInfoList = new()
        {
            new Upgrade("Ceiling Fan Tier 1", "Increase [Ceiling Fan] tr/s by 1",  500, $"Requires at least 10 [Ceiling Fan]."),
            new Upgrade("Ceiling Fan Tier 2", "Decrease [Ceiling Fan] cost by 10", 800, $"Requires at least 15 [Ceiling Fan]."),
            new Upgrade("Ceiling Fan Tier 3", "Increase [Ceiling Fan] tr/s by 1",  3000, $"Requires at least 45 [Ceiling Fan]."),
            new Upgrade("Ceiling Fan Tier 4", "Decrease [Ceiling Fan] cost by 10", 5000, $"Requires at least 60 [Ceiling Fan]."),
            new Upgrade("Ceiling Fan Tier 5", "Increase [Ceiling Fan] tr/s by 3",  10000, $"Requires at least 100 [Ceiling Fan].")
        };

        UpdateMaxLevel(upgradeInfoList.Count);
        SetUpgrade(upgradeInfoList[0]);
    }

    public override void ApplyUpgrade()
    {
        GetComponent<HasHoverInfo>().Refresh();
        base.ApplyUpgrade();
        ShopItem item = Global.itemManager.FindShopItem("Ceiling Fan");

        switch (GetLevel())
        {
            case 1:
                item.SetShopItemRate(item.GetRate() + 1);
                break;
            case 2:
                item.SetShopItemPrice(item.GetPrice() - 10);
                break;
            case 3:
                item.SetShopItemRate(item.GetRate() + 1);
                break;
            case 4:
                item.SetShopItemPrice(item.GetPrice() - 10);
                break;
            case 5:
                item.SetShopItemRate(item.GetRate() + 3);
                break;
        }

        SetUpgrade(upgradeInfoList[GetLevel()]);
    }

    public override bool CheckRequirements()
    {
        GetComponent<HasHoverInfo>().Refresh();
        Item item = Global.itemManager.FindItem("Ceiling Fan");

        switch (GetLevel())
        {
            case 0:
                return item.count >= 10;
            case 1:
                return item.count >= 15;
            case 2:
                return item.count >= 45;
            case 3:
                return item.count >= 60;
            case 4:
                return item.count >= 100;
        }
        return true;
    }

}
