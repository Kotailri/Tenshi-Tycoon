using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class TenshiBlessing : Perk
{
    [Header("Refresh time in Minutes")]
    public float blessingRefreshTime;

    private double currentTimer = 0f;
    private bool timerStopped = false;

    private RefreshHoverInfo hover;

    private void Start()
    {
        hover = GetComponent<RefreshHoverInfo>();
        purchased = true;

        SetPerkPrice(0);
        SetPerkName("Tenshi's Blessing");
        SetPerkDescription($"Gain rings equal to 1000x your current tr/s. This perk refreshes every {blessingRefreshTime} minutes.");
    }

    public override void ApplyPerk()
    {
        BigInteger currRate = Global.incomeManager.rate;
        BigInteger ringsToAdd = BigInteger.Multiply(currRate, 1000);
        Global.incomeManager.AddRings(ringsToAdd);
        currentTimer = 0f;
        timerStopped = false;
    }

    public override void LoadPerkFromSave()
    {
        base.LoadPerkFromSave();
        priceText.text = "Loading";
        GetComponent<Image>().color = new Color(0.85f, 0.85f, 0.85f, 0.8f);
    }

    private void RefreshPerk()
    {
        if (IsPurchased()) 
        {
            purchased = false;
            GetComponent<Image>().color = Color.white;
            priceText.text = "Purchased!";
        }
    }

    private void Update()
    {
        if (currentTimer >= blessingRefreshTime * 60 && !timerStopped)
        {
            timerStopped = true;
            hover.SetRefreshText("Blessing Available!");
            RefreshPerk();
        }
        
        if (!timerStopped)
        {
            currentTimer += Time.deltaTime;

            double remainingTime = (blessingRefreshTime * 60) - currentTimer;
            if (remainingTime < 60)
            {
                hover.SetRefreshText($"Blessing available in {remainingTime:n1} seconds.");
            }
            else
            {
                hover.SetRefreshText($"Blessing available in {(int)remainingTime/60}:{((int)remainingTime%60).ToString().PadLeft(2,'0')}");
            }

        }
    }
}
