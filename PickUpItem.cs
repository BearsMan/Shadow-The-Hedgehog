using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    public GameObject weaponPrefab;
    public Image itemImage;
    UIManager uiManager;
    public WeaponType weaponTypes;
    private bool playerColliding = false;
    private GameObject player;
    public enum WeaponType
    {
        lightWeapon, heavyWeapon, meleeWeapon
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerColliding = true;
            // uiManager.DisplayInformation(weaponTypes);
            player = other.gameObject;
            // Debug.Log("Colliding");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerColliding = false;
    }
    private void Update()
    {
        if (playerColliding = true && Input.GetKeyDown(KeyCode.E)) // If set to false, the weapon cannot not be picked up by the user.
        // else if set to true, the weapon can be collected by the user.
        {
                // Debug.Log("Press E to try to pick up the weapon");
                PickUpItem weapon = GetComponent<PickUpItem>();
                player.GetComponent<WeaponSystem>().AddWeapons(weapon);
                Destroy (gameObject);
        }
    }
}
