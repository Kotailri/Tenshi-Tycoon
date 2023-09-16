using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickEffectSpawner : MonoBehaviour
{
    public GameObject clickEffect;
    private readonly float range = 50.0f;

    private void Awake()
    {
        Global.clickEffectSpawner = this;
    }

    public void SpawnClickEffect(long amount, bool special=false)
    {
        GameObject obj = Instantiate(clickEffect, Input.mousePosition + new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0), Quaternion.identity);
        obj.transform.SetParent(transform, true);
        obj.GetComponent<TextMeshProUGUI>().text = "+" + amount.ToString();

        if (special) 
        {
            obj.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
    }
}
