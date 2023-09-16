using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trophy : IncomeUpdateListener
{
    [Header("Announcement")]
    public string announceText;

    private BigInteger goal;

    [Header("Bar")]
    public TextMeshProUGUI percentText;
    public Image bar;
    public Image background;

    [Space(5.0f)]
    public TextMeshProUGUI goalText;

    private bool achieved = false;

    [Space(5.0f)]
    public Trophy requiredTrophy;

    private void Start()
    {
        percentText.text = "0%";
        bar.fillAmount = 0;

        if (requiredTrophy != null)
        {
            background.color = Color.black;
        }
    }

    public bool IsAchieved()
    {
        return achieved;
    }

    public void SetCompleted()
    {
        achieved = true;
        bar.fillAmount = 100;
        background.color = Color.green;
        percentText.text = "Completed!";
        Global.announcer.CreateAnnouncement(announceText);
    }

    public override void OnIncomeUpdate()
    {
        if (requiredTrophy == null || (requiredTrophy != null && requiredTrophy.IsAchieved()))
        {
            background.color = Color.white;

            if (Global.incomeManager.totalRings > goal)
            {
                SetCompleted();
            }

            if (!achieved)
            {
                UpdateGoal();
            }
        }
    }

    public void SetGoal(BigInteger _goal)
    {
        goal = _goal;
        goalText.text = Global.LongToString(goal) + " Total Rings";
    }

    public void UpdateGoal()
    {
        string currentStr = Global.incomeManager.totalRings.ToString();
        string goalStr = goal.ToString();

        int currentLen = currentStr.Length;
        int goalLen = goalStr.Length;

        if (goalLen - currentLen <= 3 && currentLen >= 3)
        {
            currentStr = currentStr.PadLeft(goalLen, '0');
            currentStr = currentStr[..3];
            goalStr = goalStr[..3];
            
            float percent = float.Parse(currentStr) / float.Parse(goalStr);
            percentText.text = $"{percent * 100:n0}%";
            bar.fillAmount = percent;

            if (percent >= 1.0f)
            {
                SetCompleted();
            }
        }
    }
}
