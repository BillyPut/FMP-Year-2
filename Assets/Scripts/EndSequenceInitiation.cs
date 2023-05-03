using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndSequenceInitiation : MonoBehaviour
{
    public bool countdownSequence, blasterAcquired;

    private bool buttonsAppeared;
    public GameObject choiceButtons;
    public GameObject enemies;

    public WeaponSwitching weapons;
    public MouseLook cam;

    private float detonateTimer;
    public TextMeshProUGUI detonateText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        detonateText.text = (detonateTimer.ToString());

        if (detonateTimer <= 0 && countdownSequence == true)
        {
            detonateTimer = 0;
        }
        else
        {
            detonateTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (buttonsAppeared == false)
            {
                choiceButtons.SetActive(true);
                buttonsAppeared = true;
                cam.cursorState = 2;
                Time.timeScale = 0;
            }
         
        }
    }

    public void DestroyFacility()
    {
        countdownSequence = true;
        detonateTimer = 60.0f;
        ResetValues();
    }
    
    public void GetBlaster()
    {
        blasterAcquired = true;
        weapons.weaponAmount += 1;
        enemies.SetActive(true);
        ResetValues();     
    }

    void ResetValues()
    {
        choiceButtons.SetActive(false);
        cam.cursorState = 1;
        Time.timeScale = 1;
    }
}
