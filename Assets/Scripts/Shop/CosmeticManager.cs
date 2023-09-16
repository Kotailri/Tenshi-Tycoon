using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticManager : MonoBehaviour
{
    public List<Cosmetic> cosmetics = new();
    public Transform firstPosition;

    private void Awake()
    {
        Global.cosmeticManager = this;
    }

    void Start()
    {
        float yPos = 0.0f;
        foreach (Transform t in transform)
        {
            if (t.TryGetComponent(out Cosmetic cos))
            {
                cosmetics.Add(cos);
                t.position = new Vector3(firstPosition.position.x, firstPosition.position.y - yPos, firstPosition.position.z);
                yPos += 135;
            }
        }
    }
}
