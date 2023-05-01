using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int pressedNumber;
    public int selectedWeapon;
    public float switchTime;
    public int ammo, ammoAmount;
    public int weaponAmount;
    [SerializeField] public Transform[] guns;

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

        if (pressedNumber >= 0 && pressedNumber != selectedWeapon)
        {
            guns[selectedWeapon - 1].gameObject.SetActive(false);
            guns[pressedNumber - 1].gameObject.SetActive(true);
            selectedWeapon = pressedNumber;
            switchTime = 0.2f;
        }

        ammo = transform.GetChild(selectedWeapon - 1).GetComponent<WeaponSystem>().gunData.ammo;
        ammoAmount = transform.GetChild(selectedWeapon - 1).GetComponent<WeaponSystem>().gunData.overallAmmo;       
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
}
