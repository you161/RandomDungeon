using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private PlayerData playerData = null;
    [SerializeField] private float collisionRadius = 0.5f;
    [SerializeField] private LayerMask wallLayer;
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
        if (rb.linearVelocity != Vector3.zero)
        {
            rb.linearVelocity = Vector3.zero;
        }

        float moveDistance = playerData.moveSpeed * Time.fixedDeltaTime;

        Vector3 origin = rb.position + Vector3.up * 0.5f;

        bool isHit = Physics.SphereCast(
            origin,
            collisionRadius,
            moveDirection,
            out RaycastHit hit,
            moveDistance,
            wallLayer
        );

        if (isHit)
        {
            return;
        }

        rb.MovePosition(rb.position + moveDirection * moveDistance);
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