using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Transform itemsShopContent;
    public List<Item> itemsList = new();

    private void Awake()
    {
        Global.itemManager = this;
    }

    private void Start()
    {
        foreach (Transform t in itemsShopContent)
        {
            if (t.TryGetComponent(out Item item))
            {
                itemsList.Add(item);
            }
        }
    }

    public long GetCollectiveRate()
    {
        long collection = 0;
        foreach (Item item in itemsList)
        {
            collection += item.GetIncome();
        }
        return collection;
    }
}
