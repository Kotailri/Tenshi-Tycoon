using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KaraokeTime : Upgrade
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

        long currRate = Global.incomeController.shopItems[3].ShopItemRate;
        currRate += (3 * (currRate / 100));
        Global.incomeController.shopItems[3].SetShopItemRate(currRate);
        Global.incomeController.shopItems[3].GetComponent<HasHoverText>().text = ("Increases tr/s by " + Global.LongToString(currRate));
    }

    private void Start()
    {
        AddButtonListener();
        UpdateName("Karaoke Time");
        UpdatePrice(10_000);
        UpdateInfo("Increase efficiency of [Amai Light Stick] by 5%");
    }
}
