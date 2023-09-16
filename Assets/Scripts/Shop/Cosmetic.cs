using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CosToggleState
{
    Disabled,
    On,
    Off
}

public abstract class Cosmetic : IncomeUpdateListener
{
    public string cosName;
    public long cosPrice;
    public string cosDescription;

    [Header("Links")]
    public List<Cosmetic> linkedCosmetics = new();

    private Image spinner;
    private TextMeshProUGUI cosNameText;
    private TextMeshProUGUI cosPriceText;
    private TextMeshProUGUI cosDescriptionText;

    private CosToggleState state = CosToggleState.Disabled;

    private void OnEnable()
    {
        foreach (Transform t in transform)
        {
            if (t.TryGetComponent(out Image image))
            {
                if (t.gameObject.name == "Spinner")
                {
                    spinner = image;
                    continue;
                }
                    
            }

            if (t.TryGetComponent(out TextMeshProUGUI tmp))
            {
                if (t.gameObject.name == "Name")
                {
                    cosNameText = tmp;
                    if (cosName != "")
                    {
                        cosNameText.text = cosName;
                    }
                    continue;
                }

                if (t.gameObject.name == "Price")
                {
                    cosPriceText = tmp;
                    if (cosPrice != 0)
                    {
                        cosPriceText.text = Global.LongToString(cosPrice) + " rings";
                    }
                    continue;
                }

                if (t.gameObject.name == "Description")
                {
                    cosDescriptionText = tmp;
                    if (cosDescription != "")
                    {
                        cosDescriptionText.text = cosDescription;
                    }
                    continue;
                }
            }
        }

        GetComponent<Button>().onClick.AddListener(() => { CosmeticClicked(); });
        GetComponent<Button>().interactable = false;
    }

    public void SetName(string _name)
    {
        cosName = _name;
        cosNameText.text = cosName;
    }

    public void SetPrice(long _price)
    {
        cosPrice = _price;
        cosPriceText.text = Global.LongToString(cosPrice) + " rings";
    }

    public void SetDescription(string _desc) 
    {
        cosDescription = _desc;
        cosDescriptionText.text = cosDescription;
    }

    public void LoadUnlockCosmetic()
    {
        GetComponent<Button>().interactable = true;
        GetComponent<Image>().color = Color.yellow;
        spinner.color = Color.black;
        cosPriceText.text = "Inactive";
        state = CosToggleState.Off;
    }

    public bool IsUnlocked()
    {
        return state != CosToggleState.Disabled;
    }

    private void CosmeticClicked()
    {
        switch (state)
        {
            case CosToggleState.Disabled:
                Global.incomeManager.SubtractRings(cosPrice);
                state = CosToggleState.On;
                GetComponent<Image>().color = Color.green;
                GetComponent<Button>().interactable = true;
                spinner.color = Color.white;
                cosPriceText.text = "Active";
                DeactivateLinked();

                ToggleOn();
                break;

            case CosToggleState.On:
                state = CosToggleState.Off;

                GetComponent<Image>().color = Color.yellow;
                spinner.color = Color.black;
                cosPriceText.text = "Inactive";

                ToggleOff();
                break;

            case CosToggleState.Off:
                state = CosToggleState.On;

                GetComponent<Image>().color = Color.green;
                spinner.color = Color.white;
                cosPriceText.text = "Active";
                DeactivateLinked();

                ToggleOn();
                break;
        }
    }

    public void Deactivate()
    {
        if (state == CosToggleState.Disabled)
        {
            return;
        }

        state = CosToggleState.Off;

        GetComponent<Image>().color = Color.yellow;
        spinner.color = Color.black;
        cosPriceText.text = "Inactive";
    }

    private void DeactivateLinked()
    {
        
        foreach (Cosmetic cos in linkedCosmetics)
        {
            cos.Deactivate();
            cos.ToggleOff();
        }
    }

    public override void OnIncomeUpdate()
    {
        if (state != CosToggleState.Disabled)
            return;

        if (Global.incomeManager.CanAfford(cosPrice))
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public abstract void ToggleOn();
    public abstract void ToggleOff();
}
