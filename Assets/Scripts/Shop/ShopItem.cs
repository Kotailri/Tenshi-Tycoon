using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [Header("TMPro")]
    public TextMeshProUGUI shopItemNameText;
    public TextMeshProUGUI shopItemPriceText;
    public TextMeshProUGUI shopItemCountText;

    [Space(15.0f)]
    public Item item;

    private void Start()
    {
        item = GetComponent<Item>();

        GetComponent<Button>().onClick.AddListener(() => 
        {
            if (Global.incomeManager.CanAfford(item.price))
            {
                Global.incomeManager.SubtractRings(item.price);
            }
            item.IncrementItemCount();
        });
    }

    
}
