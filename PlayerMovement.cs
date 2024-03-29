using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region
    // Basic Player Movement Variables
    [Header("Movements")]
    public float moveSpeed = 3f;
    public float jumpForce = 12f;
    public int attackSpeed = 0;
    public Rigidbody body;
    public Transform cameraTransform;
    public bool isGrounded = false;
    public bool isJumping = false;
    public bool isFlying = false;
    public bool canAttack = true;
    public float detectionRadius = 10f;
    public float shootingTimerCoolDown = 10.0f;
    public float attackCoolDown = 1f;
    public float sprintThreshold = 3f;
    private float holdTimeSprint = 0f;
    private bool isSprinting = false;
    private bool canShoot = true; // Sets to false when the main player is hit or knocked out.
    private bool hasBeenDamaged = false;
    public Material standardForm1; // Normal Form for Shadow.
    public Material standardForm2; // Same material object as standard form 1.
    public Material superForm1; // Super Form for Shadow.
    public Material superForm2; // Same material as super form 1.
    public SkinnedMeshRenderer objectMaterialRender1;
    public SkinnedMeshRenderer objectMaterialRender2;
    private CharacterAnimationController animController;
    private WeaponSystem weaponController;
    public PickUpItem energyBlast;
    private bool inAir;
    private States currentStates;
    [Header("Audio")]
    /*
    These audio files should only play whenever the red or blue bars for the attacks are filled, and it should never play in a loop.
    Plays Audio Source for Attacks
    */
    public AudioClip chaosBlast;
    /*
    Plays Audio Clip for Chaos Blast when the red bar is filled, and will only fill when the player takes damage.
    */
    public AudioClip chaosControl;
    /*
     Plays audio clip for "Chaos Control" when the blue bar is filled, and will only fill when the enemies take damage.
    */
    public AudioClip chaosSpear;
    // Plays Audio Clip for the Chaos Spear whenever the blue bar is filled, however, it is spilt into 2 audio clips.
    [Header("")]
    // Plays audio source for Chaos Control.
    [Header("")]
    // Plays audio source for Chaos Spear. "Note" this attack is only used when you have enough rings.

    [Header("")]
    // This sound should only play when the player loses a certain amount of rings after the enemy hits you.

    private AudioSource audioSource;
    public AudioClip ringGain;
    public AudioClip ringLost;
    // Ends audio source play after using the correct attacks based on the color on the bar that is being filled.
    #endregion

    private enum States
    {
        flying,
        idle,
        inAir,
        powerUp,
        running,
        shooting,
        homingAttack
    }
    // Start is called before the first frame update
    private void Start()
    {
        GetComponents();
        currentStates = States.idle;
    }
    private Animator anim;

    // Update is called once per frame
    [System.Obsolete]
    private void Update()
    {
        // Start of Flying animation when transforming into Super Shadow.
        switch (currentStates)
        {
            case States.powerUp:
                break;
            case States.flying: 
                break;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnHit();
            /*
             Debug.Log("Spawn Rings"); 
            */
            /*This is only used for debugging.
            */
        }
        // Ground check using raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        bool isForwardPress = Input.GetAxis("Vertical") > 0;
        if (isForwardPress && !isFlying)
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
            animController.JumpAnim();
        }

        // Attacks for characters
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            NormalAttack();
        }
        if (Input.GetKeyDown(KeyCode.B )&& canShoot)
        {
            weaponController.Shoot();
            animController.isShooting = true;
            if (!isGrounded)
            {
                moveSpeed = 0f;
                body.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
        }
        else if (Input.GetKeyUp(KeyCode.B))
        {
            StartCoroutine(FallDelay(5f));
        }
        if (isFlying)
        {
            isFlying = false;
            FlyController();
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
        Invoke(nameof(ResetAttackCoolDown), attackCoolDown);
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
    public void SoundEffect()
    {
        // Plays the correct sound effect based on the stage played, and the attack patterns being called.
    }

    private IEnumerator FallDelay(float seconds)
    {
        
        yield return new WaitForSeconds(seconds); // Re-use the delay at anytime.
        canShoot = true;
        hasBeenDamaged = false;
        body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // This will auto-unfreeze the movement.
    }
    private void GetComponents()
    {
        audioSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody>();
        animController = GetComponent<CharacterAnimationController>();
        weaponController = GetComponent<WeaponSystem>();
    }

    [System.Obsolete]
    private void FlyController()
    {
        body.constraints = RigidbodyConstraints.FreezePositionY;
        // animController.currentAnim = animController.currentAnim;
        ChangeToSuperForm();
    }

    [System.Obsolete]
    public void ChangeToSuperForm()
    {
        objectMaterialRender1.material = superForm1;
        objectMaterialRender2.material = superForm2;
        weaponController.AddWeapons(energyBlast);
        GameManager.instance.isInSuperForm = true; // When not playing the game scene, revert back to normal form.
    }
    public void OnHit()
    {
        if (!hasBeenDamaged)
        {
            hasBeenDamaged = true;
            canShoot = false;
            audioSource.PlayOneShot(ringLost);
            GameManager.instance.PlayerDamage(10f, (transform.position));
            animController.TakeDamageAnim();
            /*
            Debug.Log("Input Press"); 
            Select an input when the letter I is pressed.
            */
            body.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            StartCoroutine(FallDelay(1.5f));
        }
    }
    #endregion
}