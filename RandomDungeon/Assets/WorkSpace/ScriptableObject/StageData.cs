using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    [Header("生成設定")]
    public int roomCount = 0;

    [Header("部屋のサイズ")]
    public float roomSize = 0;
    [Header("敵の生成間隔")]
    public int enemySpawnInterval = 0;
    [Header("幅上限")]
    public int maxWidth = 0;
    [Header("高さ上限")]
    public int maxHeight = 0;
    [Header("最新の部屋を選ぶ確率")]
    [Range(0.0f, 1.0f)]
    public float latestRoomRate = 0;
    [Header("逃げる敵を生成する最低距離")]
    public int escapeEnemyMinDistance = 0;
}