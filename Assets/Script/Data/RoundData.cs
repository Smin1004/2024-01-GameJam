using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/RoundData", order = 1), Serializable]
public class RoundData : ScriptableObject
{
    private static RoundData _instance = null;
    public static RoundData Instance => _instance;

    public int stageIndex;
    public int mapIndex;
    public bool isEasy = false;
    public bool isTop = false;
    public bool isMute = true;


    public void Reset(int index)
    {
        stageIndex = index;
        mapIndex = 0;
    }

    public void InitData()
    {
        _instance = this;
    }
}
