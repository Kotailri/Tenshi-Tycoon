using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RGBEffect : MonoBehaviour
{
    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
        StartAnim();
    }

    public void StartAnim()
    {
        StartCoroutine(LoopAnim());
    }

    private IEnumerator LoopAnim()
    {
        LeanTween.color(img.rectTransform, new Color(1, 0, 0, 0.3f), 5).setEaseInOutExpo();
        yield return new WaitForSecondsRealtime(5);
        LeanTween.color(img.rectTransform, new Color(0, 1, 0, 0.3f), 5).setEaseInOutExpo();
        yield return new WaitForSecondsRealtime(5);
        LeanTween.color(img.rectTransform, new Color(0, 0, 1, 0.3f), 5).setEaseInOutExpo();
        yield return new WaitForSecondsRealtime(5);
        StartCoroutine(LoopAnim());
    }
}
