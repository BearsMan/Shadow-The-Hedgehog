using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public AudioClip ringSound;
    public GameManager gameManager;
    public ParticleSystem collectParticle;
    // Start is called before the first frame update
    void Start()
    {
        collectParticle.Stop(); // Stops the particle sound effect from playing once the rings are collected.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.rings++;
            if (collectParticle != null)
            {
                collectParticle.Play(); // Plays the particle sound effect each time a ring is collected.
            }
            Destroy (gameObject);
        }
    }
}
