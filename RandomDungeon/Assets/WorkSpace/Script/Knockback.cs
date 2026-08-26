using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float knockbackSpeed = 5.0f;
    [SerializeField] private float knockbackDistance = 3.0f;
    [SerializeField] private PlayerMove playerMove = null;
    [SerializeField] private EnemyController enemyController = null;

    private bool isKnockback;
    private Vector3 knockbackDirection;
    private float movedDistance;
    private bool isPlayer = false;
    private void Start()
    {
        isKnockback = false;
        isPlayer = false;
    }

    private void Update()
    {
        MoveKnockback();
    }

    public void StartKnockback(Vector3 sourcePosition,bool value)
    {
        Debug.Log("StartKnockback開始");

        knockbackDirection = transform.position - sourcePosition;
        knockbackDirection.y = 0.0f;
        knockbackDirection.Normalize();
        movedDistance = 0.0f;
        isKnockback = true;

        isPlayer = value;

        if (isPlayer)
        {
            playerMove.SetCanMove(false);
        }
        else
        {
            enemyController.SetIsKnockback(isKnockback);
        }


        Debug.Log($"isKnockback: {isKnockback}");
    }
    private void MoveKnockback()
    {
        Debug.Log($"MoveKnockback: {isKnockback}");

        if (!isKnockback)
        {
            return;
        }

            Debug.Log("実際に移動処理を実行");

        float moveDistance = knockbackSpeed * Time.deltaTime;
        transform.position += knockbackDirection * moveDistance;
        movedDistance += moveDistance;

        if (movedDistance >= knockbackDistance)
        {
            isKnockback = false;

            if (isPlayer)
            {
                playerMove.SetCanMove(true);
            }
            else
            {
                enemyController.SetIsKnockback(isKnockback);
            }
        }
    }
}