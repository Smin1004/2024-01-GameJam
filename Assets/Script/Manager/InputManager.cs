using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public event Action UpKey;
    public event Action DownKey;
    public event Action LeftKey;
    public event Action RightKey;
    public event Action SwapKey;
    private void Update()
    {
        var result = Enum.Parse(typeof(KeyCode), "W");
    }
}
