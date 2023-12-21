using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{

    // Handle physics
    public float verticalVel;
    private Vector3 moveVector;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f; // Radius of sphere to check for ground
    public LayerMask groundMask; // Only check for ground

    bool isGrounded; // Is player on ground?


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         // Handle physics
        handlePhysics();
    }

       void handlePhysics()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Check if player is on ground

        if (isGrounded && verticalVel <= 0) // Prevents gravity from accumulating
        {
            verticalVel = 0f;
        } 
        else 
        {
            verticalVel += gravity * Time.deltaTime * Time.deltaTime;

            // moveVector = new Vector3(0, verticalVel, 0);
            // transform.Translate(moveVector);

            transform.position += new Vector3(0, verticalVel, 0);
        }
    }
}
