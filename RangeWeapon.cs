using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    private PlayerMovement movement;
    private float currentAmmo = 1000f;
    private float maxAmmo;
    // public GameObject weaponPrefabList;
    public GameObject projectilePrefab;
    public RangeWeapon weaponPrefab;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip gunSound;
    public Quaternion offSet;
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
        GameObject projectile = Instantiate(projectilePrefab, barrel.position, barrel.rotation);
        projectile.transform.parent = null;
        if (gunSound && audioSource != null)
        {
            audioSource.PlayOneShot(gunSound);
        }
        
        if (currentAmmo > 0)
        {
            currentAmmo--;
            if (currentAmmo <= 0)
            {
                // Check if the weapon is out of ammo.
                Debug.Log("The player's weapon is out of ammo, please replace your weapon.");
            }
        } 
    }
}
