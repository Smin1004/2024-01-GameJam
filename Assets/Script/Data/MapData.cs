using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MapData", menuName ="Game/MapData", order = 0), Serializable]
public class MapData : ScriptableObject
{
    public int life;
    public TextAsset groundMap;
    public TextAsset enemyMap;
    public TextAsset objMap;
    public int players;
}
