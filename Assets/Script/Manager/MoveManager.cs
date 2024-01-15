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
    public GameObject clearWin;

    private int[,] curGroundMap;
    private int[,] curObjMap;
    private int[,] curMoveMap;
    private Enemy_Base[,] curMob;
    private Obj_Base[,] curObj;
    private List<MoveEnemy_Base> moveEnemy;
    //private List<MoveObj_Base> moveObj;

    public void Init()
    {
        _instance = this;
    }

    public void NextTiming()
    {
        //foreach (var obj in moveObj) obj.NextTiming();
        foreach (var enemy in moveEnemy) enemy.NextTiming();
    }

    public int MoveCheck(Vector2Int curPos, Vector2Int plusPos, bool isEnemy = false)
    {
        Vector2Int movePos = curPos + plusPos;

        if (curGroundMap[movePos.x, movePos.y] == 0) return 1;

        if (curMoveMap[movePos.x, movePos.y] != 0)
        {
            if (!isEnemy)
            {
                curMob[movePos.x, movePos.y].Hit(plusPos);
                return 2;
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

    public void DestroyEnemy(Vector2Int curPos, MoveEnemy_Base enemy = null)
    {
        curMoveMap[curPos.x, curPos.y] = 0;
        if (enemy != null) moveEnemy.Remove(enemy);
    }

    public void MapInit(int[,] _curGroundMap)
    {
        curGroundMap = _curGroundMap;

        curMoveMap = new int[_curGroundMap.GetLength(0), _curGroundMap.GetLength(1)];
    }

    public void MobInit(Enemy_Base[,] _map, Obj_Base[,] _obj, List<MoveEnemy_Base> _moveEnemy, int[,] _curObjMap)
    {
        curObjMap = _curObjMap;
        curMob = _map;
        curObj = _obj;

        //moveObj = _moveObj.ToList();
        moveEnemy = _moveEnemy.ToList();
        cam.mapSize = new int[_curObjMap.GetLength(0), _curObjMap.GetLength(1)];

        for (int i = 0; i < _map.GetLength(0); i++)
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                if (_map[i, j] != null) curMoveMap[i, j] = 2;
            }

        for (int i = 0; i < _curObjMap.GetLength(0); i++) for (int j = 0; j < _curObjMap.GetLength(1); j++)
                if (_curObjMap[i, j] == 1)
                {
                    curMoveMap[i, j] = 1;
                    Player.Instance.transform.position = new Vector3(i, j);
                    Player.Instance.curPos = new Vector2Int(i, j);
                }

    }
}