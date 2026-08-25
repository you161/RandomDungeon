using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField] private GameObject roomPrefab = null;
    [SerializeField] private StageData stageData = null;
    [SerializeField] private GameObject goalItem = null;

    //生成された部屋の座標を保存
    private Dictionary<Vector2Int, GameObject> rooms
        = new Dictionary<Vector2Int, GameObject>();

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
        Debug.Log($"Farthest room position: {farthestPosition}");
        goalItem.transform.localPosition = new Vector3(
            farthestPosition.x * stageData.roomSize,
            goalItem.transform.localPosition.y,
            farthestPosition.y * stageData.roomSize
        );
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
            //次回は今回生成した部屋から生成する
            currentPosition = nextPosition;
        }

        UpdateRoomWalls();
    }

    private void CreateRoom(Vector2Int gridPosition)
    {
        //グリッド座標をUnityの3D座標に変換
        Vector3 worldPosition = new Vector3(
            gridPosition.x * stageData.roomSize,
            0f,
            gridPosition.y * stageData.roomSize
        );

        GameObject room = Instantiate(
            roomPrefab,
            worldPosition,
            Quaternion.identity,
            transform
        );

        //Dictionaryに保存
        rooms.Add(gridPosition, room);
    }
    private void UpdateRoomWalls()
    {
        foreach (var pair in rooms)
        {
            Vector2Int position = pair.Key;
            GameObject roomObject = pair.Value;

            bool hasUp = rooms.ContainsKey(position + Vector2Int.up);
            bool hasDown = rooms.ContainsKey(position + Vector2Int.down);
            bool hasLeft = rooms.ContainsKey(position + Vector2Int.left);
            bool hasRight = rooms.ContainsKey(position + Vector2Int.right);

            Room room = roomObject.GetComponent<Room>();

            if(room != null)
            {
                room.SetWalls(hasUp,hasDown,hasLeft,hasRight);
            }
        }
    }
    private Vector2Int FindFarthestRoom(Vector2Int startPosition)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();

        Dictionary<Vector2Int, int> distances
            = new Dictionary<Vector2Int, int>();

        //スタート地点
        queue.Enqueue(startPosition);
        distances.Add(startPosition, 0);

        Vector2Int farthestPosition = startPosition;
        int maxDistance = 0;

        while (queue.Count > 0)
        {
            Vector2Int currentPosition = queue.Dequeue();

            foreach (Vector2Int direction in directions)
            {
                Vector2Int nextPosition = currentPosition + direction;

                //部屋がない
                if (!rooms.ContainsKey(nextPosition))
                {
                    continue;
                }

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
}