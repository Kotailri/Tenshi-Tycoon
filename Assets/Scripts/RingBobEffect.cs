using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBobEffect : MonoBehaviour
{
    public float amplitude;
    public float duration;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;

        LeanTween.moveY(gameObject, originalPosition.y + amplitude, duration / 2f)
            .setEase(LeanTweenType.easeInOutSine)
            .setLoopPingPong()
            .setRepeat(-1);
    }
}
