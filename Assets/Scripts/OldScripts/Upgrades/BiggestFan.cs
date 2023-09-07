using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BiggestFan : Upgrade, ProgressLock
{
    public override void SetAsSold()
    {
        base.SetAsSold();
        updateLock();
    }

    public override void AddButtonListener()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            BuyUpgrade();
        });
    }

    public override void BuyUpgrade()
    {
        if (checkLock())
        {
            Global.incomeController.DecreaseRings(price);
            Global.incomeController.shopItems[0].ShopItemCount *= 2;
            Global.incomeController.shopItems[0].ShopItemCountText.text = Global.incomeController.shopItems[0].ShopItemCount.ToString();
            updateLock();
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public bool checkLock()
    {
        bool rotationSpeedCheck = Global.incomeController.upgradeItems[1].GetComponent<RotationSpeed>().level >= 4;
        bool fanNumberCheck = Global.incomeController.shopItems[0].ShopItemCount >= 50;

        return rotationSpeedCheck && fanNumberCheck;
    }

    private void Start()
    {
        AddButtonListener();
        GetComponent<HasHoverText>().text = "Requires <Rotation Speed V> and at least 50 [Ceiling Fan]";
        UpdateName("Biggest Fan");
        UpdatePrice(100_000);
        UpdateInfo("Double the number of [Ceiling Fans]");
    }

    public void updateLock()
    {
        GetComponent<HasHoverText>().DisableHoverBox();
        isSold = true;
        UpdatePrice("Purchased");
        GetComponent<Image>().color = Color.green;
        GetComponent<Button>().interactable = false;
    }
}
