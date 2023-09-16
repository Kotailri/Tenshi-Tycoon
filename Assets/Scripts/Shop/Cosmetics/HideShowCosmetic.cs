using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowCosmetic : Cosmetic
{
    public Transform obj;
    private Vector3 defaultScale;

    private void Start()
    {
        defaultScale = obj.localScale;
        obj.localScale = Vector3.zero;
    }

    public override void ToggleOff()
    {
        obj.localScale = Vector3.zero;
    }

    public override void ToggleOn()
    {
        obj.localScale = defaultScale;
    }
}
