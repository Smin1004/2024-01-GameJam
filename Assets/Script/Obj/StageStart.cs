using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStart : Obj_Base
{
    public override void UseObj()
    {
        MoveManager.Instance.curPlayer.allStop = true;
        
    }
}
