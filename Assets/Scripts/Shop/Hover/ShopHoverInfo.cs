public class ShopHoverInfo : HasHoverInfo
{
    private Item item;

    private void Start()
    {
        item = GetComponent<ShopItem>().item;
    }

    public override string GetInfoText()
    {
        string itemRate = Global.LongToString(item.rate);
        string itemCount = Global.LongToString(item.count);
        string itemRateTotal = Global.LongToString(item.rate * item.count);

        return $"Increases tr/s by {itemRate}\n\nCurrently increasing tr/s by:\n({itemRate} * {itemCount}) = {itemRateTotal} tr/s";
    }
}
