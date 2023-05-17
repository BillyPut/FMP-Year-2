using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSequenceInitiation : MonoBehaviour
{
    public bool countdownSequence, blasterAcquired;

    private bool buttonsAppeared;
    public GameObject choiceButtons;

    public GameObject enemies;
    public WeaponSwitching weapons;
    public GameObject blasterText;

    public MouseLook cam;
    public GameObject blackScreen;
    public GameManager gameManager; 

    [HideInInspector]
    public float detonateTimer;

    public TextMeshProUGUI detonateText;

    public DoorOpenShut closingDoor;
    public GameObject door1, door2;

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
            gameManager.pauseDisable = true;
            StartCoroutine(Die());
            
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
        closingDoor.moveIt = true;
        door1.SetActive(false);
        ResetValues();
    }
    
    public void GetBlaster()
    {
        blasterAcquired = true;
        weapons.weaponAmount += 1;
        blasterText.SetActive(true);
        enemies.SetActive(true);
        closingDoor.moveIt = true;
        door2.SetActive(false);
        ResetValues();     
    }

    void ResetValues()
    {
        choiceButtons.SetActive(false);
        cam.cursorState = 1;
        Time.timeScale = 1;
    }

    private IEnumerator Die()
    {
        blackScreen.SetActive(true);

        yield return new WaitForSeconds(1f);

        cam.cursorState = 0;
        SceneManager.LoadScene("Menu");

    }
}
