using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.InputSystem;
public class MoveManager : MonoBehaviour
{
    private static MoveManager _instance = null;
    public static MoveManager Instance => _instance;

    [SerializeField] private CameraMove cam;
    [SerializeField] private FadeManager fade;
    public Player curPlayer;
    public Player subPlayer;
    public GameObject clearWin;

    [SerializeField] private Setting _setting;

    private int[,] curGroundMap;
    private int[,] curObjMap;
    private int[,] curMoveMap;
    private Enemy_Base[,] curMob;
    private Obj_Base[,] curObj;
    private Player players;
    private List<Enemy_Base> enemys;

    public bool GameEnd { get; private set; }

    [SerializeField] private bool mainScene;

    public void Init()
    {
        _instance = this;
        GameEnd = false;// mainScene = true;
    }

    private void ChangePlayer()
    {
        if (curPlayer.StopCheck() || _setting.SettingShow || subPlayer == null) return;

        if (subPlayer != null)  
        {
            Player temp = subPlayer;
            subPlayer = curPlayer;
            curPlayer = temp;

            StartCoroutine(curPlayer.Event());

            subPlayer.allStop = true;
        }
    }

    private void ChangeWeapon()
    {
        if (curPlayer.StopCheck() || _setting.SettingShow) return;

        curPlayer.isWeapon = !curPlayer.isWeapon;
        StartCoroutine(curPlayer.Event());
    }

    public void NextTiming()
    {
        if (enemys.Count != 0) foreach (var enemy in enemys) enemy.PlayerCheck();
        else Clear();
    }

    private void Clear()
    {
        if (!mainScene)
        {
            Debug.Log("end");
            int clearStage = PlayerPrefs.GetInt("clearStage", -1);
            if (clearStage < RoundData.Instance.stageIndex)
            {
                PlayerPrefs.SetInt("clearStage", RoundData.Instance.stageIndex);
            }
            GameEnd = true;

            GameExit();
        }
    }

    public int MoveCheck(Vector2Int curPos, Vector2Int plusPos, bool isEnemy = false)
    {
        Vector2Int movePos = curPos + plusPos;

        if(plusPos == Vector2Int.zero) return 1;
        if (curGroundMap[movePos.x, movePos.y] == 0) return 1;
        if (curMoveMap[movePos.x, movePos.y] != 0)
        {
            if (!isEnemy && curMoveMap[movePos.x, movePos.y] != 1)
            {
                if (curMob[movePos.x, movePos.y].isNotCheck || curPlayer.isWeapon == curMob[movePos.x, movePos.y].isWeapon)
                {
                    curMob[movePos.x, movePos.y].Hit(plusPos);
                    return 2;
                }
                else return 1;
            }
            else return 1;
        }
        if (curObjMap[movePos.x, movePos.y] != 0 && !isEnemy)
        {
            curObj[movePos.x, movePos.y].UseObj();

            if (!curObj[movePos.x, movePos.y].isCanMove) return 1;
        }

        curMob[movePos.x, movePos.y] = curMob[curPos.x, curPos.y];
        curMob[curPos.x, curPos.y] = null;

        curMoveMap[movePos.x, movePos.y] = curMoveMap[curPos.x, curPos.y];
        curMoveMap[curPos.x, curPos.y] = 0;

        return 0;
    }

    public int EnemyAttackCheck(Vector2Int curPos)
    {
        Vector2Int movePos = Vector2Int.zero;

        for (int i = -1; i <= 1; i++) for (int j = -1; j <= 1; j++)
            {
                movePos = curPos + new Vector2Int(i, j);

                if (curMoveMap[movePos.x, movePos.y] == 1) return 2;
            }

        return 1;
    }

    public void InOutIndex(Vector2Int curPos, Define.MapType map, int index = 0)
    {
        switch (map)
        {
            case Define.MapType.Ground:
                if (curGroundMap[curPos.x, curPos.y] == 0) curGroundMap[curPos.x, curPos.y] = index;
                else curGroundMap[curPos.x, curPos.y] = 0;
                break;

            case Define.MapType.Obj:
                if (curObjMap[curPos.x, curPos.y] == 0) curObjMap[curPos.x, curPos.y] = index;
                else curObjMap[curPos.x, curPos.y] = 0;
                break;
        }
    }

    public void DestroyEnemy(Vector2Int curPos, Enemy_Base enemy)
    {
        curMoveMap[curPos.x, curPos.y] = 0;
        enemys.Remove(enemy);
    }

    public void MapInit(int[,] _curGroundMap)
    {
        curGroundMap = _curGroundMap;

        curMoveMap = new int[_curGroundMap.GetLength(0), _curGroundMap.GetLength(1)];
    }

    public void MobInit(Enemy_Base[,] _map, Obj_Base[,] _obj, List<Enemy_Base> _enemy, int[,] _curObjMap, Player[] players)
    {
        curObjMap = _curObjMap;
        curMob = _map;
        curObj = _obj;

        enemys = _enemy.ToList();
        cam.mapSize = new int[_curObjMap.GetLength(0), _curObjMap.GetLength(1)];

        for (int i = 0; i < _map.GetLength(0); i++)
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                if (_map[i, j] != null) curMoveMap[i, j] = 2;
            }

        for (int i = 0; i < players.Length; i++)
        {
            curMoveMap[players[i].curPos.x, players[i].curPos.y] = 1;

            if (i == 0) curPlayer = players[i];
            else { players[i].allStop = true; subPlayer = players[i]; }
        }

        InputManager.instance.Up += MoveUp;
        InputManager.instance.Down += MoveDown;
        InputManager.instance.Left += MoveLeft;
        InputManager.instance.Right += MoveRight;
        InputManager.instance.WeaponSwap += ChangeWeapon;
        InputManager.instance.PlayerSwap += ChangePlayer;

        curPlayer.destoryAction += ExitEvent;
    }
    private void MoveUp()
    {
        if (!_setting.SettingShow)
            curPlayer.Move(Vector2.up);
    }
    private void MoveDown()
    {
        if (!_setting.SettingShow)
            curPlayer.Move(Vector2.down);
    }
    private void MoveLeft()
    {
        if (!_setting.SettingShow)
            curPlayer.Move(Vector2.left);
    }
    private void MoveRight()
    {
        if (!_setting.SettingShow)
            curPlayer.Move(Vector2.right);
    }

    private void ExitEvent()
    {
        InputManager.instance.Up -= MoveUp;
        InputManager.instance.Down -= MoveDown;
        InputManager.instance.Left -= MoveLeft;
        InputManager.instance.Right -= MoveRight;
        InputManager.instance.PlayerSwap -= ChangeWeapon;
        InputManager.instance.PlayerSwap -= ChangePlayer;

        curPlayer.destoryAction -= ExitEvent;
        curPlayer.allStop = true;
    }
    public void GameExit()
    {
        ExitEvent();
        GameEnd = true;
        fade.FadeOut(1.0f);

        Invoke("GoMainScene", 1);
    }
    private void GoMainScene()
    {
        SceneManager.LoadScene(1);
    }
}