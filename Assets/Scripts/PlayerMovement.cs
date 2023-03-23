using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController cC;

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
            transform.localScale = new Vector3(1, 0.5F, 1);
            
        }
        else
        {
            if (headBump == false)
            {
                ResetToDefault();
            }
        }

      

        cC.Move(walk * speed * Time.deltaTime);

       
       

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
