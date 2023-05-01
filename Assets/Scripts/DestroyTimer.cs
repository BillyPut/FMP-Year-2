using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float deathTime;

    void Update()
    {
        deathTime -= Time.deltaTime;
        if (deathTime <= 0)
        {
            Destroy(gameObject);
        }
    }

}
