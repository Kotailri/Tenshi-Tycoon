public class Item
{
    public string shopname;

    public long price;
    public long rate;
    public long count;
    public long limit;

    public Item(string _name, long _rate, long _price)
    {
        shopname = _name;
        price = _price;
        rate = _rate;
        limit = 100;

        count = 0;
    }

    public override string ToString()
    {
        return $"{shopname} : {rate} tp/s";
    }

    public long GetIncome()
    {
        return rate * count;
    }

    public void SetShopItemName(string _name)
    {
        shopname = _name;
    }

    public void SetShopItemPrice(long _price)
    {
        price = _price;
    }

    public void SetShopItemRate(long _rate)
    {
        rate = _rate;
    }

    public void SetShopItemCount(long _count)
    {
        count = _count;
    }

    public void IncrementItemCount()
    {
        count++;
    }
}
