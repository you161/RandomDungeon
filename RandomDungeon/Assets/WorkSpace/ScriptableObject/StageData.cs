using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    [Header("生成設定")]
    public int roomCount = 10;

    [Header("部屋のサイズ")]
    public float roomSize = 5.0f;
    [Header("敵の生成間隔")]
    public int enemySpawnInterval = 3;
}