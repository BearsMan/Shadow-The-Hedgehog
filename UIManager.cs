using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<Sprite> weaponIcons = new List<Sprite>();
    public Image weaponsImage;
    public void DisplayInformation(PickUpItem.weaponType weapons)
    {
        GetComponentInChildren<Image>(weaponsImage);
        switch (weapons)
        {
            case PickUpItem.weaponType.lightWeapon:
                weaponsImage.sprite = weaponIcons[0];
                break;

            case PickUpItem.weaponType.heavyWeapon:
                weaponsImage.sprite = weaponIcons[1];
                break;

            case PickUpItem.weaponType.meleeWeapon:
                weaponsImage.sprite = weaponIcons[2];
                break;
        }
    }
}
