using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StartingScreen : MonoBehaviour
{
    public TextMeshProUGUI text;

    private float timeElapsed;
    private float valueToLerp;
    private float delayTime = 3f;

    void Start()
    {
        LeanTween.color(GetComponent<Image>().rectTransform, new Color(0, 0, 0, 0.0f), delayTime)
            .setOnComplete(() => { Destroy(gameObject); });
    }

    private void Update()
    {
        valueToLerp = Mathf.Lerp(1, 0, timeElapsed / delayTime);
        timeElapsed += Time.deltaTime;
        text.color = new Color(text.color.r, text.color.g, text.color.b, valueToLerp);
    }
}
