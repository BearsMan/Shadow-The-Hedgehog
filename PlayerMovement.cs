using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region
    // Basic Player Movement Variables
    public float moveSpeed = 3f;
    public float jumpForce = 12f;
    public int attackSpeed = 0;
    public Rigidbody body;
    public Transform cameraTransform;
    public Transform weaponAnchor;
    public bool isGrounded = false;
    public bool isJumping = false;
    public bool canAttack = true;
    public float detectionRadius = 10f;
    public float shootingTimerCoolDown = 10.0f;
    public float attackCoolDown = 1f;
    public float sprintThreshold = 3f;
    private float holdTimeSprint = 0f;
    private bool isSprinting = false;
    private CharacterAnimationController animController;
    private RangeWeapon weapons;
    public GameObject currentWeapon;
    #endregion
    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        animController = GetComponent<CharacterAnimationController>();
        weapons = currentWeapon.GetComponent<RangeWeapon>();
    }
    private Animator anim;
    // Update is called once per frame
    private void Update()
    {
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
        #region
        // Get camera's forward and right directions
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Horizontal movement relative to camera
        float moveInputHorizontal = Input.GetAxis("Horizontal");
        float moveInputVertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = (cameraForward * moveInputVertical + cameraRight * moveInputHorizontal).normalized;

        body.velocity = new Vector3(moveDirection.x * moveSpeed, body.velocity.y, moveDirection.z * moveSpeed);

        // Look at movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Jumping mechanics
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            body.velocity = new Vector3(body.velocity.x, jumpForce, body.velocity.z);
        }

        // Attacks for characters
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            NormalAttack();
        }
        if (Input.GetKeyDown(KeyCode.B) && isGrounded)
        {
            Shoot();
        }
        #endregion
    }
    #region
    // Input for characters handling controls.
    private void InputHandler()
    {
        
    }
    // Setup Normal Attack for Character
    public void NormalAttack()
    {
        canAttack = false;
        animController.isPunching = true;
        Invoke("ResetAttackCoolDown", attackCoolDown);
    }
    // Create Homing Attack Controls
    public void HomingAttack(Transform nearestEnemy)
    {
          if (!isGrounded)
        {
            transform.position = Vector3.Lerp(transform.position, nearestEnemy.position, 2f);

            // Check if character is jumping with the corresponding key.
            if (isJumping)
            {
                Input.GetKeyDown(KeyCode.A);
            }
        }
    }
    // Find the nearest enemy closest to the player.
    public Transform FindNearestEnemy()
    {
        GameObject[] listEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (listEnemies.Length == 0)
        {
            return null;
        }
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        foreach (GameObject Enemy in listEnemies)
        {
            float distance = Vector3.Distance(transform.position, Enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = Enemy.transform;
            }
        }
        return nearestEnemy;
    }
    // Function for Resets on Animation Cooldowns.
    private void ResetAttackCoolDown()
    {
        canAttack = true;
        animController.isShooting = false;
        animController.isPunching = false;
    }
    // Function to shoot with Shadow's Gun.
    public void Shoot()
    {
        weapons.ShootCurrentWeapon();
        canAttack = false;
        animController.isShooting = true;
        Invoke("ResetAttackCoolDown", 1f);
    }
    // Adds weapons to characters
    public void AddWeapons(PickUpItem Weapons)
    {
        Destroy (currentWeapon);
        GameObject newWeapon = Instantiate(Weapons.weaponPrefab, weaponAnchor);
        newWeapon = currentWeapon;
        weapons = currentWeapon.GetComponent<RangeWeapon>();
    }
    #endregion
}