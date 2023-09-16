using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]

public class ProgressBar : MonoBehaviour
{
    [Header("Bar Setting")]
    public Color BarColor;
    public Color BarCompletionColor;
    public Color BarBackGroundColor;
    public Sprite BarBackGroundSprite;

    private Image bar, barBackground;
    private float barValue;

    public float BarValue
    {
        get { return barValue; }

        set
        {
            value = Mathf.Clamp(value, 0, 100);
            barValue = value;
            UpdateValue(barValue);
        }
    }

    private void Awake()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
        barBackground = GetComponent<Image>();
        barBackground = transform.Find("BarBackground").GetComponent<Image>();
    }

    private void Start()
    {
        bar.color = BarColor;
        barBackground.color = BarBackGroundColor; 
        barBackground.sprite = BarBackGroundSprite;

        UpdateValue(barValue);
    }

    void UpdateValue(float val)
    {
        bar.fillAmount = val / 100;
        if (bar.fillAmount >= 100)
        {
            bar.color = BarCompletionColor;
        }
        else
        {
            bar.color = BarColor;
        }
    }

}
