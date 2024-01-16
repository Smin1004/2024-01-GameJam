using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    float sponTime;
    void Update()
    {
        transform.position += new Vector3(0, 1, 0) * Time.deltaTime * 2;
        sponTime += Time.deltaTime;
        if (sponTime > 10) Destroy(gameObject);
    }
}
