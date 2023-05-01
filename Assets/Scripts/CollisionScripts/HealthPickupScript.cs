using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupScript : MonoBehaviour
{
    public int healthAmount;
    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, Time.deltaTime * 100f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.health += healthAmount;
            Destroy(gameObject);
        }
    }
}
