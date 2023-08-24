using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody rb;
    public float jumpForce;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundMask;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
        MovementController();
    }
    public void MovementController()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // calculate movement
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        if (rb.velocity.magnitude > 0.1f)
        {
            Vector3 target = Camera.main.transform.TransformDirection(moveDirection);
            target.y = 0f;
            rb.velocity = target * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
