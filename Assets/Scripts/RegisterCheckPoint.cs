using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterCheckPoint : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.respawnPoint = transform.position;
        }
    }
}
