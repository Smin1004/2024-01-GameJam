using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<StageData> stageList;
    [SerializeField] private Player[] playerList;
    [SerializeField] private StageData curStage;
    //[SerializeField] private Player player;
    [SerializeField] private int stageIndex;
    [SerializeField] private int mapIndex;
    private MoveManager moveManager;

    private List<MapData> battleMapData = new();
    private List<GameObject> mapTile = new();
    private List<Obj_Base> mapObj = new();
    private List<Enemy_Base> mapEnemy = new();
    private int playerCount;

    public int PlayerCount
    {
        get;
        private set;
    }
    private void Init()
    {
        if (stageList.Count != 0)
        {
            stageIndex = RoundData.Instance.stageIndex;
            mapIndex = RoundData.Instance.mapIndex;
            curStage = stageList[stageIndex];
        }

        Debug.Log($"{RoundData.Instance.stageIndex}");

        battleMapData = curStage.battleMapData.ToList();
        mapTile = curStage.mapTile.ToList();
        mapObj = curStage.mapObj.ToList();
        mapEnemy = curStage.mapEnemy.ToList();
    }

    private void ChoiceMap()
    {
        //player.Setting(battleMapData[mapIndex].life);
        playerCount = battleMapData[mapIndex].players;
        CreateMap(battleMapData[mapIndex]);
    }

    private void CreateMap(MapData curMap)
    {
        int[,] curGroundData = LoadCSV.Load(curMap.groundMap);
        //Debug.Log($"{curGroundData.GetLength(0)}, {curGroundData.GetLength(1)}");
        int[,] totalData = new int[curGroundData.GetLength(0), curGroundData.GetLength(1)];

        for (int i = 0; i < curGroundData.GetLength(0); i++) for (int j = 0; j < curGroundData.GetLength(1); j++)
            {
                if (curGroundData[i, j] == 0) continue; // void

                var temp = Instantiate(mapTile[curGroundData[i, j] - 1],
                new Vector3(i, j), Quaternion.identity, transform);
            }

        moveManager.MapInit(curGroundData);
        Spawn(curMap, totalData);
    }

    private void Spawn(MapData Map, int[,] curMap)
    {
        Enemy_Base[,] map = new Enemy_Base[curMap.GetLength(0), curMap.GetLength(1)];
        Obj_Base[,] obj = new Obj_Base[curMap.GetLength(0), curMap.GetLength(1)];
        Player[] players = new Player[playerCount];

        int[,] curObjData = LoadCSV.Load(Map.objMap);
        int[,] curEnemyData = LoadCSV.Load(Map.enemyMap);
        List<Enemy_Base> enemy = new();

        for (int i = 0; i < curObjData.GetLength(0); i++) for (int j = 0; j < curObjData.GetLength(1); j++)
            {
                if (curObjData[i, j] == 0) continue; // void
                var temp = Instantiate(mapObj[curObjData[i, j] - 1], new Vector3(i, j),
                Quaternion.identity, transform);
                temp.curPos = new(i, j);
                obj[i, j] = temp;
            }

        for (int i = 0; i < curEnemyData.GetLength(0); i++) for (int j = 0; j < curEnemyData.GetLength(1); j++)
            {
                if (curEnemyData[i, j] == 0) continue; // void
                Mob_Base mob = null;

                if (curEnemyData[i, j] <= playerCount) mob = playerList[curEnemyData[i, j] - 1];
                else mob = mapEnemy[curEnemyData[i, j] - (playerCount + 1)];



                var temp = Instantiate(mob, new Vector3(i, j), Quaternion.identity);
                temp.curPos = new(i, j);

                if (temp.TryGetComponent<Enemy_Base>(out var e))
                {
                    map[i, j] = e;
                    enemy.Add(e);
                }else players[curEnemyData[i, j] - 1] = temp.GetComponent<Player>();
            }

        moveManager.MobInit(map, obj, enemy, curObjData, players);
    }

    private void Awake()
    {
        //player.Init();
        moveManager = GetComponent<MoveManager>();
        moveManager.Init();
        Init();
        ChoiceMap();
    }
}
