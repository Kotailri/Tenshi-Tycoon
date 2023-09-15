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

    private BoostEffect boostEffect;
    
    [HideInInspector]
    public bool boostEnabled = false;

    public int boostMultiplier;
    public float boostTime;

    private void Update()
    {
        // DEBUG
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddRings(BigInteger.Parse("100"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddRings(BigInteger.Parse("1000"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddRings(BigInteger.Parse("10000"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AddRings(BigInteger.Parse("100000"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AddRings(BigInteger.Parse("1000000"));
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AddRings(BigInteger.Parse("100000000000000000000000000000000"));
        }
    }

    public int GetBoostMultiplier()
    {
        return boostMultiplier;
    }

    public void StartBoost()
    {
        if (!boostEnabled)
            boostEffect.StartBoostTimer(boostTime);
    }

    public void EnableBoost()
    {
        boostEnabled = true;
    }

    public void DisableBoost()
    {
        boostEnabled = false;
    }

    private void CollectRings()
    {
        long collection = Global.itemManager.GetCollectiveRate();

        if (boostEnabled) { 
            collection *= boostMultiplier; 
        }

        rate = collection;
        rateText.text = Global.LongToString(collection) + " tr/s";

        if (collection >= (updateRate * 100))
        {
            AddRings((long)(collection * updateRate));
        }
        else
        {
            partialRings += collection * updateRate;
        }

        if (partialRings >= 1)
        {
            partialRings -= 1;
            AddRings(1);
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
        NotifyIncomeListeners();
    }

    private void NotifyIncomeListeners()
    {
        foreach (IncomeUpdateListener listener in Global.incomeListeners)
        {
            listener.OnIncomeUpdate();
        }
    }

    public void AddRings(BigInteger _rings)
    {
        rings += _rings;
        ringText.text = Global.LongToString(rings, true);
        NotifyIncomeListeners();
    }

    private void OnEnable()
    {
        Global.incomeManager = this;
        boostEffect = GetComponent<BoostEffect>();
        InvokeRepeating(nameof(CollectRings), 0.1f, updateRate);
    }
}
