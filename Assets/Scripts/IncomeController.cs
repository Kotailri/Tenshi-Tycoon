using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class IncomeController : MonoBehaviour
{
    public long baseRate;

    [Space(15.0f)]
    public TextMeshProUGUI rateText;
    public TextMeshProUGUI ringText;

    [Space(10.0f)]
    public List<ShopItem> shopItems = new();
    public List<Upgrade> upgradeItems = new();

    public GameObject ShopContent;
    public GameObject UpgradeContent;

    public long rings = 0;
    public long currentRate = 1;

    public class Item
    {
        public string itemName;
        public long rate;
        public long price;
        public Item(string _name, long _rate, long _price)
        {
            itemName = _name;
            rate = _rate;
            price = _price;
        }
    }

    public List<Item> itemInfo = new List<Item> 
    {
        //       Name                                Rate                      Price
        new Item("Ceiling Fan",                        1,                      100),
        new Item("Head Wing Feather",                 10,                     1000),
        new Item("Poofy Cloud",                       80,                   11_000),    
        new Item("Amai Light Stick",                 470,                  120_000),
        new Item("Juice",                           2600,                1_300_000),
        new Item("Parfait",                       14_000,               14_000_000),
        new Item("Golden Frying Pan",             78_000,              200_000_000),
        new Item("Hydrangea Bouquet",            440_000,            3_300_000_000),
        new Item("Maki Figurine",              2_600_000,           51_000_000_000),
        new Item("Pair of Trucks",            16_000_000,          750_000_000_000),
        new Item("Tenshi Daki",              100_000_000,       10_000_000_000_000),
        new Item("Tenshi Wing",              650_000_000,      140_000_000_000_000),
        new Item("Amelia Watson Hat",      4_300_000_000,    1_700_000_000_000_000),
        new Item("Onion Ring Factory",    29_000_000_000,   21_000_000_000_000_000),
        new Item("Tenshi Hug",           210_000_000_000,  260_000_000_000_000_000)
    };

    void Start()
    {
        Global.incomeController = this;

        rateText.text = Global.LongToString(currentRate) + " tr/s";
        ringText.text = Global.LongToString(rings);
        
        int currId = 0;
        int currUpgradeId = 0;

        float ypos = -15f;
        float yposUpgrade = -60f;

        foreach (Transform t in ShopContent.transform)
        {
            if (t.TryGetComponent(out ShopItem item))
            {
                if (currId >= itemInfo.Count)
                {
                    t.localScale = Vector3.zero;
                    continue;
                }

                t.name = "[" + currId.ToString() + "] " + itemInfo[currId].itemName;
                item.ShopItemId = currId;
                item.SetShopItemName(itemInfo[currId].itemName);
                item.SetShopItemPrice(itemInfo[currId].price);
                item.SetShopItemRate(itemInfo[currId].rate);
                shopItems.Add(item);

                t.position = new Vector3(t.position.x, ypos, t.position.z);
                ypos -= 90f;
                currId++;
            }
        }

        foreach (Transform t in UpgradeContent.transform)
        {
            if (t.TryGetComponent(out Upgrade upgrade))
            {
                t.name = "[" + currUpgradeId.ToString() + "] " + upgrade.upgradeName;
                upgrade.upgradeId = currUpgradeId;
                upgradeItems.Add(upgrade);
                t.position = new Vector3(t.position.x, yposUpgrade, t.position.z);
                yposUpgrade -= 180f;
                currUpgradeId++;
            }
        }

        InvokeRepeating(nameof(UpdateIncome), 0f, 0.1f);
    }

    public void UpdateIncome()
    {
        ringText.text = Global.LongToString(rings);
        long calculatedRate = baseRate;
        foreach (ShopItem item in shopItems)
        {
            calculatedRate += item.GetFullRate();
        }
        currentRate = calculatedRate;
        rings += (calculatedRate/10);
        rateText.text = Global.LongToString(currentRate) + " tr/s";
        ringText.text = Global.LongToString(rings);
    }

    public bool DecreaseRings(long _rings)
    {
        if (rings - _rings >= 0)
        {
            rings -= _rings;
            UpdateIncome();
            return true;
        }
        return false;
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            LoadData();
        }
    }

    //==================== DATA SAVING ========================

    [System.Serializable]
    public class SerializableList<T>
    {
        public List<T> list;
    }

    [System.Serializable]
    public class SaveFile
    {
        public long saved_rings;
        public long saved_rate;

        public List<long> sav_shopItemCount;
        public List<int> sav_upgradeItemLevels;

        public SaveFile(long _saved_rings, long _saved_rate, List<long> _sav_shopItemCount, List<int> _sav_upgradeItemLevels)
        {
            sav_shopItemCount = new();
            sav_upgradeItemLevels = new();

            saved_rings = _saved_rings;
            saved_rate = _saved_rate;

            foreach (long n in _sav_shopItemCount)
            {
                sav_shopItemCount.Add(n);
            }

            foreach(int n in _sav_upgradeItemLevels)
            {
                sav_upgradeItemLevels.Add(n);
            }
        }
    }

    public void LoadData()
    {
        using (StreamReader r = new StreamReader("./save_file.json"))
        {
            string json = r.ReadToEnd();
            SaveFile sav = JsonUtility.FromJson<SaveFile>(json);

            rings = sav.saved_rings;
            currentRate = sav.saved_rate;

            for (int i = 0; i < shopItems.Count; i++)
            {
                shopItems[i].ShopItemCount = sav.sav_shopItemCount[i];
                shopItems[i].ShopItemCountText.text = sav.sav_shopItemCount[i].ToString();
            }

            for (int i = 0; i < upgradeItems.Count; i++)
            {
                if (upgradeItems[i].TryGetComponent(out ScalingUpgrade upgrade1))
                {
                    upgrade1.SetLevel(sav.sav_upgradeItemLevels[i]);
                }
                else if (upgradeItems[i].TryGetComponent(out Upgrade upgrade2))
                {
                    if (sav.sav_upgradeItemLevels[i] == 1)
                    {
                        upgradeItems[i].SetAsSold();
                    }
                }

            }
        }
    }

    public void SaveData()
    {
        long sav_rate = currentRate;
        long sav_rings = rings;

        List<long> sav_shopItemCount = new();
        List<int> sav_upgradeItemLevels = new();

        for (int i = 0; i < shopItems.Count; i++)
        {
            sav_shopItemCount.Add(shopItems[i].ShopItemCount);
        }

        for (int i = 0; i < upgradeItems.Count; i++)
        {
            if (upgradeItems[i].TryGetComponent(out ScalingUpgrade upgrade1))
            {
                sav_upgradeItemLevels.Add(upgrade1.GetLevel());
            }
            else if (upgradeItems[i].TryGetComponent(out Upgrade upgrade2))
            {
                if (upgrade2.isSold)
                {
                    sav_upgradeItemLevels.Add(1);
                }
                else
                {
                    sav_upgradeItemLevels.Add(0);
                }
            }
        }

        SaveFile fileToSave = new SaveFile(sav_rings, sav_rate, sav_shopItemCount, sav_upgradeItemLevels);
        string json = JsonUtility.ToJson(fileToSave);
        System.IO.File.WriteAllText("./save_file.json", json);
        print("Saved file!");
    }
}
