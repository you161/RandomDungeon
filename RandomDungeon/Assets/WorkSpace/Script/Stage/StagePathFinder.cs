using System.Collections.Generic;
using UnityEngine;

public class StagePathFinder : MonoBehaviour
{
    [SerializeField] private StageGenerator stageGenerator = null;
    [SerializeField] private StageData stageData = null;

    //スタートから各部屋までの距離を取得
    public Dictionary<Vector2Int, int> GetRoomDistances( Vector2Int startPosition)
    {
        Dictionary<Vector2Int, HashSet<Vector2Int>> connections = stageGenerator.GetConnections();

        Queue<Vector2Int> queue = new();

        Dictionary<Vector2Int, int> distances = new();

        queue.Enqueue(startPosition);
        distances.Add(startPosition, 0);

        while (queue.Count > 0)
        {
            Vector2Int currentPosition = queue.Dequeue();

            foreach (Vector2Int nextPosition in connections[currentPosition])
            {
                //すでに探索済み
                if (distances.ContainsKey(nextPosition))
                {
                    continue;
                }

                int nextDistance = distances[currentPosition] + 1;
                distances.Add(nextPosition,nextDistance);
                queue.Enqueue(nextPosition);
            }
        }

        return distances;
    }

    //一番遠い部屋を取得
    public Vector2Int FindFarthestRoom(Vector2Int startPosition)
    {
        Dictionary<Vector2Int, int> distances = GetRoomDistances(startPosition);

        Vector2Int farthestPosition = startPosition;

        int maxDistance = 0;

        foreach (var pair in distances)
        {
            if (pair.Value > maxDistance)
            {
                maxDistance = pair.Value;
                farthestPosition = pair.Key;
            }
        }

        return farthestPosition;
    }

    //逃げる先を取得
    public Vector3 GetEscapeDestination(Vector3 enemyPosition,Vector3 playerPosition)
    {
        Dictionary<Vector2Int, HashSet<Vector2Int>> connections = stageGenerator.GetConnections();

        //現在いる部屋を取得
        Vector2Int currentRoomPosition =
            new(
                Mathf.RoundToInt(enemyPosition.x / stageData.roomSize),
                Mathf.RoundToInt(enemyPosition.z / stageData.roomSize)
            );

        //部屋が存在しない
        if (!connections.ContainsKey(currentRoomPosition))
        {
            return enemyPosition;
        }

        HashSet<Vector2Int> connectedRooms = connections[currentRoomPosition];

        //接続先がない
        if (connectedRooms.Count == 0)
        {
            return enemyPosition;
        }

        Vector2Int farthestRoom = currentRoomPosition;
        float maxDistance = 0f;

        //接続先の中からプレイヤーから一番遠い部屋を選ぶ
        foreach (Vector2Int roomPosition in connectedRooms)
        {
            Vector3 roomWorldPosition =
                new(
                    roomPosition.x * stageData.roomSize,
                    playerPosition.y,
                    roomPosition.y * stageData.roomSize
                );

            float distance = Vector3.Distance(playerPosition,roomWorldPosition);

            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthestRoom = roomPosition;
            }
        }

        return new Vector3(
            farthestRoom.x * stageData.roomSize,
            enemyPosition.y,
            farthestRoom.y * stageData.roomSize
        );
    }

    //巡回先を取得
    public Vector3 GetPatrolDestination(
        Vector3 enemyPosition)
    {
        Dictionary<Vector2Int, HashSet<Vector2Int>> connections = stageGenerator.GetConnections();

        //現在の部屋
        Vector2Int currentRoomPosition =
            new(
                Mathf.RoundToInt(enemyPosition.x / stageData.roomSize),
                Mathf.RoundToInt( enemyPosition.z / stageData.roomSize)
            );

        if (!connections.ContainsKey(currentRoomPosition))
        {
            return enemyPosition;
        }

        HashSet<Vector2Int> connectedRooms = connections[currentRoomPosition];

        if (connectedRooms.Count == 0)
        {
            return enemyPosition;
        }

        //HashSetをListに変換
        List<Vector2Int> roomList = new(connectedRooms);

        //ランダムな接続先を選ぶ
        Vector2Int nextRoom = roomList[Random.Range(0, roomList.Count)];

        return new Vector3(
            nextRoom.x * stageData.roomSize,
            enemyPosition.y,
            nextRoom.y * stageData.roomSize
        );
    }
}