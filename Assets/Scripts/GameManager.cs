using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int health;
    public TextMeshProUGUI healthText, ammoText, totalammoText;
    public WeaponSwitching weaponSwitching;
    public Vector3 respawnPoint;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {      
        if (health > 100)
        {
            health = 100;
        }



        healthText.text = (health.ToString());
        ammoText.text = (weaponSwitching.ammo.ToString());
        totalammoText.text = (weaponSwitching.ammoAmount.ToString());
    }

    public void RespawnPlayer()
    {
        player.transform.position = respawnPoint;
        health -= 20;
       
    }


}
