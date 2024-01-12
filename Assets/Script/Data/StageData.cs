using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/StageData", order = 1), Serializable]
public class StageData : ScriptableObject
{
    public List<MapData> battleMapData = new();
    public List<GameObject> mapTile = new();
    public List<Obj_Base> mapObj = new();
    public List<Enemy_Base> mapEnemy = new();

    public int stageCount;
}