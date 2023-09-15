using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    public GameObject circle;
    public Transform leftMax;
    public Transform rightMax;

    private void Start()
    {
        StartCoroutine(LoopEffect());
    }

    private void SpawnCircle()
    {
        float randX = Random.Range(leftMax.position.x, rightMax.position.x);
        GameObject c = Instantiate(circle, new Vector3(randX, transform.position.y, 0), Quaternion.identity);
        c.transform.SetParent(transform, true);
    }

    private IEnumerator LoopEffect()
    {
        SpawnCircle();
        yield return new WaitForSeconds(Random.Range(1f, 3.0f));
        StartCoroutine(LoopEffect());
    }
}
