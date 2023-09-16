using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackgroundSwap : Cosmetic
{
    public Sprite newBackgroundImage;
    public SpriteRenderer backgroundImageObject;

    public override void ToggleOff()
    {
        backgroundImageObject.sprite = Global.objectStorage.defaultBackground;
    }

    public override void ToggleOn()
    {
        backgroundImageObject.sprite = newBackgroundImage;
    }
}
