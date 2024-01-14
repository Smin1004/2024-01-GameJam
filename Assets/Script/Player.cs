using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Player : Mob_Base
{
    private static Player _instance = null;
    public static Player Instance => _instance;

    [SerializeField] private AudioClip moveSound;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private Text hpbar;

    [SerializeField] private int HP;

    public int isKey;
    public bool isLobby;
    public bool isDead;
    public bool cantMove;

    private Vector2Int plusPos;
    private float x, y;

    public void Init()
    {
        _instance = this;
    }

    protected override void Start()
    {
        base.Start();
        isDead = false;
        cantMove = false;
    }

    private void Update() {
        Move();
    }

    public void Setting(int hp)
    {
        HP = hp;
        hpbar.text = HP.ToString();
    }

    public void Damage(bool isAttack)
    {
        HP -= 1;
        hpbar.text = HP.ToString();

        if (HP <= 0)
        {
            HP = 0;
            DieDestroy();
        }
    }

    private void Move()
    {
        if (isDead || cantMove) return;

        if(Input.GetKeyDown(KeyCode.W)) CheckMove(Vector2.up);
        if(Input.GetKeyDown(KeyCode.A)) CheckMove(Vector2.left);
        if(Input.GetKeyDown(KeyCode.S)) CheckMove(Vector2.down);
        if(Input.GetKeyDown(KeyCode.D)) CheckMove(Vector2.right);
    }

    private void CheckMove(Vector2 movePos)
    {
        Managers.Sound.Play(moveSound);

        int action = MoveManager.Instance.MoveCheck(curPos, Vector2Int.RoundToInt(movePos), true);
        Debug.Log(action);

        switch (action)
        {
            case 0: curPos = curPos + plusPos; StartCoroutine(MovePlayer(movePos, 0.2f)); break;
            case 1: break;
            case 2: Attack(); break;
        }
    }

    public void ForcedMovement()
    {
        Vector3 movePos = new();

        if (plusPos == Vector2Int.up) movePos = Vector3.forward;
        if (plusPos == Vector2Int.down) movePos = Vector3.back;
        if (plusPos == Vector2Int.right) movePos = Vector3.right;
        if (plusPos == Vector2Int.left) movePos = Vector3.left;

        int action = MoveManager.Instance.MoveCheck(curPos, plusPos, true);

        switch (action)
        {
            case 0:
                Managers.Sound.Play(moveSound);
                curPos = curPos + plusPos;
                StartCoroutine(MovePlayer(movePos, 0.2f)); break;
            case 1: cantMove = false; break;
            case 2: cantMove = false; break;
        }
    }

    void RookPlayer(Vector3 pos)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), 1f);
    }

    void Attack()
    {
        Managers.Sound.Play(attackSound);
        anim.SetTrigger("doAttack");
    }

    protected override void DieDestroy()
    {
        Managers.Sound.Play(dieSound);
        anim.SetTrigger("doDeath");
        isDead = true;
    }

    private IEnumerator MovePlayer(Vector3 direction, float sec)
    {
        float elapsedTime = 0;
        Vector3 origPos = transform.position;
        Vector3 targetPos = origPos + direction;
        anim.SetTrigger("doDash");

        while (elapsedTime < sec)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, elapsedTime / sec);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
    }
}
