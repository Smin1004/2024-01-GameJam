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
    
    public bool isHend;
    public bool allStop;

    [HideInInspector] public Vector2Int redirecting;
    private bool isMove;

    protected override void Start()
    {
        base.Start();
        
    }

    public void Damage()
    {
        //DieDestroy();
    }

    public bool StopCheck(){
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
            case 1: TrunEnd(); break;
            case 2: Attack(); TrunEnd(); break;
        }
    }

    void TrunEnd(){
        isMove = false;
        MoveManager.Instance.NextTiming();
    }

    void Attack()
    {
        Managers.Sound.Play(attackSound);
        //anim.SetTrigger("doAttack");
    }

    protected override void DieDestroy()
    {
        Managers.Sound.Play(dieSound);
        //anim.SetTrigger("doDeath");
        allStop = true;
    }

    private IEnumerator MovePlayer(Vector3 direction, float sec)
    {
        float elapsedTime = 0;
        Vector3 origPos = transform.position;
        Vector3 targetPos = origPos + direction;
        //anim.

        while (elapsedTime < sec)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, elapsedTime / sec);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        CheckMove(redirecting);
    }
}
