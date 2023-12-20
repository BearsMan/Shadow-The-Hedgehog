using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    private RangeWeapon weapons;
    public GameObject currentWeapon;
    public Transform weaponAnchor;
    private bool canAttack = true;
    private void Start()
    {
        weapons = currentWeapon.GetComponent<RangeWeapon>();
    }
    public void Shoot()
    {
        weapons.ShootCurrentWeapon();
        canAttack = false;
        Invoke("ResetAttackCoolDown", 1f);
    }
    // Adds weapons to characters
    public void AddWeapons(PickUpItem Weapons)
    {
        Destroy (currentWeapon);
        GameObject newWeapon = Instantiate(Weapons.weaponPrefab, weaponAnchor);
        currentWeapon = newWeapon;
        weapons = currentWeapon.GetComponent<RangeWeapon>();
        Quaternion offSet = weapons.offSet;
        // Quaternion offSet2 = Quaternion.Euler(offSet);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = offSet;
    }
    private void ResetAttackCoolDown()
    {
        canAttack = true;
        
    }
}
