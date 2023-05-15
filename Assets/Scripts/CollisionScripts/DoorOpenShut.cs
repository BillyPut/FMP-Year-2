using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenShut : MonoBehaviour
{
    public float yPos;
    [HideInInspector]
    public bool moveIt;

    void Update()
    {
        if (moveIt == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, yPos, transform.position.z), 0.15f);
        }
        
    }  
}
