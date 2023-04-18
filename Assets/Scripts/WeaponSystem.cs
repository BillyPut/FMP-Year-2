using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponSystem : MonoBehaviour
{

    public Transform cam;
    [SerializeField]
    private LayerMask target;
    [SerializeField] GunData gunData;
    public float firingTime;
    private int ammoDecrease;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        firingTime -= Time.deltaTime;

        Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red);

        if (Input.GetMouseButton(0) && firingTime <= 0 && gunData.ammo > 0)
        {
            RaycastHit laserHit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out laserHit, target))
            {
                Debug.Log(laserHit.collider.name);
                
                if (laserHit.collider.tag == "Enemy")
                {
                    laserHit.transform.SendMessageUpwards("TakeDamage", gunData.damage);
                }


            }

            gunData.ammo -= 1;
            firingTime = gunData.fireRate;

        }

        if (Input.GetKeyDown("r") && gunData.reloading == false && gunData.ammo != gunData.magsize && this.gameObject.activeSelf) 
        {
            StartCoroutine(Reload());
        }


    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        ammoDecrease = gunData.magsize - gunData.ammo;
        //gunData.overallAmmo -= ammoDecrease;

        gunData.ammo = gunData.magsize;

        gunData.reloading = false;


    }

    private void OnDisable()
    {
        gunData.reloading = false;
    }


}
