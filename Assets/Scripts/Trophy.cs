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

    public void SetCompleted(bool withAnnouncement=true)
    {
        achieved = true;
        bar.fillAmount = 100;
        background.color = Color.green;
        percentText.text = "Completed!";

        if (withAnnouncement)
        {
            Global.announcer.CreateAnnouncement(announceText);
            AudioManager.instance.PlaySound("success");
        }
            
    }

    public override void OnIncomeUpdate()
    {
        if (IsAchieved() || Global.reloading) { return; }

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
        goalText.text = Global.LongToString(goal) + " Rings";
    }

    public void UpdateGoal()
    {
        string currentStr = Global.incomeManager.rings.ToString();
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
