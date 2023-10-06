using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    // Checks how much speed, and range the weapon has, and uses.
    public float speed = 3f;
    public float range = 5f;
    // Start is called before the first frame update
    void Start()
    {
        // Destroys the gameobject, and the range that the weapon fires at.
        Destroy(gameObject, range);
    }

    // Update is called once per frame
    void Update()
    {
        // Fires the bullet in forward direction.
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
