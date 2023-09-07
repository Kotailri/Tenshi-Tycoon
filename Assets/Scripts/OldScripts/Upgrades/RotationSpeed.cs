using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationSpeed : Upgrade, ScalingUpgrade
{

    private List<UpgradeTier> upgrades = new()
    {
        new UpgradeTier("Fan Rotation Speed I", "Increases ceiling fan trs to 2", 250, 1),
        new UpgradeTier("Fan Rotation Speed II", "Increases ceiling fan trs to 3", 500, 2),
        new UpgradeTier("Fan Rotation Speed III", "Increases ceiling fan trs to 4", 1000, 3),
        new UpgradeTier("Fan Rotation Speed IV", "Increases ceiling fan trs to 5", 5000, 4),
        new UpgradeTier("Fan Rotation Speed V", "Increases ceiling fan trs to 10", 100000, 5),
    };

    public int level = -1;

    public override void AddButtonListener()
    {
        GetComponent<Button>().onClick.AddListener(()=> {
            BuyUpgrade();
        });
    }

    public override void BuyUpgrade()
    {
        if (level >= 4)
        {
            return;
        }

        Global.incomeController.DecreaseRings(price);
        level++;

        long newRate = 1;
        switch (level)
        {
            case 0:
                newRate = 2;
                break;
            case 1:
                newRate = 3;
                break;
            case 2:
                newRate = 4;
                break;
            case 3:
                newRate = 5;
                break;
            case 4:
                newRate = 10;
                break;
        }

        if (level < 4)
        {
            UpdatePrice(upgrades[level + 1].price);
            UpdateName(upgrades[level + 1].upgradeName);
            UpdateInfo(upgrades[level + 1].upgradeDescription);
        }

        Global.incomeController.shopItems[0].ShopItemRate = newRate;
        Global.incomeController.shopItems[0].GetComponent<HasHoverText>().text = ("Increases tr/s by " + Global.LongToString(newRate));

        if (level >= 4)
        {
            isSold = true;
            UpdatePrice("Max Upgrade Reached");
            GetComponent<Image>().color = Color.green;
            GetComponent<Button>().interactable = false;
        }


    }

    void Start()
    {
        UpdatePrice(upgrades[level + 1].price);
        UpdateName(upgrades[level + 1].upgradeName);
        UpdateInfo(upgrades[level + 1].upgradeDescription);

        AddButtonListener();
    }

    public void SetLevel(int _level)
    {
        level = _level;
        long newRate = 1;
        switch (level)
        {
            case 0:
                newRate = 2;
                break;
            case 1:
                newRate = 3;
                break;
            case 2:
                newRate = 4;
                break;
            case 3:
                newRate = 5;
                break;
            case 4:
                newRate = 10;
                break;
        }

        if (level < 4)
        {
            UpdatePrice(upgrades[level + 1].price);
            UpdateName(upgrades[level + 1].upgradeName);
            UpdateInfo(upgrades[level + 1].upgradeDescription);
        }

        Global.incomeController.shopItems[0].ShopItemRate = newRate;
        Global.incomeController.shopItems[0].GetComponent<HasHoverText>().text = ("Increases tr/s by " + newRate.ToString());

        if (level >= 4)
        {
            isSold = true;
            UpdatePrice("Max Upgrade Reached");
            GetComponent<Image>().color = Color.green;
            GetComponent<Button>().interactable = false;
        }
    }

    public int GetLevel()
    {
        return level;
    }
}
