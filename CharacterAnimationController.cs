using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody body;
    private bool isGrounded;
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = playerMovement.isGrounded;
        float horizontalSpeed = new Vector3(body.velocity.x, 0f, body.velocity.z).magnitude;
        if (isGrounded)
        {
            anim.SetFloat("Speed", horizontalSpeed);
        }
    }
}
