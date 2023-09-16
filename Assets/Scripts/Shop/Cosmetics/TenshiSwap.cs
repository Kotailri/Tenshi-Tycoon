using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TenshiSwap : Cosmetic
{
    public Sprite newTenshiImage;
    public Image tenshiImageObject;

    public override void ToggleOff()
    {
        tenshiImageObject.sprite = Global.objectStorage.defaultTenshi;
    }

    public override void ToggleOn()
    {
        tenshiImageObject.sprite = newTenshiImage;
    }
}
