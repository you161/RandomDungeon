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
    public Vector3 GetRandomPatrolDestination(Vector3 enemyPosition)
    {
        Vector2Int currentRoomPosition = new Vector2Int(
            Mathf.RoundToInt(enemyPosition.x / stageData.roomSize),
            Mathf.RoundToInt(enemyPosition.z / stageData.roomSize)
        );

        Dictionary<Vector2Int, HashSet<Vector2Int>> connections = stageGenerator.GetConnections();

        if (!connections.ContainsKey(currentRoomPosition))
        {
            return enemyPosition;
        }

        HashSet<Vector2Int> connectedRooms = connections[currentRoomPosition];

        if (connectedRooms.Count == 0)
        {
            return enemyPosition;
        }

        List<Vector2Int> roomList = new (connectedRooms);
        Vector2Int nextRoomPosition = roomList[Random.Range(0, roomList.Count)];

        return new Vector3(
            nextRoomPosition.x * stageData.roomSize,
            enemyPosition.y,
            nextRoomPosition.y * stageData.roomSize
        );
    }
    public Vector3 GetWarpDestination(Vector3 enemyPosition,Vector3 playerPosition)
    {
        Dictionary<Vector2Int, GameObject> rooms = stageGenerator.GetRooms();
        List<Vector2Int> candidates = new();

        Vector2Int currentEnemyRoom =
            new(
                Mathf.RoundToInt(enemyPosition.x / stageData.roomSize),
                Mathf.RoundToInt(enemyPosition.z / stageData.roomSize)
            );

        Vector2Int playerRoom =
            new(
                Mathf.RoundToInt(playerPosition.x / stageData.roomSize),
                Mathf.RoundToInt(playerPosition.z / stageData.roomSize)
            );

        //プレイヤーの部屋からの距離を取得
        Dictionary<Vector2Int, int> distances = GetRoomDistances(playerRoom);

        foreach (var pair in distances)
        {
            Vector2Int roomPosition = pair.Key;
            int distance = pair.Value;

            //現在いる部屋にはワープしない
            if (roomPosition == currentEnemyRoom)
            {
                continue;
            }

            //プレイヤーから一定距離以上離れた部屋だけ候補
            if (distance < stageData.escapeEnemyMinDistance)
            {
                continue;
            }

            candidates.Add(roomPosition);
        }

        //候補がない場合は、現在位置を返す
        if (candidates.Count == 0)
        {
            return enemyPosition;
        }

        //候補からランダムに選択
        Vector2Int selectedRoom = candidates[Random.Range(0, candidates.Count)];

        return new Vector3(
            selectedRoom.x * stageData.roomSize,
            enemyPosition.y,
            selectedRoom.y * stageData.roomSize
        );
    }
}