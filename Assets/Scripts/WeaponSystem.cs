using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponSystem : MonoBehaviour
{

    public Transform cam;
    [SerializeField]
    private LayerMask target;
    [SerializeField] public GunData gunData;
    public float firingTime;
    private int ammoDecrease;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        gunData.ammo = gunData.magsize;
    }

    // Update is called once per frame
    void Update()
    {
        firingTime -= Time.deltaTime;

        Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red);


        if (Input.GetMouseButton(0) && firingTime <= 0 && gunData.ammo > 0 && gunData.reloading == false)
        {
            RaycastHit laserHit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out laserHit, target))
            {
                Debug.Log(laserHit.collider.name);
                
                if (laserHit.collider.tag == "Enemy" && gunData.name != "Blaster")
                {
                    laserHit.transform.SendMessageUpwards("TakeDamage", gunData.damage);

                    gunData.ammo -= 1;
                    firingTime = gunData.fireRate;

                }

                if (gunData.name == "Blaster")
                {
                    Instantiate(explosion, laserHit.point, laserHit.transform.rotation);
                }


            }

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
