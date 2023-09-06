using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IncomeController : MonoBehaviour
{
    public long baseRate;

    [Space(15.0f)]
    public TextMeshProUGUI rateText;
    public TextMeshProUGUI ringText;

    [Space(10.0f)]
    public List<ShopItem> shopItems = new();
    public GameObject ShopContent;

    public long rings = 0;
    public long currentRate = 1;

    public class Item
    {
        public string itemName;
        public long rate;
        public long price;
        public Item(string _name, long _rate, long _price)
        {
            itemName = _name;
            rate = _rate;
            price = _price;
        }
    }

    public List<Item> itemInfo = new List<Item> 
    {
        //       Name                                Rate                      Price
        new Item("Ceiling Fan",                        1,                      150),
        new Item("Head Wing Feather",                 10,                     1000),
        new Item("Poofy Cloud",                       80,                   11_000),    
        new Item("Amai Light Stick",                 470,                  120_000),
        new Item("Juice",                           2600,                1_300_000),
        new Item("Parfait",                       14_000,               14_000_000),
        new Item("Golden Frying Pan",             78_000,              200_000_000),
        new Item("Hydrangea Bouquet",            440_000,            3_300_000_000),
        new Item("Maki Figurine",              2_600_000,           51_000_000_000),
        new Item("Pair of Trucks",            16_000_000,          750_000_000_000),
        new Item("Tenshi Daki",              100_000_000,       10_000_000_000_000),
        new Item("Tenshi Wing",              650_000_000,      140_000_000_000_000),
        new Item("Amelia Watson Hat",      4_300_000_000,    1_700_000_000_000_000),
        new Item("Onion Ring Factory",    29_000_000_000,   21_000_000_000_000_000),
        new Item("Tenshi Hug",           210_000_000_000,  260_000_000_000_000_000)
    };

    void Start()
    {
        Global.incomeController = this;

        rateText.text = Global.LongToString(currentRate) + " tr/s";
        ringText.text = Global.LongToString(rings);
        
        int currId = 0;
        float ypos = -30f;

        foreach (Transform t in ShopContent.transform)
        {
            if (t.TryGetComponent(out ShopItem item))
            {
                if (currId >= itemInfo.Count)
                {
                    t.localScale = Vector3.zero;
                    continue;
                }

                item.ShopItemId = currId;
                item.SetShopItemName(itemInfo[currId].itemName);
                item.SetShopItemPrice(itemInfo[currId].price);
                item.SetShopItemRate(itemInfo[currId].rate);
                shopItems.Add(item);

                t.position = new Vector3(t.position.x, ypos, t.position.z);
                ypos -= 90f;
                currId++;
            }
        }

        InvokeRepeating(nameof(UpdateIncome), 0f, 0.1f);
    }

    public void UpdateIncome()
    {
        ringText.text = Global.LongToString(rings);
        long calculatedRate = baseRate;
        foreach (ShopItem item in shopItems)
        {
            calculatedRate += item.GetFullRate();
        }
        currentRate = calculatedRate;
        rings += (calculatedRate/10);
        rateText.text = Global.LongToString(currentRate) + " tr/s";
        ringText.text = Global.LongToString(rings);
    }

    public bool DecreaseRings(long _rings)
    {
        if (rings - _rings >= 0)
        {
            rings -= _rings;
            UpdateIncome();
            return true;
        }
        return false;
    }
}
