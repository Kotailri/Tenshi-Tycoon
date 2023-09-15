using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoostEffect : MonoBehaviour
{
    public Transform boostTextTransform;
    public Transform boostParticles;

    [Space(10.0f)]
    public TextMeshProUGUI boostText;

    private float timer = 0.0f;
    private bool timerRunning = false;

    private void Awake()
    {
        boostTextTransform.localScale = Vector3.zero;
        boostParticles.localScale = Vector3.zero;
    }

    public void StartBoostTimer(float time)
    {
        timer = time;
        timerRunning = true;

        Global.incomeManager.EnableBoost();

        boostTextTransform.localScale = Vector3.one;
        boostParticles.localScale = Vector3.one;
    }

    private void EndBoostTimer()
    {
        timerRunning = false;

        boostTextTransform.localScale = Vector3.zero;
        boostParticles.localScale = Vector3.zero;

        Global.incomeManager.DisableBoost();
    }

    private void Update()
    {
        if (timerRunning)
        {
            timer -= Time.deltaTime;

            if (timer <= 0.0f)
            {
                EndBoostTimer();
            }

            int mins = (int)(timer / 60);
            int secs = (int)(timer % 60);
            boostText.text = $"{Global.incomeManager.boostMultiplier}x Boost Time! {mins}:{secs.ToString().PadLeft(2, '0')} remaining";
        }
    }
}
