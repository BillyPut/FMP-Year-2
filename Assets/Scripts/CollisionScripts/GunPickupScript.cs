using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickupScript : MonoBehaviour
{
    public WeaponSwitching weaponSwitching;
    public GameObject gunText;
    public DoorOpenShut doorOpen, doorShut;

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
            gunText.SetActive(true);
            doorOpen.moveIt = true;
            doorShut.moveIt = true;
            Destroy(gameObject);
        }
    }
}
