using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Player : Mob_Base
{
    [SerializeField] private AudioClip moveSound;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip dieSound;
    public text text;
  
    public bool isWeapon;
    public bool allStop;

    [HideInInspector] public Vector2Int redirecting;
    private bool isMove;

    public void Damage()
    {
        DieDestroy();
    }

    protected override void DieDestroy()
    {
        Managers.Sound.Play(dieSound);
        allStop = true;
        Rigidbody2D rigid = gameObject.AddComponent<Rigidbody2D>();
        rigid.velocity = new Vector3(Random.Range(-0.5f, 0.5f),1,0) * 7;
        rigid.AddTorque(15,ForceMode2D.Impulse);
        StartCoroutine(DeadMove());
    }
    private IEnumerator DeadMove()
    {   
        yield return new WaitForSeconds(2);
        MoveManager.Instance.isEnd = true;
    }

    public IEnumerator Event()
    {
        allStop = true;
        if(text.m() == 0) DieDestroy();
        anim.Play("Player_Event");
        yield return new WaitForSeconds(0.5f);
        if(isWeapon) anim.Play("Player_Weapon_Idle");
        else anim.Play("Player_Idle");
        allStop = false;
    }

    public bool StopCheck()
    {
        if (allStop || isMove) return true;
        else return false;
    }

    public void Move(Vector2 nextPos)
    {
        if (allStop || isMove) return;

        isMove = true;
        CheckMove(nextPos);
    }

    private void CheckMove(Vector2 movePos)
    {
        redirecting = Vector2Int.RoundToInt(movePos);
        int action = MoveManager.Instance.MoveCheck(curPos, Vector2Int.RoundToInt(movePos));

        switch (action)
        {
            case 0: curPos = Vector2Int.RoundToInt(curPos + movePos); StartCoroutine(MovePlayer(movePos, 0.2f)); break;
            case 1: TrunEnd(movePos); break;
            case 2: StartCoroutine(Attack(movePos)); break;
        }
    }

    void TrunEnd(Vector2 direction)
    {
        isMove = false;
        if(text.m() == 0) DieDestroy();
        Anim(direction);
        MoveManager.Instance.NextTiming();
    }

    private IEnumerator Attack(Vector2 direction)
    {
        Managers.Sound.Play(attackSound);
        curPos = Vector2Int.RoundToInt(curPos + direction);
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(MovePlayer(direction, 0.2f, false));
        TrunEnd(direction);
    }

    private IEnumerator MovePlayer(Vector2 direction, float sec, bool isCheck = true)
    {
        float elapsedTime = 0;
        Vector2 origPos = transform.position;
        Vector2 targetPos = origPos + direction;

        Anim(direction);

        while (elapsedTime < sec)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, elapsedTime / sec);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        if(isCheck) CheckMove(redirecting);
        Anim(direction);
    }

    private void Anim(Vector2 dir)
    {
        if (isMove)
        {
            if (dir == Vector2.up)
            {
                if (!isWeapon) anim.Play("Player_MoveUp");
                else anim.Play("Player_Weapon_MoveUp");
            }
            else if (dir == Vector2.left)
            {
                if (!isWeapon) anim.Play("Player_MoveLeft");
                else anim.Play("Player_Weapon_MoveLeft");
            }
            else if (dir == Vector2.down)
            {
                if (!isWeapon) anim.Play("Player_MoveDown");
                else anim.Play("Player_Weapon_MoveDown");
            }
            else if (dir == Vector2.right)
            {
                if (!isWeapon) anim.Play("Player_MoveRight");
                else anim.Play("Player_Weapon_MoveRight");
            }
        }
        else
        {
            if (dir == Vector2.up)
            {
                if (!isWeapon) anim.Play("Player_Idle_UP");
                else anim.Play("Player_Weapon_Idle_UP");
            }
            else if (dir == Vector2.left)
            {
                if (!isWeapon) anim.Play("Player_Idle_Left");
                else anim.Play("Player_Weapon_Idle_Left");
            }
            else if (dir == Vector2.down)
            {
                if (!isWeapon) anim.Play("Player_Idle");
                else anim.Play("Player_Weapon_Idle");
            }
            else if (dir == Vector2.right)
            {
                if (!isWeapon) anim.Play("Player_Idle_Right");
                else anim.Play("Player_Weapon_Idle_Right");
            }
        }
    }
}
