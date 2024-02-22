using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    #region
    // General Variables for player animations
    public Animator currentAnim; // The current animation is played at the start of the gameplay.
    private Rigidbody body;
    private bool isFlying = false;
    public bool isShooting = false;
    public bool isPunching = false;
    private bool isGrounded;
    private bool isUsingChaosBlast, isUsingChaosSpear = false;
    public float smoothSpeed = 0f;
    public float accelerationSpeed = 2.0f;
    private PlayerMovement movement;
    public int blueMeterGaugePowerUp = 0;
    public int redMeterGaugePowerUp = 0;
    public Animator flyingAnim;
    public Animator groundAnim;
    // private Animator currentAnim;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        // This is called at the start of the game function.
        #region
        currentAnim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        movement = GetComponent<PlayerMovement>();
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        // Called when played per frame.
        #region
        isGrounded = movement.isGrounded;
        float horizontalSpeed = new Vector3(body.velocity.x, 0f, body.velocity.z).magnitude;
        smoothSpeed = Mathf.Lerp(smoothSpeed, horizontalSpeed, Time.deltaTime * accelerationSpeed);
        {
            // Animations when playing the correct key.
            currentAnim.SetFloat("Speed", smoothSpeed);
            // currentAnim.SetBool("Flying", isFlying);
            Input.GetKeyDown(KeyCode.Q);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SpecialAttack();
            }
            currentAnim.SetBool("Shooting", isShooting);
            currentAnim.SetBool("Punching", isPunching);
        }
    }
    #endregion

    // Start area for functions to be called.
    #region
    public void SpecialAttack()
    {
        if (blueMeterGaugePowerUp >= 100)
        {
            // Chaos Control or Chaos Spear Move
            currentAnim.SetBool("Flying", true);
        }

        else if (redMeterGaugePowerUp >= 100)
        {
            // Chaos Blast
            currentAnim.SetBool("Flying", false);
        }
    }
    public void TakeDamageAnim()
    {
        // The player's damage will play based on when it is being hit.
        currentAnim.SetTrigger("On Hit");
        // Debug.Log("Player is attacked"); // The current animation is bugged at this time.
        // body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // Try and set a delay.
    }
    public void JumpAnim()
    {
        currentAnim.SetTrigger("Jump");
    }
    public void Death()
    {
        currentAnim.SetBool("Death", true);
        currentAnim.enabled = false;
    }
    #endregion
}
