using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleFaller : MonoBehaviour
{
    private float randSpeed;

    // Start is called before the first frame update
    void Start()
    {
        randSpeed = Random.Range(40.0f, 80.0f);

        float scale = Random.Range(0.2f, 2.5f);
        transform.localScale = new Vector3(scale, scale, 1);
        GetComponent<Image>().color = new Color(1, 0.81f, 0.86f, Random.Range(0.5f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - randSpeed * Time.deltaTime, 0);
        if (transform.position.y < -150)
        {
            Destroy(gameObject);
        }
    }
}
