using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "WeaponSystem/Weapon")]
public class WeaponsInfo : ScriptableObject
{
    public float attackSpeed;
    public GameObject projectile;
    public GameObject weapon;
    public int damage;
    public string weaponName;
    public Transform barrel;
    public enum weaponType
    {
        lightWeapon, heavyWeapon, meleeWeapon
    }
    
}
