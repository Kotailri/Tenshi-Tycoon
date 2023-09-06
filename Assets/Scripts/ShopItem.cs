using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public void SetShopItemName(string _name)
    {
        ShopItemName = _name;
        ShopItemNameText.text = _name;
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

    public long GetFullRate(float modifier=1.0f)
    {
        return (long)(ShopItemRate * ShopItemCount * modifier);
    }
}
