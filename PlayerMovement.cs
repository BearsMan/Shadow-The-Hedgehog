using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Basic Player Movement Variables
    public float moveSpeed = 3f;
    public float jumpForce = 12f;
    public int attackSpeed = 0;
    public Rigidbody body;
    public Transform cameraTransform;
    public bool isGrounded = false;
    public bool canAttack = true;
    public float shootingTimerCoolDown = 10.0f;
    public float attackCoolDown = 1f;
    public float sprintThreshold = 3f;
    private float holdTimeSprint = 0f;
    private bool isSprinting = false;
    private CharacterAnimationController animController;

    private Rigidbody rb;

    private void Start()
    {
        // Updates called at start of frame
        rb = GetComponent<Rigidbody>();
        animController = GetComponent<CharacterAnimationController>();
    }

    private void Update()
    {
        // Updates called per frame during Gameplay based on the action.
        // Ground check using raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        bool isForwardPress = Input.GetAxis("Vertical") > 0;
        if (isForwardPress)
        {
            // Counts the timer
            holdTimeSprint += Time.deltaTime;
            // Checks if the button is being held down for 3 seconds.
            if (holdTimeSprint >= sprintThreshold && !isSprinting)
            {
                isSprinting = true;
                moveSpeed = 5f;
            }
        }
        else
        {
            // Sets the speed controls back to the default value if the player is not moving.
            holdTimeSprint = 0f;
            isSprinting = false;
            moveSpeed = 3f;
        }

        // Get camera's forward and right directions
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Horizontal movement relative to camera
        float moveInputHorizontal = Input.GetAxis("Horizontal");
        float moveInputVertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = (cameraForward * moveInputVertical + cameraRight * moveInputHorizontal).normalized;

        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);

        // Look at movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Jumping mechanics
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        // Attacks for characters
        if (Input.GetMouseButtonDown(0) && isGrounded && canAttack)
        {
            NormalAttack();

        }
        if (Input.GetKeyDown(KeyCode.B) && isGrounded && canAttack)
        {
            Shoot();
        }
    }


    // Setup Normal Attack for Character
    public void NormalAttack()
    {
        canAttack = false;
        animController.isPunching = true;
        Invoke("ResetAttackCoolDown", attackCoolDown);
    }
    private void ResetAttackCoolDown()
    {
        canAttack = true;
        animController.isShooting = false;
        animController.isPunching = false;
    }
    public void Shoot()
    {
        canAttack = false;
        animController.isShooting = true;
        Invoke("ResetAttackCoolDown", 1f);
    }
}