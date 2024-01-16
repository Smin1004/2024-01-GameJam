using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyText : MonoBehaviour
{
    void Start()
    {
        InputManager.instance.Up += () => Debug.Log("Up");
    }
}
