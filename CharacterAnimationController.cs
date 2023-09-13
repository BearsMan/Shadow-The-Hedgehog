using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody body;
    private bool isFlying = false;
    private bool isGrounded;
    private bool isUsingChaosBlast, isUsingChaosSpear = false;
    private PlayerMovement Movement;
    public int blueMeterGaugePowerUp = 0;
    public int redMeterGaugePowerUp = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        Movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Movement.isGrounded;
        float horizontalSpeed = new Vector3(body.velocity.x, 0f, body.velocity.z).magnitude;
        if (isGrounded)
        {
            anim.SetFloat("Speed", horizontalSpeed);
            anim.SetBool("Flying", isFlying);
            Input.GetKeyDown(KeyCode.Q);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SpecialAttack();
            }
        }
    }
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
}
