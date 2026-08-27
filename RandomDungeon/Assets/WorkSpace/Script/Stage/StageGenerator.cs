using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [Header("部屋プレハブ")]
    [SerializeField] private GameObject roomPrefab = null;

    [Header("ステージデータ")]
    [SerializeField] private StageData stageData = null;

    //生成された部屋
    private Dictionary<Vector2Int, GameObject> rooms = new();

    //部屋同士の接続情報
    private Dictionary<Vector2Int, HashSet<Vector2Int>> connections = new();

    //部屋の生成順序
    private List<Vector2Int> roomOrder = new();

    private readonly Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };
    public void GenerateStage()
    {
        //すでに生成されている場合
        ClearStage();
        //最初の部屋
        CreateRoom(Vector2Int.zero);

        while (rooms.Count < stageData.roomCount)
        {
            //生成元を選択
            Vector2Int currentPosition = GetNextBaseRoom();
            //ランダムな方向
            Vector2Int direction = directions[Random.Range(0, directions.Length)];
            Vector2Int nextPosition = currentPosition + direction;

            //生成範囲外
            if (Mathf.Abs(nextPosition.x) > stageData.maxWidth
              ||Mathf.Abs(nextPosition.y) > stageData.maxHeight)
            {
                continue;
            }

            //すでに部屋がある
            if (rooms.ContainsKey(nextPosition))
            {
                continue;
            }

            CreateRoom(nextPosition);

            //生成元と接続
            ConnectRooms(currentPosition,nextPosition);
        }

        //壁を設定
        UpdateRoomWalls();
    }
    private Vector2Int GetNextBaseRoom()
    {
        //最新の部屋を選ぶ
        if (Random.value <= stageData.latestRoomRate)
        {
            return roomOrder[roomOrder.Count - 1];
        }

        //すべての部屋からランダムに選ぶ
        return roomOrder[Random.Range(0, roomOrder.Count)];
    }

    private void CreateRoom(Vector2Int gridPosition)
    {
        Vector3 worldPosition = new(
            gridPosition.x * stageData.roomSize,
            0f,
            gridPosition.y * stageData.roomSize
        );

        GameObject room = Instantiate(roomPrefab,worldPosition,Quaternion.identity,transform);

        rooms.Add(gridPosition, room);
        connections.Add(gridPosition,new HashSet<Vector2Int>());
        roomOrder.Add(gridPosition);
    }
    private void ConnectRooms(Vector2Int roomA,Vector2Int roomB)
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
                room.SetWalls(hasUp,hasDown,hasLeft,hasRight);
            }
        }
    }
    private void ClearStage()
    {
        foreach (var room in rooms.Values)
        {
            Destroy(room);
        }

        rooms.Clear();
        connections.Clear();
        roomOrder.Clear();
    }
    public Dictionary<Vector2Int, GameObject> GetRooms()
    {
        return rooms;
    }
    public Dictionary<Vector2Int, HashSet<Vector2Int>> GetConnections()
    {
        return connections;
    }
    public List<Vector2Int> GetRoomOrder()
    {
        return roomOrder;
    }
}