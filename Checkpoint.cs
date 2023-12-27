using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public ParticleSystem SparkleControl;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.SetCheckPoint(transform);
            ParticleSystem particle = Instantiate(SparkleControl, transform);
            Destroy (particle, 1f);
        }
    }
}
