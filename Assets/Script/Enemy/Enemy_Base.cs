using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Enemy_Base : Mob_Base
{
    [Header("Enemy_Base")]
    [SerializeField] protected bool cantDie;
    [SerializeField] private GameObject effect;
    [SerializeField] private bool isAttack;
    public bool isNotCheck;
    public bool isWeapon;

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
                Debug.Log(this.gameObject);
                MoveManager.Instance.curPlayer.Damage();
                isAttack = false;
            }else isAttack = true;
        }
        else isAttack = false;
    }

    public virtual void Hit(Vector2Int plusPos)
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        dieAction?.Invoke();
    }

    protected override void DieDestroy()
    {
        AudioManager.Instance.PlaySFX("EnemyDead");
        Rigidbody2D rigid = gameObject.AddComponent<Rigidbody2D>();
        rigid.velocity = new Vector3(Random.Range(-0.5f, 0.5f),1,0) * 7;
        rigid.AddTorque(15,ForceMode2D.Impulse);
        StartCoroutine(DeadMove());
    }
    private IEnumerator DeadMove()
    {   
        yield return new WaitForSeconds(10);
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
