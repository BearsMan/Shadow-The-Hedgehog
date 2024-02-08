using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public AudioClip ringSound;
    public bool isFlashing = false;
    public bool isSpinning = false;
    public float rotationSpeed = 100000f;
    public float maxScale = 10f;
    public ParticleSystem collectParticle;
    private GameManager gameManager;
    public GameObject particle;
    private Vector3 initialPosition;
    private Vector3 offSet;
    // Start is called before the first frame update
    void Start()
    {
        collectParticle.Stop(); // Stops the particle sound effect from playing once the rings are collected.
        offSet = transform.position + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
        initialPosition = transform.position;
        float angle = 45f * Mathf.Deg2Rad;
        Vector3 initialVelocity = new Vector3(0 * Mathf.Cos(angle), 5f * Mathf.Sin(angle), 0);
    }
    private void Awake()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpinning)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            float time = Time.time - Time.fixedTime;
           // Vector3 newPosition = initialPosition * time + (0.5f * Physics.gravity * time * time);
           // transform.position = newPosition;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ParticleSystem particle = Instantiate(collectParticle, transform);
            Destroy(particle, 1f);
            gameManager.AddRings();
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
    public void FlashAndDisappear()
    {
        isSpinning = true;
        InvokeRepeating("ToggleVisibility", Time.deltaTime, 0.5f);
        StartCoroutine(DelayAndDestroy(5f));
    }
    void ToggleVisibility()
    {
       gameObject.SetActive(!gameObject.activeSelf);
    }
    private IEnumerator DelayAndDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy (gameObject);

    }
}
