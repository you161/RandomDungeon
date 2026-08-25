using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("移動速度")]
    public float moveSpeed = 0;
    [Header("マウス感度")] 
    public float mouseSensitivity = 0;
}