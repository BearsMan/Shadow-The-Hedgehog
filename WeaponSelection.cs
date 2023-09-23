using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    public int numberOfGuns = 0;
    public bool gunHolder = false;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Animation anim = rb.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}