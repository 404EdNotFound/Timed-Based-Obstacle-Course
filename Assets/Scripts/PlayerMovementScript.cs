using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    public bool onGround = true;
    PlayerInput player;
    InputAction move;
    InputAction jump;
    InputAction look;
    Rigidbody rb;

    public float jumpValue = 5f;
    public float playerSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerInput>();
        move = player.actions.FindAction("Move");
        jump = player.actions.FindAction("Jump");
        look = player.actions.FindAction("Look");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        JumpPlayer();
        TurnPlayer();
    }

    private void MovePlayer() {
        Vector3 moveDirection = move.ReadValue<Vector3>();
        Vector3 position = (transform.forward * moveDirection.z + transform.right * moveDirection.x).normalized;
        rb.MovePosition(rb.position + position * playerSpeed * Time.deltaTime);
    }

    private void JumpPlayer() {
    if (onGround && jump.IsPressed())
        {
            onGround = false;
            rb.AddForce(Vector3.up * jumpValue, ForceMode.Impulse);
        }
            }

    private void TurnPlayer() {
        float rotateDirection = look.ReadValue<float>();
        Debug.Log(50f * playerSpeed);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotateDirection * (50f * playerSpeed) * Time.deltaTime, 0f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        onGround = true;
    }
}
