using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [HideInInspector]
    public TextMeshProUGUI shopItemNameText;
    [HideInInspector]
    public TextMeshProUGUI shopItemPriceText;
    [HideInInspector]
    public TextMeshProUGUI shopItemCountText;

    [HideInInspector]
    public Item item;
    private bool canBuy = false;
    private bool locked = false;

    private void OnEnable()
    {
        foreach (Transform t in transform)
        {
            if (t.TryGetComponent(out TextMeshProUGUI tmp))
            {

                if (t.gameObject.name == "Name")
                {
                    shopItemNameText = tmp;
                    continue;
                }

                if (t.gameObject.name == "Price")
                {
                    shopItemPriceText = tmp;
                    continue;
                }

                if (t.gameObject.name == "Count")
                {
                    shopItemCountText = tmp;
                    continue;
                }
            }
            
        }

        GetComponent<Button>().onClick.AddListener(() => 
        {
            if (!locked)
                PurchaseItem();
        });
    }

    private void FixedUpdate()
    {
        if (!locked)
            UpdateAffordability();
    }

    public void UpdateAffordability()
    {
        bool affordable = Global.incomeManager.CanAfford(item.price);
        if (affordable == canBuy)
            return;

        canBuy = affordable;
        GetComponent<Button>().interactable = canBuy;
    }

    public void UpdateItemLimit(long _limit)
    {
        if (_limit >= item.limit)
        {
            GetComponent<Image>().color = Color.white;
            shopItemPriceText.text = Global.LongToString(item.price) + " rings";
            locked = false;
        }
        item.limit = _limit;
    }

    private void PurchaseItem()
    {
        if (Global.incomeManager.CanAfford(item.price))
        {
            Global.incomeManager.SubtractRings(item.price);
        }
        IncrementItemCount();

        if (item.count >= item.limit)
        {
            GetComponent<Image>().color = Color.green;
            shopItemPriceText.text = "Sold Out!";
            GetComponent<Button>().interactable = false;
            
            locked = true;
        }
    }

    public string GetName()
    {
        return item.shopname;
    }

    public long GetIncome()
    {
        return item.GetIncome();
    }

    public void SetItem(Item _item)
    {
        item = _item;
        shopItemNameText.text = item.shopname;
        shopItemPriceText.text = Global.LongToString(item.price) + " rings";
    }

    public void SetShopItemName(string _name)
    {
        item.SetShopItemName(_name);
        shopItemNameText.text = _name;
    }

    public void SetShopItemPrice(long _price)
    {
        item.SetShopItemPrice(_price);
        shopItemPriceText.text = Global.LongToString(_price) + " rings";
    }

    public void SetShopItemRate(long _rate)
    {
        item.SetShopItemRate(_rate);
    }

    public void SetShopItemCount(long _count)
    {
        item.SetShopItemCount(_count);
        shopItemCountText.text = Global.LongToString(_count);
    }

    public void IncrementItemCount()
    {
        item.IncrementItemCount();
        shopItemCountText.text = Global.LongToString(item.count);
    }

}
