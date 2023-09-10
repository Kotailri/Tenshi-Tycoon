using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class IncomeManager : MonoBehaviour
{
    [Header("TMPro")]
    public TextMeshProUGUI ringText;
    public TextMeshProUGUI rateText;

    [Header("Rings")]
    public BigInteger rings = 0;
    public BigInteger rate = 0;

    private readonly float updateRate = 0.1f;
    private float partialRings = 0.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // DEBUG
            AddRings(BigInteger.Parse("10000000"));
        }
    }

    private void CollectRings()
    {
        long collection = Global.itemManager.GetCollectiveRate();
        rateText.text = Global.LongToString(collection) + " tr/s";

        if (collection >= (updateRate * 100))
        {
            rings += (long)(collection * updateRate);
        }
        else
        {
            partialRings += collection * updateRate;
        }

        if (partialRings >= 1)
        {
            partialRings -= 1;
            rings += 1;
        }
        
        ringText.text = Global.LongToString(rings, true);
    }

    public bool CanAfford(BigInteger _rings)
    {
        return rings >= _rings;
    }

    public void SubtractRings(BigInteger _rings)
    {
        rings -= _rings;
        ringText.text = Global.LongToString(rings, true);
    }

    private bool millionaire = false;

    public void AddRings(BigInteger _rings)
    {
        rings += _rings;
        ringText.text = Global.LongToString(rings, true);

        if (rings > 1_000_000 && !millionaire)
        {
            Global.announcer.CreateAnnouncement("You're a millionaire! Congrats!");
            millionaire = true;
        }
    }

    private void OnEnable()
    {
        Global.incomeManager = this;
        InvokeRepeating(nameof(CollectRings), 0.1f, updateRate);
    }
}
