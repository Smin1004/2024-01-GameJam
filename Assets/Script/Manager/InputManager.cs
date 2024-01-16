using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public event Action Up;
    public event Action Down;
    public event Action Left;
    public event Action Right;
    public event Action WeaponSwap;
    public event Action PlayerSwap;

    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode weaponKey;
    public KeyCode playerKey;

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
        if (Input.GetKeyDown(upKey)) Up?.Invoke();
        if (Input.GetKeyDown(downKey)) Down?.Invoke();
        if (Input.GetKeyDown(leftKey)) Left?.Invoke();
        if (Input.GetKeyDown(rightKey)) Right?.Invoke();
        if (Input.GetKeyDown(weaponKey)) WeaponSwap?.Invoke();
        if (Input.GetKeyDown(playerKey)) PlayerSwap?.Invoke();
    }
}
