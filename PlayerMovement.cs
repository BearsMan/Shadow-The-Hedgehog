using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody body;
    public float jumpForce;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundMask;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
        Move();
    }
    public void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // calculate movement
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        if (body.velocity.magnitude > 0.1f)
        {
            Vector3 target = Camera.main.transform.TransformDirection(moveDirection);
            target.y = 0f;
            body.velocity = target * speed;
        }
        else
        {
            body.velocity = Vector3.zero;
        }
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
