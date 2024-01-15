using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<StageData> stageList;
    [SerializeField] private StageData curStage;
    [SerializeField] private Player player;
    [SerializeField] private int stageIndex;
    [SerializeField] private int mapIndex;
    private MoveManager moveManager;

    private List<MapData> battleMapData = new();
    private List<GameObject> mapTile = new();
    private List<Obj_Base> mapObj = new();
    private List<Enemy_Base> mapEnemy = new();

    private void Init()
    {
        if (stageList.Count != 0)
        {
            stageIndex = RoundData.Instance.stageIndex;
            mapIndex = RoundData.Instance.mapIndex;
            curStage = stageList[stageIndex];
        }

        battleMapData = curStage.battleMapData.ToList();
        mapTile = curStage.mapTile.ToList();
        mapObj = curStage.mapObj.ToList();
        mapEnemy = curStage.mapEnemy.ToList();
    }

    private void ChoiceMap()
    {
        player.Setting(battleMapData[mapIndex].life);
        CreateMap(battleMapData[mapIndex]);
    }

    private void CreateMap(MapData curMap)
    {
        int[,] curGroundData = LoadCSV.Load(curMap.groundMap);
        int[,] totalData = new int[curGroundData.GetLength(0), curGroundData.GetLength(1)];
        MoveGround_Base[,] groundMap = new MoveGround_Base[curGroundData.GetLength(0), curGroundData.GetLength(1)];

        for (int i = 0; i < curGroundData.GetLength(0); i++) for (int j = 0; j < curGroundData.GetLength(1); j++)
            {
                if (curGroundData[i, j] == 0) continue; // void

                var temp = Instantiate(mapTile[curGroundData[i, j] - 1],
                new Vector3(i, j), Quaternion.identity, transform);

                if (temp.TryGetComponent<MoveGround_Base>(out var move))
                {
                    move.curPos = new(i, j);
                    move.index = curGroundData[i, j];

                    curGroundData[i, j] = 99;
                    groundMap[i, j] = move;
                }
            }

        moveManager.MapInit(curGroundData);
        Spawn(curMap, totalData);
    }

    private void Spawn(MapData Map, int[,] curMap)
    {
        Enemy_Base[,] map = new Enemy_Base[curMap.GetLength(0), curMap.GetLength(1)];
        Obj_Base[,] obj = new Obj_Base[curMap.GetLength(0), curMap.GetLength(1)];
        //List<MoveObj_Base> moveObj = new();
        List<MoveEnemy_Base> moveEnemy = new();

        int[,] curObjData = LoadCSV.Load(Map.objMap);
        int[,] curEnemyData = LoadCSV.Load(Map.enemyMap);

        for (int i = 0; i < curObjData.GetLength(0); i++) for (int j = 0; j < curObjData.GetLength(1); j++)
            {
                if (curObjData[i, j] == 0) continue; // void
                var temp = Instantiate(mapObj[curObjData[i, j] - 1], new Vector3(i, j),
                Quaternion.identity, transform);
                temp.curPos = new(i, j);
                obj[i, j] = temp;

                // if (temp.TryGetComponent<MoveObj_Base>(out var move))
                // {
                //     move.index = curObjData[i, j];
                //     moveObj.Add(move);
                // }
            }

        for (int i = 0; i < curEnemyData.GetLength(0); i++) for (int j = 0; j < curEnemyData.GetLength(1); j++)
            {
                if (curEnemyData[i, j] <= 1) continue; // void

                var temp = Instantiate(mapEnemy[curEnemyData[i, j] - 2], new Vector3(i, j), Quaternion.identity);
                temp.curPos = new(i, j);
                if (temp.TryGetComponent<MoveEnemy_Base>(out var move))
                {
                    for (int k = -1; k <= 1; k++) for (int l = -1; l <= 1; l++)
                        {
                            if (k != 0 && l != 0) continue;
                            else if (k == 0 && l == 0) continue;

                            int x = i + k;
                            int y = j + l;

                            if (curEnemyData[x, y] == 1)
                            {
                                move.attackPos = new(x, y);
                                moveEnemy.Add(move);
                            }
                        }
                }

                map[i, j] = temp;
            }

        moveManager.MobInit(map, obj, moveEnemy, curObjData);
    }

    private void Awake()
    {
        player.Init();
        moveManager = GetComponent<MoveManager>();
        moveManager.Init();
        Init();
        ChoiceMap();
    }
}
