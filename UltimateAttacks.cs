using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateAttacks : MonoBehaviour
{
    private bool chaosBlastAttack = false;
    private bool powerUpActive = true;
    public GameObject chaosBlastSphereForm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpActive && Input.GetButtonDown("Power Up"))
        {
            // To use either Chaos Control or Chaos Blast, depending on the color of the bar being hit by the most.
            if (chaosBlastAttack)
            {
                // Chaos Blast is being used.
                Instantiate(chaosBlastSphereForm);
            }
            else
            {
                // Chaos Control is being used instead.
            }
            /*
            powerUpActive = false; 
            Set to true when timer for power up is active.
            */
        }
    }
    // Setup Ultimate Attacks for Shadow
    public void SuperAttack(bool chaosBlast)
    {
        chaosBlastAttack = chaosBlast;
        if (chaosBlast)
        {
            // Make sure that the red bar is completely filled or else the attack cannot be called.
        }
        else
        {
            // Use Chaos Control when the blue bar is filled, depending on how the player reacts.
        }
    }
}
