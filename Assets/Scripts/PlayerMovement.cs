using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Handle physics
    public Transform groundCheck;
    public float groundDistance = 0.4f; // Radius of sphere to check for ground
    public LayerMask groundMask; // Only check for ground

    Vector3 velocity;
    bool isGrounded; // Is player on ground?


    // Update is called once per frame
    void Update()
    {
        // Handle physics
        handlePhysics();

        // Handle jump
        handleJump();

        // Handle movement
        handleMovement();
    }

    void handlePhysics()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Check if player is on ground

        if (isGrounded && velocity.y <= 0) // Prevents gravity from accumulating
        {
            velocity.y = 0f;
        } 
        else 
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void handleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) // Spacebar
        {
            velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity); // Calculates velocity to jump
        }
    }

    void handleMovement() 
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // A, D, Left, Right
        float vertical = Input.GetAxisRaw("Vertical"); // W, S, Up, Down
        
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // Normalized to prevent faster diagonal movement

        if (direction.magnitude >= 0.1f) // Prevents movement when not pressing any keys
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; // Calculates angle to move towards
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); // Smooths angle to move towards
            transform.rotation = Quaternion.Euler(0f, angle, 0f); // Rotates towards angle

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // Calculates direction to move towards
            controller.Move(moveDirection.normalized * speed * Time.deltaTime); // Moves towards direction
        }
    }
}
