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

    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode weaponKey;

    private static InputManager _instance;
    public static InputManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InputManager>();
                if (_instance == null)
                {
                    GameObject temp = new GameObject(typeof(InputManager).Name);
                    _instance = temp.AddComponent<InputManager>();
                    DontDestroyOnLoad(temp);
                }
                else DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(upKey)) UpKey?.Invoke();
        if (Input.GetKeyDown(downKey)) DownKey?.Invoke();
        if (Input.GetKeyDown(leftKey)) LeftKey?.Invoke();
        if (Input.GetKeyDown(rightKey)) RightKey?.Invoke();
        if (Input.GetKeyDown(weaponKey)) SwapKey?.Invoke();
    }
}
