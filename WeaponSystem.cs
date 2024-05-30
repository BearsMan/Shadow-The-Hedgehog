using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class WeaponSystem : MonoBehaviour
{
    private RangeWeapon weapons;
    public GameObject currentWeapon;
    public Transform weaponAnchor;
    private bool canAttack = true;
    private bool isGrounded = false;
    private Rigidbody body;
    private CharacterAnimationController animController;
    private void Start()
    {
        // weapons = currentWeapon.GetComponent<RangeWeapon>();
        body = GetComponent<Rigidbody>();
        animController = GetComponent<CharacterAnimationController>();
    }
    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        if (Input.GetKeyDown(KeyCode.B) && canAttack)
        {
            Shoot();
            animController.isShooting = true;
            if (!isGrounded)
            {
                // moveSpeed = 0f;
                body.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
            }
        }
        else if (Input.GetKeyUp(KeyCode.B))
        {
            StartCoroutine(FallDelay(5f));
        }
    }
    private IEnumerator FallDelay(float seconds)
    {

        yield return new WaitForSeconds(seconds);
        canAttack = true;
        // hasBeenDamaged = false;
        body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY; // This will auto-unfreeze the movement.
    }
    public void Shoot()
    {
        if (currentWeapon != null)
        {
            weapons.ShootCurrentWeapon();
        }
        /*
        canAttack = false;
        Invoke("ResetAttackCoolDown", 1f);
        */
    }
    // Adds weapons to characters
    public void AddWeapons(PickUpItem Weapons)
    {
        Destroy (currentWeapon);
        GameObject newWeapon = Instantiate(Weapons.weaponPrefab, weaponAnchor);
        currentWeapon = newWeapon;
        weapons = currentWeapon.GetComponent<RangeWeapon>();
        currentWeapon.transform.localPosition = Vector3.zero;
        // currentWeapon.transform.localRotation = Quaternion.identity;
    }
    private void ResetAttackCoolDown()
    {
        canAttack = true;
    }
}
