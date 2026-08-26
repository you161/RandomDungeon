using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("移動速度")]
    public float moveSpeed = 0;
    [Header("クールタイム")]
    public float coolTime = 0;
    [Header("攻撃時間")]
    public float attackTime = 0;
    [Header("ディレイ時間")]
    public float delayTime = 0;
    [Header("攻撃距離")]
    public float attackDistance = 0;
    [Header("移動可能距離")]
    public float moveDistance = 0;
    [Header("タグ名")]
    public string tagName = "";
    [Header("HP")]
    public float hp = 0;
}