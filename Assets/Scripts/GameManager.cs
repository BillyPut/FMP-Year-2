using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int health;
    public TextMeshProUGUI healthText, ammoText, totalammoText;
    public WeaponSwitching weaponSwitching;
    public Vector3 respawnPoint;
    public GameObject player;
    
    public GameObject startButton;
    
    public MouseLook cam;
    public GameObject pauseMenu;
    public Slider healthBar;

    private bool dead;
    public GameObject blackScreen;
    // Start is called before the first frame update
    void Start()
    {
        startButton.GetComponent<Button>().Select();
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {      
        if (health > 100)
        {
            health = 100;
        }
        if (health <= 0)
        {
            health = 0;

            if (dead == false)
            {
                StartCoroutine(Death());
                dead = true;
            }
            
            cam.cursorState = 0;
        }
        
        if (healthText != null)
        {
            healthText.text = (health.ToString());
            ammoText.text = (weaponSwitching.ammo.ToString());
            totalammoText.text = (weaponSwitching.ammoAmount.ToString());
        }

        if (Input.GetKey(KeyCode.Escape) && pauseMenu != null)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            cam.cursorState = 0;
        }

        if (healthBar != null)
        {
            healthBar.value = health;
        }

    }

    public void RespawnPlayer()
    {
        player.transform.position = respawnPoint;
        health -= 20;
       
    }

    public void StartGame()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            SceneManager.LoadScene("OutdoorsScene");
        }
        else
        {
            Time.timeScale = 1.0f;
            cam.cursorState = 1;
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
   
    public void QuitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator Death()
    {
        blackScreen.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Menu");
    }
}
