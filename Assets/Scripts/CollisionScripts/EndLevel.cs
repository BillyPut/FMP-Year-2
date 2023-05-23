using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndLevel : MonoBehaviour
{
    public GameObject player;
    public GameObject hud, timer, whiteScreen, endText;
    public EndSequenceInitiation endSequence;
    public GameManager gameManager;
    public MouseLook cam;

    [SerializeField][TextArea] private string[] endingText;
    [SerializeField] private TextMeshProUGUI endMessage;
    private int currentDisplayText = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.pauseDisable = true;
            whiteScreen.SetActive(true);
            player.transform.position = transform.position;
            player.GetComponent<CharacterController>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;
            hud.SetActive(false);
            timer.SetActive(false);
            endText.SetActive(true);
            endSequence.detonateTimer = 120f;
            StartCoroutine(EndGame());

        }
    }

    private IEnumerator EndGame()
    {
        for (int i = 0; i < endingText[currentDisplayText].Length + 1; i++)
        {
            endMessage.text = endingText[currentDisplayText].Substring(0, i);
            yield return new WaitForSeconds(0.04f);
        }

        yield return new WaitForSeconds(5.0f);

        cam.cursorState = 0;
        SceneManager.LoadScene("Menu");
    }

    
}
