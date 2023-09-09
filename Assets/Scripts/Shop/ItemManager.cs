using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Transform itemsShopContent;
    public GameObject storeItemTemplate;

    private List<Item> itemsList;
    public List<ShopItem> shopItems = new();

    private void Awake()
    {
        Global.itemManager = this;
    }

    private void Start()
    {
        foreach (Transform child in itemsShopContent.transform)
        {
            Destroy(child.gameObject);
        }

        InstantiateItems();
        PopulateStore();
    }

    private void InstantiateItems()
    {
        itemsList = new()
        {
            //       Name                                 Rate                      Price
            new Item("Ceiling Fan",                         1,                      100),
            new Item("Head Wing Feather",                  10,                     1000),
            new Item("Poofy Cloud",                        80,                   11_000),
            new Item("Amai Light Stick",                  470,                  120_000),
            new Item("Juice",                            2600,                1_300_000),
            new Item("Parfait",                        14_000,               14_000_000),
            new Item("Golden Frying Pan",              78_000,              200_000_000),
            new Item("Hydrangea Bouquet",             440_000,            3_300_000_000),
            new Item("Maki Figurine",               2_600_000,           51_000_000_000),
            new Item("Pair of Trucks",             16_000_000,          750_000_000_000),
            new Item("Tenshi Daki",               100_000_000,       10_000_000_000_000),
            new Item("Tenshi Wing",               650_000_000,      140_000_000_000_000),
            new Item("Amelia Watson Hat",       4_300_000_000,    1_700_000_000_000_000),
            new Item("Onion Ring Factory",     29_000_000_000,   21_000_000_000_000_000),
            new Item("Tenshi Wish",      210_000_000_000,  260_000_000_000_000_000)
        };
    }

    public ShopItem FindShopItem(string name)
    {
        foreach (ShopItem item in shopItems)
        {
            if (item.GetName().Equals(name))
            {
                return item;
            }
        }
        Debug.LogError($"Could not find shop item '{name}'");
        return null;
    }

    public Item FindItem(string name)
    {
        foreach (ShopItem item in shopItems)
        {
            if (item.GetName().Equals(name))
            {
                return item.item;
            }
        }
        Debug.LogError($"Could not find item '{name}'");
        return null;
    }

    private void PopulateStore()
    {
        Vector3 firstPosition = new Vector3(716.99f, -15f, 0.00f);
        float yPos = 0.0f;

        foreach (Item item in itemsList)
        {
            GameObject shopItem = Instantiate(storeItemTemplate, (firstPosition + new Vector3(0,yPos,0)), Quaternion.identity);
            shopItem.GetComponent<ShopItem>().SetItem(new Item(item.shopname, item.rate, item.price));
            shopItem.transform.SetParent(itemsShopContent);
            shopItem.transform.name = item.shopname;
            shopItems.Add(shopItem.GetComponent<ShopItem>());
            yPos -= 90f;
        }
    }

    public long GetCollectiveRate()
    {
        long collection = 0;
        foreach (ShopItem item in shopItems)
        {
            collection += item.GetIncome();
        }
        return collection;
    }
}
