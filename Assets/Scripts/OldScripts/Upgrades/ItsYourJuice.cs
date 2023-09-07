using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItsYourJuice : Upgrade
{
    public override void AddButtonListener()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            BuyUpgrade();
        });
    }

    public override void BuyUpgrade()
    {
        Global.incomeController.DecreaseRings(price);
        isSold = true;
        UpdatePrice("Purchased");
        GetComponent<Image>().color = Color.green;
        GetComponent<Button>().interactable = false;
        Global.incomeController.shopItems[4].ShopItemRate += (long)(Global.incomeController.shopItems[4].ShopItemRate*0.25f);
    }

    private void Start()
    {
        AddButtonListener();
        UpdateName("It's Your Juice");
        UpdatePrice(1_000_000);
        UpdateInfo("Increase efficiency of [Juice] by 25%");
    }
}
