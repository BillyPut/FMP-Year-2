using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public string[] guns;

    public float reloadTime;
    public float fireRate;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        guns[0] = "pistol";
        guns[1] = "ar";
        guns[2] = "smg";
        guns[3] = "shotgun";
        guns[4] = "blaster";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {

        }
    }

    public void pistolOut()
    {
        reloadTime = 1.0f;
        fireRate = 1.0f;
        damage = 1.0f;
    }
    public void arOut()
    {
        reloadTime = 2.0f;
        fireRate = 2.0f;
        damage = 2.0f;
    }
    public void smgOut()
    {
        reloadTime = 5.0f;
        fireRate = 5.0f;
        damage = 5.0f;
    }
    public void shotgunOut()
    {
        reloadTime = 10.0f;
        fireRate = 10.0f;
        damage = 10.0f;
    }
    public void blasterOut()
    {
        reloadTime = 05f;
        fireRate = 0.5f;
        damage = 0.5f;
    }


}
