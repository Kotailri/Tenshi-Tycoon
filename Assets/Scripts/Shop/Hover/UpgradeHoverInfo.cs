public class UpgradeHoverInfo : HasHoverInfo
{
    private ShopUpgrade shopUpgrade;

    private void Start()
    {
        shopUpgrade = GetComponent<ShopUpgrade>();
    }

    public override string GetInfoText()
    {
        return shopUpgrade.GetRequirementText() + $" You have {Global.itemManager.FindShopItem(shopUpgrade.targetItemName).GetCount()}";
    }
}
