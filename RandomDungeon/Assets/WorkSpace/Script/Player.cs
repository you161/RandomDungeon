using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform playerCamera = null;
    [SerializeField] private float mouseSensitivity = 0;
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private float moveSpeed = 0;

    private float xRotation = 0f;
    private Vector3 moveDirection = Vector3.zero;

    private void Start()
    {
        // マウスカーソルを画面中央に固定
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Look();
        MoveInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Look()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
    private Vector3 MoveInput()
    {
        moveDirection = Vector3.zero;

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

        return moveDirection;
    }
    private void Move()
    {
        Vector3 velocity = rb.linearVelocity;

        velocity.x = moveDirection.x * moveSpeed;
        velocity.z = moveDirection.z * moveSpeed;

        rb.linearVelocity = velocity;
    }
}