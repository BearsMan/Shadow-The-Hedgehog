using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    GameManager manager; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Player"))
        {
            CharacterAnimationController charAnimController = gameObject.GetComponent<CharacterAnimationController>();
            if (other.gameObject.CompareTag("Enemy")|| other.gameObject.CompareTag("Projectile"))
            {
                // Gets the component for when the player is takes damage.
                float damage = 0f;
                // manager.PlayerDamage(damage);
                charAnimController.TakeDamageAnim();
            }
        }
    }
}
