using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TenshiBlessing : Upgrade, ProgressLock
{
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
            Global.incomeController.DecreaseRings(-1000);
            updateLock();
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public override void SetAsSold()
    {
        base.SetAsSold();
        updateLock();
    }

    public bool checkLock()
    {
        bool fanNumberCheck = Global.incomeController.shopItems[0].ShopItemCount >= 5;
        return fanNumberCheck;
    }

    private void Start()
    {
        AddButtonListener();
        GetComponent<HasHoverText>().text = "Requires at least 5 [Ceiling Fan]";
        UpdateName("Blessing of Tenshi");
        UpdatePrice(0);
        UpdateInfo("Gain 1000 rings :bless:");
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
