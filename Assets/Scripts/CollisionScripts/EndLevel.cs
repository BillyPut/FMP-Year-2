using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public GameObject player;
    public GameObject hud, timer, whiteScreen, endText;
    public EndSequenceInitiation endSequence;

    public bool trig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            whiteScreen.SetActive(true);
            player.transform.position = transform.position;
            player.GetComponent<CharacterController>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;
            hud.SetActive(false);
            timer.SetActive(false);
            endText.SetActive(true);
            endSequence.detonateTimer = 120f;

        }
    }
}
