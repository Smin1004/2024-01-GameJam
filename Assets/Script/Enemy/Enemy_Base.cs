using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Enemy_Base : Mob_Base
{
    [Header("Enemy_Base")]
    [SerializeField] protected bool cantDie;
    private bool isAttack;
    public bool isNotCheck;
    public bool isHend;

    protected override void Start()
    {
        base.Start();
        transform.LookAt(transform.position + Vector3.back);
    }

    public void PlayerCheck()
    {
        int action = MoveManager.Instance.EnemyAttackCheck(curPos);

        if (action == 2)
        {
            if (isAttack)
            {
                //Player.Instance.Damage();
                isAttack = false;
            }else isAttack = true;
        }
        else isAttack = false;
    }

    public virtual void Hit(Vector2Int plusPos)
    {
        Debug.Log("die");
        dieAction?.Invoke();
        // Vector3 movePos = new();

        // if (plusPos == Vector2Int.up) movePos = Vector3.forward;
        // if (plusPos == Vector2Int.down) movePos = Vector3.back;
        // if (plusPos == Vector2Int.right) movePos = Vector3.right;
        // if (plusPos == Vector2Int.left) movePos = Vector3.left;

        // if (cantMove) return;

        // int action = MoveManager.Instance.MoveCheck(curPos, plusPos, true);

        // switch (action)
        // {
        //     case 0: curPos = curPos + plusPos; StartCoroutine(MoveEnemy(movePos, 0.2f)); break;
        //     case 1: if (!cantDie) dieAction?.Invoke(); break;
        //     case 2: break;
        // }
    }

    protected override void DieDestroy()
    {
        Managers.Resource.Destroy(gameObject);
    }

    protected IEnumerator MoveEnemy(Vector3 direction, float sec)
    {
        float elapsedTime = 0;
        Vector3 origPos = transform.position;
        Vector3 targetPos = origPos + direction;

        while (elapsedTime < sec)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, elapsedTime / sec);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
    }
}
