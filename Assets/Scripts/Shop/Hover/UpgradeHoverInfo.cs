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
            return shopUpgrade.GetRequirementText() + $" You have {Global.itemManager.FindShopItem(shopUpgrade.targetItemName).GetCount()}";
        }
        else
        {
            return "";
        }
    }
}
