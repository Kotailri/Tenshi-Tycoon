using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverBox : MonoBehaviour
{
    public TextMeshProUGUI boxText;

    // Start is called before the first frame update
    void Start()
    {
        Global.hoverBox = this;
        transform.localScale = Vector3.zero;
    }

    public void ShowText(string text)
    {
        boxText.text = text;
        transform.localScale = Vector3.one;
    }

    public void HideText()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition + new Vector3(170,55,0);
    }
}
