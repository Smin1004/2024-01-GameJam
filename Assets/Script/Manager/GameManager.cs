using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float musicVolume;
    public float sfxVolume;

    public string upKey;
    public string downKey;
    public string leftKey;
    public string rightKey;
    public string swapKey;

    public GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if(_instance == null)
                {
                    GameObject temp = new GameObject(typeof(GameManager).Name);
                    _instance = temp.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    public GameManager _instance;
}
