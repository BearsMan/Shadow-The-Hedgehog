using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    PlayerMovement movement;
    private float currentAmmo = 1000f;
    private float maxAmmo;
    public GameObject weaponPrefabList;
    public GameObject projectilePrefab;
    public RangeWeapon weaponPrefab;
    public Transform barrel;
    public Transform offSet;
    public AudioSource audioSource;
    public AudioClip gunSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Target for shooting a weapon with the amount of ammo used.
    public void ShootCurrentWeapon()
    {
        Instantiate(projectilePrefab, barrel.position, offSet.rotation);
        if (gunSound && audioSource != null)
        {
            audioSource.PlayOneShot(gunSound);
        }
        
        if (currentAmmo > 0)
        {
            currentAmmo--;
            if (currentAmmo <= 0)
            {
                // The player's weapon is out of ammo, please replace your weapon.
            }
        } 
    }
}
