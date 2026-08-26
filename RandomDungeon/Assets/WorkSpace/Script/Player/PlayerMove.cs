using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private PlayerData playerData = null;
    private Vector3 moveDirection = Vector3.zero;
    private bool canMove = false;

    private void Start()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        moveDirection = Vector3.zero;
    }
    private void Update()
    {
        MoveInput();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void MoveInput()
    {
        moveDirection = Vector3.zero;

        if (!canMove)
        {
            return;
        }

        if (Keyboard.current.wKey.isPressed)
        {
            moveDirection += transform.forward;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            moveDirection -= transform.forward;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            moveDirection -= transform.right;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            moveDirection += transform.right;
        }

        moveDirection.Normalize();
    }
    private void Move()
    {
        Vector3 horizontalDelta;
        horizontalDelta.x = moveDirection.x * playerData.moveSpeed * Time.fixedDeltaTime;
        horizontalDelta.y = 0.0f;
        horizontalDelta.z = moveDirection.z * playerData.moveSpeed * Time.fixedDeltaTime;

        Vector3 targetPosition = rb.position + horizontalDelta;

        rb.MovePosition(targetPosition);

        if(rb.linearVelocity != Vector3.zero)
        {
            rb.linearVelocity = Vector3.zero;
        }

        if(rb.angularVelocity != Vector3.zero)
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
    public void SetCanMove(bool value)
    {
        canMove = value;
        if (!canMove)
        {
            moveDirection = Vector3.zero;
        }
    }
}