using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class Perk : IncomeUpdateListener
{
    public string PerkName;
    public long PerkPrice;
    public string PerkDescription;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI infoText;

    public bool purchased = false;
    private bool isHidden = false;

    [Space(15)]
    [Header("Requirement")]
    public Perk requiredPerk = null;

    [Space(15)]
    [Header("Link")]
    public List<Perk> linkedPerks = new();
    public bool perkDisabled = false;

    public void CheckRequiredPerk()
    {
        if (requiredPerk == null || purchased) { return; }

        if (requiredPerk.IsPurchased())
        {
            UnhidePerk();
        }
        else
        {
            HidePerk();
        }
    }

    public void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(() => { PurchasePerk(); });

        foreach (Transform t in transform)
        {
            if (t.TryGetComponent(out TextMeshProUGUI tmp))
            {
                
                if (t.gameObject.name == "Name")
                {
                    nameText = tmp;
                    continue;
                }

                if (t.gameObject.name == "Price")
                {
                    priceText = tmp;
                    continue;
                }

                if (t.gameObject.name == "Info")
                {
                    infoText = tmp;
                    continue;
                }
            }
        }
        nameText.text = PerkName;
        if (PerkPrice == 0)
        {
            priceText.text = "Free";
        }
        else
        {
            priceText.text = Global.LongToString(PerkPrice) + " rings";
        }
        
        infoText.text = PerkDescription;
        CheckRequiredPerk();
    }

    public void SetInfo(string PerkName, string PerkDescription, long PerkPrice)
    {
        this.PerkDescription = PerkDescription;
        this.PerkName = PerkName;
        this.PerkPrice = PerkPrice;
    }

    public void SetPerkName(string _name)
    {
        PerkName = _name;
        if (!isHidden) { nameText.text = PerkName; }
    }

    public void SetPerkDescription(string desc)
    {
        PerkDescription = desc;
        if (!isHidden) { infoText.text = PerkDescription; }
    }

    public void SetPerkPrice(long _price)
    {
        PerkPrice = _price;
        if (!isHidden) {
            if (PerkPrice == 0)
            {
                priceText.text = "Free";
            }
            else
            {
                priceText.text = Global.LongToString(PerkPrice) + " rings";
            }
        }
    }

    public void HidePerk()
    {
        isHidden = true;
        nameText.text  = "??????";
        priceText.text = "????? rings";
        infoText.text  = "Perk Locked";
        GetComponent<Image>().color = Color.gray;
        GetComponent<Button>().interactable = false;
    }

    public void UnhidePerk()
    {
        isHidden = false;
        nameText.text = PerkName;
        priceText.text = Global.LongToString(PerkPrice) + " rings";
        infoText.text = PerkDescription;
        if (PerkPrice == 0)
        {
            priceText.text = "Free";
        }
        GetComponent<Image>().color = Color.white;
    }

    public void DisableLinkedPerks()
    {
        foreach (Perk p in linkedPerks)
        {
            p.DisablePerk();
        }
    }

    public void DisablePerk()
    {
        perkDisabled = true;
        nameText.text = "Path Locked";
        priceText.text = Global.LongToString(PerkPrice) + " rings";
        infoText.text = PerkDescription;
        GetComponent<Image>().color = Color.gray;
        GetComponent<Button>().interactable = false;
    }

    public void PurchasePerk()
    {
        if (Global.incomeManager.CanAfford(PerkPrice) && !purchased)
        {
            LoadPerk();
            AudioManager.instance.PlaySound("buy");
            Global.incomeManager.SubtractRings(PerkPrice);
        }
    }

    public bool IsPurchased()
    {
        return purchased;
    }

    public virtual void LoadPerk()
    {
        purchased = true;
        UnhidePerk();
        GetComponent<Image>().color = Color.green;
        priceText.text = "Purchased!";
        Global.perkManager.CheckPerkRequirements();
        ApplyPerk();
        DisableLinkedPerks();
    }

    public virtual void LoadPerkFromSave()
    {
        LoadPerk();
    }

    public override void OnIncomeUpdate()
    {
        if (purchased || perkDisabled)
            return;
        Global.perkManager.CheckPerkRequirements();

        if (Global.incomeManager.CanAfford(PerkPrice) && !isHidden)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public virtual void ApplyPerkUpdate()
    {

    }

    public abstract void ApplyPerk();
}
