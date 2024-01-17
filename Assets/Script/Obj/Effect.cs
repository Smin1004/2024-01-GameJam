using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private float time;

    void Start()
    {
        Invoke("Destroy", time);
    }

    private void Destroy(){
        Destroy(gameObject);
    }
}
