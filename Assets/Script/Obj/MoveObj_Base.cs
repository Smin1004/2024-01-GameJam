using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveObj_Base : Obj_Base
{
    public int index;

    [SerializeField] private bool notMove;
    [SerializeField] private int curtiming;
    [SerializeField] private int endtiming;

    public void NextTiming()
    {
        if (notMove) return;

        curtiming++;

        if(curtiming < endtiming) return;

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            MoveManager.Instance.InOutIndex(curPos, Define.MapType.Obj, index);
        }
        else
        {
            gameObject.SetActive(true);
            if(MoveManager.Instance.MoveCheck(curPos, new(0,0), false, true) == 2) UseObj();
            MoveManager.Instance.InOutIndex(curPos, Define.MapType.Obj);
        }
    }
}
