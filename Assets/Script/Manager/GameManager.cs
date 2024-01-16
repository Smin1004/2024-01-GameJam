using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private Player curPlayer;
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
}
