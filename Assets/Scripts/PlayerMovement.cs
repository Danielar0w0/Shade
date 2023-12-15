using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Animator anim;
    public CharacterController controller;
	public Camera cam;

	private Vector3 desiredMoveDirection;
    private Vector3 desiredRotationDirection;
	private float desiredRotationSpeed = 0.1f;

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0,1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

	public float Speed;
    public float Velocity;
    public float allowPlayerRotation = 0.1f;

    // Handle physics
    public float verticalVel;
    private Vector3 moveVector;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Handle physics
    public Transform groundCheck;
    public float groundDistance = 0.4f; // Radius of sphere to check for ground
    public LayerMask groundMask; // Only check for ground

    private float InputX;
	private float InputZ;

    bool isGrounded; // Is player on ground?

    // Start is called before the first frame update
    void Start() {
        // Obtain Animator and CharacterController
        anim = GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
        // Obtain Camera
        cam = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {

        InputMagnitude();

        // Faulty jump animation
		InputJump();

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
            verticalVel += gravity * Time.deltaTime;

            moveVector = new Vector3(0, verticalVel * 1.2f * Time.deltaTime, 0);
            controller.Move(moveVector);
        }
    }

    void PlayerMoveAndRotation() {

		// InputX = Input.GetAxisRaw("Horizontal");
		// InputZ = Input.GetAxisRaw("Vertical");

		// var forward = cam.transform.forward;
		// var right = cam.transform.right;

		// forward.y = 0f;
		// right.y = 0f;

		// forward.Normalize ();
		// right.Normalize ();

		// desiredMoveDirection = forward * InputZ + right * InputX;

        // if (desiredMoveDirection != Vector3.zero)
        //     transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (desiredMoveDirection), desiredRotationSpeed);
        
        // controller.Move(desiredMoveDirection * Time.deltaTime * Velocity);

        InputX = Input.GetAxisRaw("Horizontal"); // A, D, Left, Right
        InputZ = Input.GetAxisRaw("Vertical"); // W, S, Up, Down
        
        Vector3 direction = new Vector3(InputX, 0f, InputZ).normalized; // Normalized to prevent faster diagonal movement

        if (direction.magnitude >= 0.1f) // Prevents movement when not pressing any keys
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y; // Calculates angle to move towards
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); // Smooths angle to move towards
            transform.rotation = Quaternion.Euler(0f, angle, 0f); // Rotates towards angle

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // Calculates direction to move towards
            controller.Move(moveDirection.normalized * Velocity * Time.deltaTime); // Moves towards direction
        }
	}

    public void LookAt(Vector3 pos)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), desiredRotationSpeed);
    }

    public void RotateToCamera(Transform t)
    {

        var forward = cam.transform.forward;

        desiredMoveDirection = forward;
        t.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }

	void InputMagnitude() {
		//Calculate Input Vectors
		InputX = Input.GetAxisRaw("Horizontal");
		InputZ = Input.GetAxisRaw("Vertical");

		//anim.SetFloat ("InputZ", InputZ, VerticalAnimTime, Time.deltaTime * 2f);
		//anim.SetFloat ("InputX", InputX, HorizontalAnimSmoothTime, Time.deltaTime * 2f);

		//Calculate the Input Magnitude
		Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        //Physically move player

		if (Speed > allowPlayerRotation) {
			anim.SetFloat ("Blend", Speed, StartAnimTime, Time.deltaTime);
			PlayerMoveAndRotation ();
		} else if (Speed < allowPlayerRotation) {
			anim.SetFloat ("Blend", Speed, StopAnimTime, Time.deltaTime);
		}
	}

	void InputJump() {
		if (Input.GetButtonDown("Jump") && isGrounded) {
			// anim.SetTrigger ("jump");
            verticalVel = Mathf.Sqrt(2f * jumpHeight * -gravity); // Calculates velocity to jump
		}
	}
}
