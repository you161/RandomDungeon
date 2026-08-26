using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("移動速度")]
    public float moveSpeed = 0;
    [Header("マウス感度")] 
    public float mouseSensitivity = 0;
    [Header("クールタイム")]
    public float coolTime = 0;
    [Header("攻撃時間")]
    public float attackTime = 0;
    [Header("ディレイ時間")]
    public float delayTime = 0;
    [Header("タグ名")]
    public string tagName = "";
    [Header("攻撃ダメージ")]
    public float attackDamage = 0;
}