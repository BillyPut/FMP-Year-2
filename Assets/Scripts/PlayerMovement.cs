using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController cC;
    public Transform cam;
    public Transform gunHolder;

    public float speed = 10f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundLayer;
    bool isGrounded;

    public Transform headCheck;
    public float headDistance = 0.4f;
    bool headBump;

    Vector3 velocity;
   
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
        
        //Only checks if anything is above the player's head if crouching
        if (speed < 10)
        {
            headBump = Physics.CheckSphere(headCheck.position, headDistance, groundLayer);
        }
        else
        {
            headBump = false;
        }

        
        if (isGrounded && velocity.y < 0 )
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 walk = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded == true && speed >= 10)
        {
            speed = 18f;
        }
        else if (Input.GetKey(KeyCode.LeftControl) && speed <=10)// && isGrounded == true)
        {
            speed = 4f;
            cC.height = 1.4f;

            cam.transform.position = new Vector3(transform.position.x, transform.position.y + 0.51f, transform.position.z);
            gunHolder.transform.position = new Vector3(gunHolder.transform.position.x, transform.position.y + 0.505f, gunHolder.transform.position.z);


            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    transform.GetChild(i).transform.localScale = new Vector3(1.2f, 0.6f, 1.2f);
                }
                else
                {
                    transform.GetChild(i).transform.localScale = new Vector3(1f, 0.5f, 1f);
                }
            }
                    
        }
        else
        {
            if (headBump == false)
            {
                ResetToDefault();
            }

            cC.height = 2.8f;
            cam.transform.position = new Vector3(transform.position.x, transform.position.y + 1.02f, transform.position.z);
            gunHolder.transform.position = new Vector3(gunHolder.transform.position.x, transform.position.y + 1.01f, gunHolder.transform.position.z);

            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    transform.GetChild(i).transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                }
                else
                {
                    transform.GetChild(i).transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }
        }

        cC.Move(speed * Time.deltaTime * walk);



    }

    //Sets values back to normal walking
    public void ResetToDefault()
    {
        speed = 10f;      
        transform.localScale = Vector3.one;
       
    }

    //Lets you see the range of the Physics.CheckSphere
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
        Gizmos.DrawSphere(headCheck.position, headDistance);
    }
}
