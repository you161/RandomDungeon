using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("通常の敵の移動速度")]
    public float enemyMoveSpeed = 0;
    [Header("逃げる敵の移動速度")]
    public float escapeEnemyMoveSpeed = 0;
    [Header("追いかけ始める距離")]
    public float moveDistance = 0;
    [Header("逃げ始める距離")]
    public float escapeDistance = 5f;
    [Header("攻撃のクールタイム")]
    public float coolTime = 0;
    [Header("攻撃時間")]
    public float attackTime = 0;
    [Header("攻撃までのディレイ時間")]
    public float delayTime = 0;
    [Header("攻撃距離")]
    public float attackDistance = 0;
    [Header("タグ名")]
    public string tagName = "";
    [Header("HP")]
    public float hp = 0;
    [Header("攻撃ダメージ")]
    public float attackDamage = 0;
}