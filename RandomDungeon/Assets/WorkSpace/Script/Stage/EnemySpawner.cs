using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("通常敵プレハブ")]
    [SerializeField] private GameObject enemyPrefab = null;

    [Header("逃げる敵プレハブ")]
    [SerializeField] private GameObject escapeEnemyPrefab = null;
    [Header("ステージデータ")]
    [SerializeField] private StageData stageData = null;

    [Header("ステージ生成")]
    [SerializeField] private StageGenerator stageGenerator = null;

    [Header("経路探索")]
    [SerializeField] private StagePathFinder stagePathFinder = null;
    [Header("敵生成オフセット")]
    [SerializeField] private float enemySpawnOffsetY = 0f;
    private Vector2Int escapeEnemyRoomPosition;
    private bool hasEscapeEnemyRoom = false;

    public void SpawnEnemies( Vector2Int goalPosition)
    {
        Dictionary<Vector2Int, GameObject> rooms = stageGenerator.GetRooms();
        List<Vector2Int> roomOrder = stageGenerator.GetRoomOrder();

        int interval = stageData.enemySpawnInterval;

        if (interval <= 0)
        {
            Debug.LogWarning("enemySpawnIntervalが0以下");
            return;
        }

        for (int i = 0; i < roomOrder.Count; ++i)
        {
            //スタート部屋は生成しない
            if (i == 0)
            {
                continue;
            }

            //指定間隔以外
            if (i % interval != 0)
            {
                continue;
            }

            Vector2Int roomPosition = roomOrder[i];

            //ゴール部屋は生成しない
            if (roomPosition == goalPosition)
            {
                continue;
            }

            //逃げる敵がいる部屋には生成しない
            if (hasEscapeEnemyRoom && roomPosition == escapeEnemyRoomPosition)
            {
                continue;
            }

            Room room = rooms[roomPosition].GetComponent<Room>();

            if (room == null)
            {
                continue;
            }

            Transform spawnPoint = room.GetRandomEnemySpawnPoint();

            if (spawnPoint == null)
            {
                continue;
            }

            Vector3 spawnPosition = spawnPoint.position;
            spawnPosition.y += enemySpawnOffsetY;
            GameObject enemy = Instantiate(enemyPrefab,spawnPosition,Quaternion.identity);
            enemy.SetActive(true);
        }
    }

    public void SpawnEscapeEnemy(Vector2Int goalPosition)
    {
        Dictionary<Vector2Int, GameObject> rooms = stageGenerator.GetRooms();

        //スタートから各部屋までの距離
        Dictionary<Vector2Int, int> distances = stagePathFinder.GetRoomDistances(Vector2Int.zero);

        List<Vector2Int> candidates = new();

        foreach (var pair in distances)
        {
            Vector2Int roomPosition = pair.Key;
            int distance = pair.Value;

            //指定距離より近い
            if (distance <
                stageData.escapeEnemyMinDistance)
            {
                continue;
            }

            //ゴール部屋
            if (roomPosition == goalPosition)
            {
                continue;
            }

            candidates.Add(roomPosition);
        }

        if (candidates.Count == 0)
        {
            Debug.LogWarning("逃げる敵を生成できる部屋がない");
            return;
        }

        //候補からランダムに選択
        Vector2Int selectedRoomPosition = candidates[Random.Range(0,candidates.Count)];
        Room room = rooms[selectedRoomPosition].GetComponent<Room>();

        if (room == null)
        {
            return;
        }

        Transform spawnPoint = room.GetRandomEnemySpawnPoint();

        if (spawnPoint == null)
        {
            return;
        }

        escapeEnemyRoomPosition = selectedRoomPosition;
        hasEscapeEnemyRoom = true;
        Vector3 spawnPosition = spawnPoint.position;
        spawnPosition.y += enemySpawnOffsetY;
        GameObject escapeEnemy = Instantiate(escapeEnemyPrefab,spawnPosition,Quaternion.identity);
        escapeEnemy.SetActive(true);
    }
}