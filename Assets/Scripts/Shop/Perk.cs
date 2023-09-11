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

    private void OnEnable()
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

        HidePerk();
    }

    public void SetInfo(string PerkName, string PerkDescription, long PerkPrice)
    {
        this.PerkDescription = PerkDescription;
        this.PerkName = PerkName;
        this.PerkPrice = PerkPrice;
    }

    public void HidePerk()
    {
        nameText.text  = "??????";
        priceText.text = "????? rings";
        infoText.text  = "Perk Locked";
        GetComponent<Image>().color = Color.black;
    }

    public void UnhidePerk()
    {
        nameText.text = PerkName;
        priceText.text = Global.LongToString(PerkPrice) + " rings";
        infoText.text = PerkDescription;
        GetComponent<Image>().color = Color.white;
    }

    public void PurchasePerk()
    {
        if (Global.incomeManager.CanAfford(PerkPrice) && !purchased)
        {
            LoadPerk();
        }
    }

    public bool IsPurchased()
    {
        return purchased;
    }

    public void LoadPerk()
    {
        purchased = true;
        GetComponent<Button>().interactable = false;
        GetComponent<Image>().color = Color.green;
        priceText.text = "Purchased!";
        ApplyPerk();
    }

    public override void OnIncomeUpdate()
    {
        if (purchased)
            return;

        if (Global.incomeManager.CanAfford(PerkPrice))
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
