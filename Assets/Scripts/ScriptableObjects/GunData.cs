using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Gun", menuName ="Weapon/Gun")]
public class GunData : ScriptableObject
{
    public new string name;

    [Header("Shooting")]
    public float damage;
    public float fireRate;

    [Header("Reloading")]
    public int ammo;
    public int magsize;
    public int overallAmmo;
    public float reloadTime;
    public bool reloading;
    

}
