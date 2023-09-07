using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string shopname;
    public string shopinfo;

    public long price;
    public long rate;
    public long count;

    [Space(15.0f)]
    public ShopItem shopItem;

    private void Start()
    {
        shopItem = GetComponent<ShopItem>();
    }

    public long GetIncome()
    {
        return rate * count;
    }

    public void SetShopItemName(string _name)
    {
        shopname = _name;
        shopItem.shopItemNameText.text = _name;
    }

    public void SetShopItemPrice(long _price)
    {
        price = _price;
        shopItem.shopItemPriceText.text = Global.LongToString(_price);
    }

    public void SetShopItemRate(long _rate)
    {
        rate = _rate;
        SetShopItemInfo("Increases tr/s by " + Global.LongToString(_rate));
    }

    public void SetShopItemInfo(string _info)
    {
        shopinfo = _info;
    }

    public void SetShopItemCount(long _count)
    {
        count = _count;
        shopItem.shopItemCountText.text = Global.LongToString(_count);
    }

    public void IncrementItemCount()
    {
        count++;
        shopItem.shopItemCountText.text = Global.LongToString(count);
    }
}
