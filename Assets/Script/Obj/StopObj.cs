using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopObj : Obj_Base
{
   public override void UseObj()
    {
        MoveManager.Instance.curPlayer.redirecting = Vector2Int.zero;
    } 
}
