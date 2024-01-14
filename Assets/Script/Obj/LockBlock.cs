using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBlock : Obj_Base
{
    public override void UseObj()
    {
        if(Player.Instance.isKey <= 0) return;

        Player.Instance.isKey--;

        MoveManager.Instance.InOutIndex(curPos, Define.MapType.Obj);
        Destroy(gameObject);
    }
}
