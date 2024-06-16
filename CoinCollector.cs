using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private Rigidbody coinBody;
    // Start is called before the first frame update
    void Start()
    {
        coinBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin Stop"))
        {
            coinBody.constraints = RigidbodyConstraints.FreezeAll;
            // Notes for next week: Round to proper coordinates.
        }
    }
}
