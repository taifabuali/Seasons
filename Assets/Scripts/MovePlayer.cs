using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    private Vector3 velocity;
    public CharacterController controller;

    private void Start()
    {
        // Get the CharacterController component
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Ground check using a sphere at the groundCheck position
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // Ensure the player stays grounded
        }

        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction relative to the player's orientation
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Apply movement to the CharacterController
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Formula to calculate the upward velocity needed to achieve a jump of a specific height
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed *= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed /= 2;
        }


        // Apply gravity over time
        velocity.y += gravity * Time.deltaTime;

        // Apply gravity to the CharacterController
        controller.Move(velocity * Time.deltaTime);
    }
}