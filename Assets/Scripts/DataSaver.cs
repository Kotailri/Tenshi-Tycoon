using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataSaver : MonoBehaviour
{
    private readonly string filename = "./sav.json";

    private void SaveData()
    {
        CheckSaveFile();

        BigInteger ringCount = Global.incomeManager.rings;
        List<long> itemCounts = new();
        List<int> upgradeLevels = new();
        List<bool> perkUnlocks = new();
        List<bool> trophies = new();
        List<bool> cosmeticUnlocks = new();

        // Item Count
        foreach (ShopItem item in Global.itemManager.shopItems) 
        {
            itemCounts.Add(item.GetCount());
        }

        // Upgrade Levels
        foreach (ShopUpgrade upgrade in Global.upgradeManager.upgradeList)
        {
            upgradeLevels.Add(upgrade.GetLevel());
        }

        // Perk Unlocks
        foreach (Perk perk in Global.perkManager.perkList) 
        {
            perkUnlocks.Add(perk.IsPurchased());
        }

        // Trophy Unlocks
        foreach (Trophy tr in Global.trophyManager.Trophys)
        {
            trophies.Add(tr.IsAchieved());
        }

        // Cosmetic Unlocks
        foreach (Cosmetic cos in Global.cosmeticManager.cosmetics)
        {
            cosmeticUnlocks.Add(cos.IsUnlocked());
        }

        SaveFile fileToSave = new(itemCounts, upgradeLevels, perkUnlocks, trophies, 
            cosmeticUnlocks, ringCount.ToString());
        File.WriteAllText(filename, JsonUtility.ToJson(fileToSave));
    }

    private bool CheckSaveFile()
    {
        if (!File.Exists(filename)) 
        {
            Debug.LogWarning("Save file not found! Creating");
            using (File.Create(filename)) { }
            return false;
        }
        return true;
    }

    private void LoadData()
    {
        if (!CheckSaveFile()) 
        {
            return; 
        }

        Global.reloading = true;
        Global.announcer.LockAnnouncer();

        using StreamReader r = new(filename);
        string json = r.ReadToEnd();
        SaveFile sav = JsonUtility.FromJson<SaveFile>(json);

        Global.incomeManager.rings = BigInteger.Parse(sav.ringCount);

        // Item Count
        for (int i = 0; i < sav.itemCounts.Count; i++)
        {
            Global.itemManager.shopItems[i].SetShopItemCount(sav.itemCounts[i]);
            Global.itemManager.shopItems[i].CheckLimit();
        }

        // Upgrade Levels
        for (int i = 0; i < sav.upgradeLevels.Count; i++)
        {
            Global.upgradeManager.upgradeList[i].SetLevel(sav.upgradeLevels[i]);
        }

        // Perk Unlocks
        for (int i = 0; i < sav.perkUnlocks.Count; i++)
        {
            if (sav.perkUnlocks[i])
            {
                Global.perkManager.perkList[i].LoadPerkFromSave();
            }
        }

        // Trophy Unlocks
        for (int i = 0; i < sav.trophies.Count; i++)
        {
            if (sav.trophies[i])
            {
                Global.trophyManager.Trophys[i].SetCompleted(false);
            }
        }

        // Cosmetic Unlocks
        for (int i = 0;i < sav.cosmeticUnlocks.Count; i++)
        {
            if (sav.cosmeticUnlocks[i])
            {
                Global.cosmeticManager.cosmetics[i].LoadUnlockCosmetic();
            }
        }

        Global.announcer.UnlockAnnouncer();
        Global.reloading = false;
    }

    private void Start()
    {
        Global.InvokeLambda(() => { LoadData(); }, 0.5f);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && Global.debugMode)
        {
            // DEBUG
            File.Delete(filename);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

[System.Serializable]
public class SaveFile
{
    public List<long> itemCounts;
    public List<int> upgradeLevels;
    public List<bool> perkUnlocks;
    public List<bool> trophies;
    public List<bool> cosmeticUnlocks;
    public string ringCount;

    public SaveFile(List<long> itemCounts, List<int> upgradeLevels, List<bool> perkUnlocks, 
        List<bool> trophies, List<bool> cosmeticUnlocks, string ringCount)
    {
        this.itemCounts = itemCounts;
        this.upgradeLevels = upgradeLevels;
        this.ringCount = ringCount;
        this.perkUnlocks = perkUnlocks;
        this.trophies = trophies;
        this.cosmeticUnlocks = cosmeticUnlocks;
    }
}
