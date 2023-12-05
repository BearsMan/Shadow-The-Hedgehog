using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public AudioClip ringSound;
    public float maxScale = 10f;
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
        for (int i = 0; i < 8; i++)
        {
            Vector3 direction = Quaternion.Euler(0, i * 45, 0) * transform.forward;
            GameObject ringParticle = Instantiate(particle, transform.position, Quaternion.identity);
            ringParticle.transform.forward = direction;
            ringParticle.transform.forward = Vector3.zero;
            StartCoroutine(PushTrail(ringParticle));
        }
    }
    private IEnumerator PushTrail(GameObject trail)
    {
        float currentScale = 0.0f;
        Vector3 targetScale = new Vector3(maxScale, maxScale, maxScale);
        while (currentScale < maxScale)
        {
            currentScale += Time.deltaTime;
            transform.position += transform.forward * Time.deltaTime;
            // trail.transform.position = Vector3.Lerp(Vector3.zero, targetScale, currentScale / maxScale);
            yield return null;
        }
        Destroy (trail);
    }
}
