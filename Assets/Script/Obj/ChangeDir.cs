using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDir : Obj_Base
{
    [SerializeField] private Vector2Int changePos;

    public override void UseObj()
    {
        Player.Instance.redirecting = changePos;
    }
}
