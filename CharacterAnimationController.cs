using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    #region
    // General Variables for player animations
    private Animator anim;
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
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        // This is called at the start of the game function.
        #region
        anim = GetComponent<Animator>();
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
            anim.SetFloat("Speed", smoothSpeed);
            // anim.SetBool("Flying", isFlying);
            Input.GetKeyDown(KeyCode.Q);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SpecialAttack();
            }
            anim.SetBool("Shooting", isShooting);
            anim.SetBool("Punching", isPunching);
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
            anim.SetBool("Flying", true);
        }

        else if (redMeterGaugePowerUp >= 100)
        {
            // Chaos Blast
            anim.SetBool("Flying", false);
        }
    }
    public void TakeDamageAnim()
    {
        // The player's damage will play based on when it is being hit.
        anim.SetTrigger("On Hit");
        Debug.Log("Player is attacked"); // Current animation is bugged at this time.
       // body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // Try and set a delay.
    }
    public void JumpAnim()
    {
        anim.SetTrigger("Jump");
    }
    #endregion
}
