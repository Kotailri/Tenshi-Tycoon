using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemAchivementContainer
{
    public long amount;
    public string text;
}

public class ItemAchivement : MonoBehaviour
{
    private List<bool> flags = new();
    private int tracker = 0;

    public List<ItemAchivementContainer> achivements = new();

    private void Start()
    {
        for (int i = 0; i < achivements.Count; i++)
        {
            flags.Add(false);
        }
    }

    public void CheckItemAchivements(long count)
    {
        if (tracker >= achivements.Count)
        {
            return;
        }

        if (!flags[tracker] && count >= achivements[tracker].amount)
        {
            Global.announcer.CreateAnnouncement(achivements[tracker].text);
            flags[tracker] = true;
            tracker++;
        }
    }
}
