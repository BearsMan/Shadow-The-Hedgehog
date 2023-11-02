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
            //uiManager.DisplayInformation(weaponTypes);
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
        if (playerColliding = true && Input.GetKeyDown(KeyCode.E))
        {
                // Debug.Log("Press E to try to pick up the weapon");
                PickUpItem weapon = GetComponent<PickUpItem>();
                player.GetComponent<PlayerMovement>().AddWeapons(weapon);
                Destroy (gameObject);
        }
    }
}
