using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove = null;
    [SerializeField] private PlayerLook playerLook = null;
    [SerializeField] private PlayerAttack playerAttack = null;
    [SerializeField] private FadeManager fadeManager = null;

    private bool previousCanMove;

    private void Start()
    {
        SetPlayerCanMove(!fadeManager.GetIsFading());
    }

    private void Update()
    {
        bool currentCanMove = !fadeManager.GetIsFading();

        if (currentCanMove != previousCanMove)
        {
            SetPlayerCanMove(currentCanMove);
        }
    }

    private void SetPlayerCanMove(bool value)
    {
        previousCanMove = value;

        playerMove.SetCanMove(value);
        playerLook.SetCanMove(value);
        playerAttack.SetCanMove(value);
    }
}