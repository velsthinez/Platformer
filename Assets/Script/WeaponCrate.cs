using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCrate : MonoBehaviour
{
    public GameObject WeaponPickup;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
            return;

        if (WeaponPickup == null)
            return;
        
        WeaponHandler weaponHandler =  col.GetComponent<WeaponHandler>();

        GameObject go = GameObject.Instantiate(WeaponPickup, transform.position, transform.rotation);

        Weapon weapon = go.GetComponent<Weapon>();
        
        weaponHandler.EquipWeapon(weapon);
        // weaponHandler.CurrentWeapon = WeaponPickup;
        
        Destroy(this.gameObject);
    }
}
