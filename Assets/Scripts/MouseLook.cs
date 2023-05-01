using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 200f;

    public Transform player;
    public Transform weapons;

    float xRotation = 0f;

    public int cursorState;

    // Start is called before the first frame update
    void Start()
    {
        cursorState = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        weapons.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);

        if (cursorState == 0)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (cursorState == 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (cursorState == 2)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        
    }
}
