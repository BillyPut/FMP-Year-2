using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmmount;
    public string ammoType;
    public WeaponSwitching weapon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            weapon.UseAmmoPickup(ammoAmmount, ammoType);
            Destroy(gameObject);
        }
    
    }
}
