using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingAttack : MonoBehaviour
{
    private bool aerialAttackActive;
    private CharacterAnimationController animController;
    public float nullAttackForce = 1f;
    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<CharacterAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && !aerialAttackActive)
        {
            aerialAttackActive = true;
        }
        if (isGrounded)
        {
            aerialAttackActive = false;
        }
        if (aerialAttackActive)
        {
            OnHomingAttack(FindNearestEnemy());
        }
    }
    // Create Homing Attack Controls
    public void OnHomingAttack(Transform nearestEnemy)
    {
        {
            transform.position = Vector3.MoveTowards(transform.position, nearestEnemy.position, nullAttackForce * Time.deltaTime);
            if (Vector3.Distance(transform.position, nearestEnemy.position) < 0.01f)
            {
                transform.position = nearestEnemy.position;
                aerialAttackActive = false;
            }
            // transform.position = Vector3.Lerp(targetPosition, nearestEnemy.position, 20f * Time.deltaTime);
            animController.SetAerialAttack(true);
        }
    }
    // Find the nearest enemy closest to the player.
    public Transform FindNearestEnemy()
    {
        GameObject[] listEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (listEnemies.Length == 0)
        {
            Transform shadowForward = transform;
            shadowForward.position = transform.position + transform.forward * nullAttackForce;
            aerialAttackActive = true;
            return shadowForward;
        }
        else
        {
            Transform nearestEnemy = null;
            float shortestDistance = Mathf.Infinity;
            foreach (GameObject Enemy in listEnemies)
            {
                float distance = Vector3.Distance(transform.position, Enemy.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestEnemy = Enemy.transform;
                }
            }
            return nearestEnemy;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>()&& aerialAttackActive)
        {
            collision.gameObject.GetComponent<Health>().OnHit(50);
            StartCoroutine(Delay(0.2f));
            aerialAttackActive = false;
        }
    }
    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
