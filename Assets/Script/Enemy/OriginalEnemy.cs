using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalEnemy : Enemy_Base
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void DieDestroy()
    {
        MoveManager.Instance.DestroyEnemy(curPos);
        base.DieDestroy();
    }
}
