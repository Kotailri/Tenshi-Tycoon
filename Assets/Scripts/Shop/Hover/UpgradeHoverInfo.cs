public class UpgradeHoverInfo : HasHoverInfo
{
    private ShopUpgrade shopUpgrade;

    private void Start()
    {
        shopUpgrade = GetComponent<ShopUpgrade>();
    }

    public override string GetInfoText()
    {
        if (shopUpgrade.GetRequirementLock())
        {
            return shopUpgrade.GetRequirementText() + $" You have {Global.itemManager.FindItem(shopUpgrade.targetItemName).count}";
        }
        else
        {
            return "";
        }

        
    }
}
