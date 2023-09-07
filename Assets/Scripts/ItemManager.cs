using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Item> itemsList = new();

    private void Awake()
    {
        Global.itemManager = this;
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
