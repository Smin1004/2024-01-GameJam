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

    private Vector2Int plusPos;
    private float x, y;
    private bool isMove;

    public void Init()
    {
        _instance = this;
    }

    protected override void Start()
    {
        base.Start();
        isDead = false;
        isMove = false;
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
        if (isDead || isMove) return;

        if(Input.GetKeyDown(KeyCode.W)) CheckMove(Vector2.up);
        if(Input.GetKeyDown(KeyCode.A)) CheckMove(Vector2.left);
        if(Input.GetKeyDown(KeyCode.S)) CheckMove(Vector2.down);
        if(Input.GetKeyDown(KeyCode.D)) CheckMove(Vector2.right);
    }

    private void CheckMove(Vector2 movePos)
    {
        isMove = true;
        Managers.Sound.Play(moveSound);

        int action = MoveManager.Instance.MoveCheck(curPos, Vector2Int.RoundToInt(movePos));

        switch (action)
        {
            case 0: curPos = Vector2Int.RoundToInt(curPos + movePos); StartCoroutine(MovePlayer(movePos, 0.2f)); break;
            case 1: isMove = false; break;
            case 2: Attack(); isMove = false; break;
        }
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
        isDead = true;
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
        CheckMove(direction);
    }
}
