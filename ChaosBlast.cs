using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosBlast : MonoBehaviour
{
    public float expansionRate = 1.0f;
    public float duration = 5.0f;
    public float targetScale = 10.0f;
    private Transform sphereTransform;
    // Start is called before the first frame update
    void Start()
    {
        sphereTransform = transform;
        StartCoroutine(ExpandsOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Debug.Log("Hit Enemy");
            other.GetComponent<Health>().OnHit(1000);
        }
    }
    private IEnumerator ExpandsOverTime()
    {
        Vector3 initialScale = sphereTransform.localScale;
        Vector3 targetVectorScale = Vector3.one * targetScale;
        float elipseTime = 0f;
        while (elipseTime < duration)
        {
            sphereTransform.localScale = Vector3.Lerp(initialScale, targetVectorScale, elipseTime / duration);
            elipseTime += Time.deltaTime * expansionRate;
            yield return null;
        }
        sphereTransform.localScale = targetVectorScale;
        Destroy (gameObject);
    }
}
