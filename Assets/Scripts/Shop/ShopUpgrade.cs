using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade
{
    public string name;
    public string info;
    public string requirements;
    public BigInteger price;

    public Upgrade(string _name, string _info, BigInteger _price, string _requirements)
    {
        name = _name;
        info = _info;
        requirements = _requirements;
        price = _price;
    }
}

public class ShopUpgrade : IncomeUpdateListener
{
    [HideInInspector]
    public TextMeshProUGUI upgradeNameText;
    [HideInInspector]
    public TextMeshProUGUI upgradePriceText;
    [HideInInspector]
    public TextMeshProUGUI upgradeInfoText;

    [HideInInspector]
    public string targetItemName;
    [HideInInspector]
    public ShopItem targetShopItem;

    private string upgradeName;
    private string upgradeInfo;
    private BigInteger upgradePrice;
    private string requirementText;

    public List<Upgrade> upgradeInfoList = new();

    private bool locked = false;
    private bool requirementLocked = false;
    private int currentLevel = 0;
    private int maxLevel = 5;


    private void OnEnable()
    {
        foreach (Transform t in transform)
        {
            if (t.TryGetComponent(out TextMeshProUGUI tmp))
            {

                if (t.gameObject.name == "Name")
                {
                    upgradeNameText = tmp;
                    continue;
                }

                if (t.gameObject.name == "Price")
                {
                    upgradePriceText = tmp;
                    continue;
                }

                if (t.gameObject.name == "Info")
                {
                    upgradeInfoText = tmp;
                    continue;
                }
            }

        }

        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (!locked)
                PurchaseUpgrade();
        });
    }

    public void SetUpgrade(Upgrade up)
    {
        SetName(up.name);
        SetInfo(up.info);
        SetPrice(up.price);
        SetRequirementText(up.requirements);
    }

    public override void OnIncomeUpdate()
    {
        if (!locked)
        {
            UpdateRequirementCheck();
        }
    }

    public virtual bool CheckRequirements()
    {
        return true;
    }

    public virtual void ApplyUpgrade()
    {
        IncreaseLevel();
        GetComponent<HasHoverInfo>().Refresh();
    }

    public void UpdateRequirementCheck()
    {
        bool affordable = Global.incomeManager.CanAfford(upgradePrice);

        if (CheckRequirements())
        {
            requirementLocked = false;
        }
        else
        {
            requirementLocked = true;
        }

        if (CheckRequirements() && affordable)
        {
            GetComponent<Image>().color = Color.white;
            GetComponent<Button>().interactable = true;
        }
        else if (!CheckRequirements() && affordable)
        {
            GetComponent<Image>().color = new Color(255f/255f, 189f/255f, 128f/255f, 1);
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
            GetComponent<Button>().interactable = false;
        }
    }

    private void PurchaseUpgrade()
    {
        if (Global.incomeManager.CanAfford(upgradePrice) && !locked)
        {
            AudioManager.instance.PlaySound("buy");
            Global.incomeManager.SubtractRings(upgradePrice);
            ApplyUpgrade();
        }
    }

    public void IncreaseLevel()
    {
        currentLevel++;
        if (currentLevel == maxLevel)
        {
            GetComponent<Image>().color = Color.green;
            upgradePriceText.text = "Upgrade Maxed!";
            GetComponent<Button>().interactable = false;

            locked = true;
        }
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            ApplyUpgrade();
        }
    }

    public void UpdateMaxLevel(int _level)
    {
        maxLevel = _level;
    }

    public bool GetRequirementLock()
    {
        return requirementLocked;
    }

    public int GetLevel()
    {
        return currentLevel;
    }

    public string GetName()
    {
        return upgradeName;
    }

    public string GetInfo()
    {
        return upgradeInfo;
    }

    public string GetRequirementText()
    {
        return requirementText;
    }

    public void SetName(string _name)
    {
        upgradeName = _name;
        upgradeNameText.text = _name;
    }

    public void SetPrice(BigInteger _price)
    {
        upgradePrice = _price;
        upgradePriceText.text = Global.LongToString(_price) + " rings";
    }

    public void SetInfo(string _info)
    {
        upgradeInfo = _info;
        upgradeInfoText.text = _info;
    }

    public void SetRequirementText(string _req)
    {
        requirementText = _req;
    }
}
