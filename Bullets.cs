using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Bullets : MonoBehaviour
{
    // Checks how much speed, and range the weapon has, and uses.
    public float speed = 10f;
    public float range = 5f;
    // Start is called before the first frame update
    void Start()
    {
        // Destroys the gameobject, and the range that the weapon fires at.
        Destroy (gameObject, range);
    }

    // Update is called once per frame
    void Update()
    {
        // Fires the bullet in forward direction.
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Weapons"))
        {
            
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            // Try to damage the enemy.
        }
        else
        {
            Destroy (gameObject);
        }
    }
}
