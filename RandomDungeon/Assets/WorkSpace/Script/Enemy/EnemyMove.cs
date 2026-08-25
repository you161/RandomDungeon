using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Transform targetPosition = null;
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private Rigidbody rb = null;

    private Vector3 moveDirection = Vector3.zero;
    private bool canMove = false;

    private void Start()
    {
        moveDirection = Vector3.zero;
        canMove = false;
    }

    private void Update()
    {
        moveDirection = targetPosition.position - transform.position;
        moveDirection.y = 0.0f;

        if (moveDirection.sqrMagnitude != 0.0f)
        {
            moveDirection.Normalize();
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }
    }

    private void Move()
    {
        rb.MovePosition(rb.position + moveDirection * enemyData.moveSpeed * Time.fixedDeltaTime);
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}