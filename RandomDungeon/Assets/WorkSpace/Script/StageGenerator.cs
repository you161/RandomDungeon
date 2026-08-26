using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField] private GameObject roomPrefab = null;
    [SerializeField] private StageData stageData = null;
    [SerializeField] private GameObject goalItem = null;
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private float enemySpawnOffsetY = 0;

    //生成された部屋の座標を保存
    private Dictionary<Vector2Int, GameObject> rooms
        = new Dictionary<Vector2Int, GameObject>();
    //部屋同士の接続情報を保存
    private Dictionary<Vector2Int, HashSet<Vector2Int>> connections
        = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
    //部屋の生成順序を保存
    private List<Vector2Int> roomOrder = new List<Vector2Int>();

    private readonly Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    private void Start()
    {
        GenerateStage();

        Vector2Int farthestPosition = FindFarthestRoom(Vector2Int.zero);

        goalItem.transform.localPosition = new Vector3(
            farthestPosition.x * stageData.roomSize,
            goalItem.transform.localPosition.y,
            farthestPosition.y * stageData.roomSize
        );

        navMeshSurface.BuildNavMesh();

        SpawnEnemies(farthestPosition);
    }

    private void GenerateStage()
    {
        Vector2Int currentPosition = Vector2Int.zero;

        //最初の部屋を生成
        CreateRoom(currentPosition);

        while (rooms.Count < stageData.roomCount)
        {
            //ランダムな方向を選ぶ
            Vector2Int direction = directions[Random.Range(0, directions.Length)];
            Vector2Int nextPosition = currentPosition + direction;

            //すでに部屋がある場合
            if (rooms.ContainsKey(nextPosition))
            {
                continue;
            }

            //新しい部屋を生成
            CreateRoom(nextPosition);
            //生成元の部屋と新しい部屋を接続
            ConnectRooms(currentPosition, nextPosition);
            //次回は今回生成した部屋から生成する
            currentPosition = nextPosition;
        }

        UpdateRoomWalls();
    }

    private void CreateRoom(Vector2Int gridPosition)
    {
        Vector3 worldPosition = new Vector3(
            gridPosition.x * stageData.roomSize,
            0f,
            gridPosition.y * stageData.roomSize
        );

        GameObject room = Instantiate(roomPrefab,worldPosition,Quaternion.identity,transform);

        rooms.Add(gridPosition, room);
        connections.Add(gridPosition, new HashSet<Vector2Int>());

        //生成順を保存
        roomOrder.Add(gridPosition);
    }

    private void ConnectRooms(Vector2Int roomA, Vector2Int roomB)
    {
        connections[roomA].Add(roomB);
        connections[roomB].Add(roomA);
    }

    private void UpdateRoomWalls()
    {
        foreach (var pair in rooms)
        {
            Vector2Int position = pair.Key;
            GameObject roomObject = pair.Value;

            bool hasUp = connections[position].Contains(position + Vector2Int.up);
            bool hasDown = connections[position].Contains(position + Vector2Int.down);
            bool hasLeft = connections[position].Contains(position + Vector2Int.left);
            bool hasRight = connections[position].Contains(position + Vector2Int.right);

            Room room = roomObject.GetComponent<Room>();

            if (room != null)
            {
                room.SetWalls(hasUp, hasDown, hasLeft, hasRight);
            }
        }
    }

    private Vector2Int FindFarthestRoom(Vector2Int startPosition)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, int> distances = new Dictionary<Vector2Int, int>();

        //スタート地点
        queue.Enqueue(startPosition);
        distances.Add(startPosition, 0);

        Vector2Int farthestPosition = startPosition;
        int maxDistance = 0;

        while (queue.Count > 0)
        {
            Vector2Int currentPosition = queue.Dequeue();

            //現在の部屋と接続されている部屋だけを探索
            foreach (Vector2Int nextPosition in connections[currentPosition])
            {
                //すでに探索済み
                if (distances.ContainsKey(nextPosition))
                {
                    continue;
                }

                int nextDistance = distances[currentPosition] + 1;
                distances.Add(nextPosition, nextDistance);
                queue.Enqueue(nextPosition);

                //今までで一番遠い部屋なら更新
                if (nextDistance > maxDistance)
                {
                    maxDistance = nextDistance;
                    farthestPosition = nextPosition;
                }
            }
        }

        return farthestPosition;
    }
    private void SpawnEnemies(Vector2Int goalPosition)
    {
        int interval = stageData.enemySpawnInterval;

        for (int i = 0; i < roomOrder.Count; i++)
        {
            if (i == 0)
            {
                continue;
            }

            if (i % interval != 0)
            {
                continue;
            }

            Vector2Int roomPosition = roomOrder[i];

            //ゴール部屋
            if (roomPosition == goalPosition)
            {
                continue;
            }

            Room room = rooms[roomPosition].GetComponent<Room>();

            if (room == null)
            {
                continue;
            }

            Transform spawnPoint = room.GetRandomEnemySpawnPoint();
            spawnPoint.localPosition = new Vector3(
                spawnPoint.localPosition.x,
                spawnPoint.localPosition.y + enemySpawnOffsetY,
                spawnPoint.localPosition.z
            );
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemy.SetActive(true);
        }
    }
}