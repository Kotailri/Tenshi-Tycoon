using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class Perk : IncomeUpdateListener
{
    public string PerkName;
    public long PerkPrice;
    public string PerkDescription;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI infoText;

    private bool purchased = false;
    private bool isHidden = false;

    [Space(15)]
    [Header("Requirement")]
    public Perk requiredPerk = null;

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
        priceText.text = Global.LongToString(PerkPrice) + " rings";
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
        if (!isHidden) { priceText.text = Global.LongToString(PerkPrice) + " rings"; }
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

    public void PurchasePerk()
    {
        if (Global.incomeManager.CanAfford(PerkPrice) && !purchased)
        {
            LoadPerk();
            Global.incomeManager.SubtractRings(PerkPrice);
        }
    }

    public bool IsPurchased()
    {
        return purchased;
    }

    public void LoadPerk()
    {
        purchased = true;
        GetComponent<Image>().color = Color.green;
        priceText.text = "Purchased!";
        Global.perkManager.CheckPerkRequirements();
        ApplyPerk();
    }

    public override void OnIncomeUpdate()
    {
        if (purchased)
            return;

        if (Global.incomeManager.CanAfford(PerkPrice) && !isHidden)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public abstract void ApplyPerk();
}
