using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItem : IncomeUpdateListener
{
    [HideInInspector]
    public TextMeshProUGUI shopItemNameText;
    [HideInInspector]
    public TextMeshProUGUI shopItemPriceText;
    [HideInInspector]
    public TextMeshProUGUI shopItemCountText;

    private Button plus10Button;
    private Button plusInfButton;

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

            if (t.TryGetComponent(out Button button))
            {
                if (t.gameObject.name == "plus10")
                {
                    button.onClick.AddListener(() => 
                    {
                        if (Global.incomeManager.CanAfford(item.price*10) && GetCount()+10 <= item.limit && !locked)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                PurchaseItem();
                            }
                        }
                        
                    });
                }

                if (t.gameObject.name == "plusinf")
                {
                    button.onClick.AddListener(() => 
                    {
                        do { 
                            PurchaseItem(); 
                        } while (!locked && Global.incomeManager.CanAfford(item.price));
                    });
                }
            }
            
        }

        GetComponent<Button>().onClick.AddListener(() => 
        {
            if (!locked)
                PurchaseItem();
        });
    }

    public void UpdateAffordability()
    {
        bool affordable = Global.incomeManager.CanAfford(item.price);
        if (affordable == canBuy)
            return;

        canBuy = affordable;
        GetComponent<Button>().interactable = canBuy;
    }

    public void UpdateItemLimit(long _limit, bool increaseFrom=false)
    {
        long lim = _limit;
        if (increaseFrom)
        {
            lim = _limit + item.limit;
        }

        if (lim >= item.limit)
        {
            if (lim == item.count)
            {
                GetComponent<Image>().color = Color.green;
                shopItemPriceText.text = "Sold Out!";
                GetComponent<Button>().interactable = false;
                locked = true;
            }
            else
            {
                GetComponent<Image>().color = Color.white;
                shopItemPriceText.text = Global.LongToString(item.price) + " rings";
                locked = false;
            }
            
        }
        item.limit = lim;
        OnIncomeUpdate();
    }

    private void PurchaseItem()
    {
        if (!Global.incomeManager.CanAfford(item.price) || locked)
        {
            return;
        }

        Global.incomeManager.SubtractRings(item.price);
        IncrementItemCount();

        GetComponent<HasHoverInfo>().Refresh();

        CheckLimit();
    }

    public void SetLock(bool isLocked)
    {
        locked = isLocked;
        GetComponent<Image>().color = Color.green;
        shopItemPriceText.text = "Sold Out!";
        GetComponent<Button>().interactable = false;
    }

    public void CheckLimit()
    {
        if (item.count >= item.limit)
        {
            SetLock(true);
        }
    }

    public string GetName()
    {
        return item.shopname;
    }

    public long GetRate()
    {
        return item.rate;
    }

    public long GetPrice()
    {
        return item.price;
    }

    public long GetCount()
    {
        return item.count;
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
        if (locked)
            return;

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

    public void IncrementItemCount(long _count)
    {
        for (int i = 0; i < _count; i++)
        {

            IncrementItemCount();
        }
    }

    public void IncrementItemCount()
    {
        item.IncrementItemCount();
        if (item.count == 1)
        {
            string an = $"Bought [{item.shopname}] for the first time!";
            Global.announcer.CreateAnnouncement(an);
        }

        if (item.count == 69)
        {
            string an = "haha funny number";
            Global.announcer.CreateAnnouncement(an);
        }

        if (TryGetComponent(out ItemAchivement ach))
        {
            ach.CheckItemAchivements(item.count);
        }

        shopItemCountText.text = Global.LongToString(item.count);
    }

    public override void OnIncomeUpdate()
    {
        if (!locked && item != null)
            UpdateAffordability();
    }
}
