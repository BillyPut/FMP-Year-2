using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickupScript : MonoBehaviour
{
    public WeaponSwitching weaponSwitching;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, Time.deltaTime * 100f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            weaponSwitching.weaponAmount += 1;
            Destroy(gameObject);
        }
    }
}
