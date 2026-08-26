using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove = null;
    [SerializeField] private PlayerLook playerLook = null;
    [SerializeField] private PlayerAttack playerAttack = null;
    [SerializeField] private bool canMove = false;

    private void Start()
    {
        playerMove.SetCanMove(canMove);
        playerLook.SetCanMove(canMove);
        playerAttack.SetCanMove(canMove);
    }
}