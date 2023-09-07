using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTier
{
    public string upgradeName;
    public string upgradeDescription;
    public long price;
    public int level;

    public UpgradeTier(string _upgradeName, string _upgradeDescription, long _price, int level)
    {
        upgradeName = _upgradeName;
        upgradeDescription = _upgradeDescription;
        price = _price;
    }
}

public interface ProgressLock
{
    public bool checkLock();
    public void updateLock();
}

public interface ScalingUpgrade
{
    public void SetLevel(int level);
    public int GetLevel();
}

public abstract class Upgrade : MonoBehaviour
{
    public long price;
    public bool isSold = false;
    public string upgradeName;
    public string upgradeInfo;

    [Space(10.0f)]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI infoText;

    [Space(10.0f)]
    public int upgradeId;

    public abstract void BuyUpgrade();
    public abstract void AddButtonListener();

    public void Update()
    {
        if (!isSold)
        {
            UpdateAffordability();
        }
    }

    public virtual void SetAsSold()
    {
        isSold = true;
    }

    public void UpdateName(string _text)
    {
        upgradeName = _text;
        nameText.text = _text;
    }

    public void UpdatePrice(long _price)
    {
        price = _price;
        priceText.text = _price.ToString() + " rings";
    }
    public void UpdatePrice(string _text)
    {
        priceText.text = _text;
    }

    public void UpdateInfo(string _text)
    {
        upgradeInfo = _text;
        infoText.text = _text;
    }

    public void UpdateHoverText(string _text)
    {
        if (TryGetComponent(out HasHoverText hoverText))
        {
            hoverText.text = _text;
        }
        else
        {
            print(name + " upgrade has no hover component");
        }

    }

    public void UpdateAffordability()
    {

        if (Global.incomeController.rings >= price)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
