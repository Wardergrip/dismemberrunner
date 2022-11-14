using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject visuals;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [SerializeField] private float groundDrag;

    [SerializeField] private float jumpForce;
    [SerializeField] private float airMultiplier;

    public float crouchScale = 0.5f;

    [SerializeField] private float fallMultiplier = 2.5f;

    [Header("Keybinds")]
    private const string MOVEMENT_HORIZONTAL = "Horizontal Movement";
    private const string MOVEMENT_VERTICAL = "Vertical Movement";
    private const string JUMP_BUTTON = "Jump";

    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;
    bool isGrounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    float raycastLength = 1.0f;

    Vector3 moveDirection;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        isGrounded = Physics.Raycast(visuals.transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        ProcessInputs();
        SpeedControl();

        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
       MovePlayer();
    }

    private void ProcessInputs()
    {
        horizontalInput = Input.GetAxisRaw(MOVEMENT_HORIZONTAL);
        verticalInput = Input.GetAxisRaw(MOVEMENT_VERTICAL);

        if (Physics.Raycast(transform.position, -orientation.right, raycastLength) && horizontalInput < -float.Epsilon)
        {
            horizontalInput = 0;
        }
        else if (Physics.Raycast(transform.position,orientation.right, raycastLength) && horizontalInput > float.Epsilon)
        {
            horizontalInput = 0;
        }
        if (Physics.Raycast(transform.position, orientation.forward, raycastLength) && verticalInput > float.Epsilon)
        {
            verticalInput = 0;
        }
        else if (Physics.Raycast(transform.position, -orientation.forward, raycastLength) && verticalInput < -float.Epsilon)
        {
            verticalInput = 0;
        }

        if (Input.GetButtonDown(JUMP_BUTTON) && isGrounded)
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x,0f,rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x,rb.velocity.y,limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x,0.0f,rb.velocity.z);

        rb.AddForce(transform.up * jumpForce,ForceMode.Impulse);
    }
}
