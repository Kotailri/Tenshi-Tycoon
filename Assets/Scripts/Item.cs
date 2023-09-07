using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public long rate;
    public long count;

    public long GetIncome()
    {
        return rate * count;
    }

    private void Awake()
    {
        Global.itemManager.itemsList.Add(this);
    }
}
