using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private StageData stageData;

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
}