using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInputs controls;

    public CharacterController controller;
    public Transform cam;

    //physical qualities
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    //defines ground interactions
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    //determines turning
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    
    void Awake()
    {
        controls = new PlayerInputs();
        //checks for jumping if the character is grounded
        controls.Player.Jump.started += ctx =>
        {
            Debug.Log("jump button pressed");
            if(isGrounded == true)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        };
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        //checks every frame whether the character is touching the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //prevents character from falling if grounded
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        //using the new input system: checks the input for movement controls
        Vector2 movementVector = controls.Player.Move.ReadValue<Vector2>();
        float horizontal = movementVector.x;
        float vertical = movementVector.y;
        //normalizes the input directions for an angle
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //read movement input and factors the character movement based on the camera position
        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir =Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //applies the horizontal movement
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        //apply gravity
        velocity.y += gravity * Time.deltaTime;
        //uses the velocity to make the jump
        controller.Move(velocity * Time.deltaTime);
    }
}
