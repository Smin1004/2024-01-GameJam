using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopObj : Obj_Base
{
   public override void UseObj()
    {
        MoveManager.Instance.curPlayer.redirecting = new Vector2Int(0,0);
    } 
}
