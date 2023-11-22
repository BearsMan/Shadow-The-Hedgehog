using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public AudioClip ringSound;
    public ParticleSystem collectParticle;
    private GameManager gameManager;
    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        collectParticle.Stop(); // Stops the particle sound effect from playing once the rings are collected.
    }
    private void Awake()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ParticleSound();
            gameManager.rings++;
            if (collectParticle != null)
            {
                collectParticle.Play(); // Plays the particle sound effect each time a ring is collected.
            }
            Destroy (gameObject);
        }
    }
    private void ParticleSound()
    {
        for (int i = 0; i < 360; i+= 45)
        {
            GameObject ringParticle = Instantiate(particle, transform.position, Quaternion.identity);
            ringParticle.transform.rotation = Quaternion.Euler(0f, i, 0f);
        }
    }
}
