using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingColour : Cosmetic
{
    public Color newColor;
    public Image ringClickerImage;

    public override void ToggleOff()
    {
        ringClickerImage.color = Global.defaultRingColor;
    }

    public override void ToggleOn()
    {
        ringClickerImage.color = newColor;
    }
}
