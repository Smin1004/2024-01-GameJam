using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveGround_Base : MonoBehaviour
{
    public Vector2Int curPos;
    public bool isUse;
    public int index;

    public abstract void Use();
}
