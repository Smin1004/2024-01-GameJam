using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
public class MoveManager : MonoBehaviour
{
    private static MoveManager _instance = null;
    public static MoveManager Instance => _instance;

    [SerializeField] private CameraMove cam;
    public Player curPlayer;
    public Player subPlayer;
    public GameObject clearWin;

    private int[,] curGroundMap;
    private int[,] curObjMap;
    private int[,] curMoveMap;
    private Enemy_Base[,] curMob;
    private Obj_Base[,] curObj;
    private Player players;
    private List<Enemy_Base> enemys;

    public void Init()
    {
        _instance = this;
    }

    private void ChangePlayer()
    {
        Debug.Log("Cheage");
        if(curPlayer.StopCheck()) return;

        Player temp = subPlayer;
        subPlayer = curPlayer;
        curPlayer = temp;

        curPlayer.allStop = false;
        subPlayer.allStop = true;
    }

    public void NextTiming()
    {
        foreach (var enemy in enemys) enemy.PlayerCheck();
    }

    public int MoveCheck(Vector2Int curPos, Vector2Int plusPos, bool isEnemy = false)
    {
        Vector2Int movePos = curPos + plusPos;

        if (curGroundMap[movePos.x, movePos.y] == 0) return 1;
        if (curMoveMap[movePos.x, movePos.y] != 0)
        {
            if (!isEnemy && curMoveMap[movePos.x, movePos.y] != 1)
            {
                curMob[movePos.x, movePos.y].Hit(plusPos);
                return 2;
            }
            else return 1;
        }
        Debug.Log($"{movePos.x}, {movePos.y}");
        Debug.Log($"{curObjMap.GetLength(0)}, {curObjMap.GetLength(1)}");
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
            else { players[i].allStop = true; subPlayer = players[i];}
        }

        InputManager.instance.Up += () => curPlayer.Move(Vector2.up);
        InputManager.instance.Down += () => curPlayer.Move(Vector2.down);
        InputManager.instance.Left += () => curPlayer.Move(Vector2.left);
        InputManager.instance.Right += () => curPlayer.Move(Vector2.right);
        InputManager.instance.PlayerSwap += () => ChangePlayer();
    }
}