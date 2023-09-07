using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopItem : MonoBehaviour
{
    [Header("Item Info")]
    public long ShopItemPrice;
    public long ShopItemRate;
    [Space(25.0f)]

    public string ShopItemName;
    public long ShopItemId;
    [Space(15.0f)]

    [Header("Text")]
    public TextMeshProUGUI ShopItemNameText;
    public TextMeshProUGUI ShopItemPriceText;

    [Space(5.0f)]
    public TextMeshProUGUI ShopItemCountText;

    [Space(10.0f)]
    public long ShopItemCount = 0;

    [HideInInspector]
    public delegate long RateMod(long rate);
    [HideInInspector]
    public List<RateMod> rateModList = new();

    public void SetShopItemCount(long count)
    {
        ShopItemCount = count;
        ShopItemCountText.text = count.ToString();
    }

    public void SetShopItemName(string _name)
    {
        ShopItemName = _name;
        ShopItemNameText.text = _name;
        transform.name = "[" + ShopItemId + "] " + _name;
    }

    public void SetShopItemPrice(long _price)
    {
        ShopItemPrice = _price;
        ShopItemPriceText.text = Global.LongToString(_price) + " rings";
    }

    public void SetShopItemRate(long _rate)
    {
        ShopItemRate = _rate;
    }

    public void BuyItem()
    {
        if (Global.incomeController.DecreaseRings(ShopItemPrice))
        {
            ShopItemCount++;
            ShopItemCountText.text = ShopItemCount.ToString();
        }
    }

    public void Update()
    {
        UpdateAffordability();
    }

    public void UpdateAffordability()
    {
        if (Global.incomeController.rings >= ShopItemPrice)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public long GetFullRate()
    {
        long rate = ShopItemRate * ShopItemCount;
        foreach (RateMod modify in rateModList)
        {
            rate = modify(rate);
        }

        return rate;
    }
}
