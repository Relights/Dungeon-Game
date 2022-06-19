using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    public LayerMask groundMask;

    public CharacterController controller;

    //store player's movement speed
    string speedKey = "SpeedKey";
    public float speed;

    public float currentSpeed;

    public Transform GroundCheck;
    public float groundDistance = 0.4f;

    public float jumpHeight;

    Vector3 velocity;
    bool isGrounded;


    private float gravity = -9.81f;

    void Start()
    {
        speed = PlayerPrefs.GetFloat(speedKey, 2);
        currentSpeed = speed;
    }

    void Update()
    {
        PlayerPrefs.SetFloat(speedKey, speed);
        //Check if player is on the ground.
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //Player controls
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * currentSpeed * Time.deltaTime);

        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        //Gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = speed;
            currentSpeed*=2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = speed;
        }

    }
}
