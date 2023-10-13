using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    public Image itemImage;
    public GameObject itemPrefab;
    WeaponsInfo weaponInfo;
    UIManager uiManager;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Pull enum information from WeaponsInfo.
            // UIManager.DisplayInformation(WeaponsInfo.WeaponType);

            // Hook into UI Manager.
            if (Input.GetButtonDown("E"))
            {
                Destroy(itemPrefab);
            }
        }
    }
    private void ShowItem()
    {
        
    }
}
