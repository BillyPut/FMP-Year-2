using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitching : MonoBehaviour
{
    public int pressedNumber;
    public int selectedWeapon;
    public float switchTime;
    public int ammo, ammoAmount;
    public int weaponAmount;
    public Transform[] guns;

    public GameObject gunTextHolder;
    public float appearTimer;

    // Start is called before the first frame update
    void Start()
    {
        guns = new Transform[transform.childCount];
        selectedWeapon = 1;
        pressedNumber = 1;
        switchTime = 0.2f;
        
        for (int i = 0; i < transform.childCount; i++) 
        {
            guns[i] = transform.GetChild(i);
           
        }
        
        weaponAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (switchTime <= 0)
        {
            GetPressedKey();
        }
   
        switchTime -= Time.deltaTime;   
        appearTimer -= Time.deltaTime;  

        if (pressedNumber >= 0 && pressedNumber != selectedWeapon)
        {
            guns[selectedWeapon - 1].gameObject.SetActive(false);
            guns[pressedNumber - 1].gameObject.SetActive(true);
            selectedWeapon = pressedNumber;
            switchTime = 0.2f;
            appearTimer = 2.8f;
        }

        ammo = transform.GetChild(selectedWeapon - 1).GetComponent<WeaponSystem>().gunData.ammo;
        ammoAmount = transform.GetChild(selectedWeapon - 1).GetComponent<WeaponSystem>().gunData.overallAmmo;       

        if (appearTimer > 0)
        {
            gunTextHolder.SetActive(true);
            Button button = gunTextHolder.transform.GetChild(selectedWeapon - 1).GetComponent<Button>();     
            if (Time.timeScale != 0)
            {
                button.Select();
            }
        }
        else
        {
            gunTextHolder.SetActive(false);
        }
    }

    public void GetPressedKey()
    {
        for (int num = 1; num <= 5; num++)
        {
            if (weaponAmount >= num)
            {
                if (Input.GetKeyDown(num.ToString()))
                {
                    pressedNumber = num;
                }
            }
                    
        }

    }

    public void UseAmmoPickup(int ammoAmmount, string ammoType)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            WeaponSystem gun = transform.GetChild(i).GetComponent<WeaponSystem>();
            Debug.Log(gun.gunData.name);

            if (ammoType == gun.gunData.name)
            {
                gun.gunData.overallAmmo += ammoAmmount;
            }
        }
    }
}
