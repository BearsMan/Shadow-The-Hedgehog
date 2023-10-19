using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    public Image itemImage;
    UIManager uiManager;
    public WeaponType weaponTypes;
    public enum WeaponType
    {
        lightWeapon, heavyWeapon, meleeWeapon
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.DisplayInformation(weaponTypes);
            if (Input.GetButtonDown("E"))
            {
                Destroy (gameObject);
            }
        }
    }
}
