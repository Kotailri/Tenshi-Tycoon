using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IncomeManager : MonoBehaviour
{
    [Header("TMPro")]
    public TextMeshProUGUI ringText;
    public TextMeshProUGUI rateText;

    [Header("Rings")]
    public long rings = 0;
    public float rate = 0;

    private readonly float updateRate = 0.1f;

    private void CollectRings()
    {
        long collection = Global.itemManager.GetCollectiveRate();
        rateText.text = Global.LongToString(collection) + " tr/s";
        rings += (long)(collection * updateRate);
        ringText.text = Global.LongToString(rings);
    }

    public bool CanAfford(long _rings)
    {
        return rings >= _rings;
    }

    public void SubtractRings(long _rings)
    {
        rings -= _rings;
        ringText.text = Global.LongToString(rings);
    }

    public void AddRings(long _rings)
    {
        rings += _rings;
        ringText.text = Global.LongToString(rings);
    }

    void Start()
    {
        Global.incomeManager = this;
        InvokeRepeating(nameof(CollectRings), 0f, updateRate);
    }
}
