using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 1.0f);
    }
}
