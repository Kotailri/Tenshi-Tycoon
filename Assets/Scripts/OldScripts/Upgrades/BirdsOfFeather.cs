using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdsOfFeather : Upgrade
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

        long currRate = Global.incomeController.shopItems[11].ShopItemRate;
        long feathers = Global.incomeController.shopItems[1].ShopItemCount;
        feathers /= 10;
        currRate += (currRate * (feathers / 100));
        Global.incomeController.shopItems[11].SetShopItemRate(currRate);
        Global.incomeController.shopItems[11].GetComponent<HasHoverText>().text = ("Increases tr/s by " + Global.LongToString(currRate));
    }

    private void Start()
    {
        AddButtonListener();
        UpdateName("Birds of a Feather");
        UpdatePrice(1_000_000_000_000);
        UpdateInfo("Increase efficiency of [Tenshi Wing] by 1% for every 10 [Head Wing Feather] you **CURRENTLY** have");
    }
}
