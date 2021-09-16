using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    public float moveSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    private Vector3 velocity;

    [Header("Ground Check Variables")]
    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;

    private bool isGrounded = false;

    [Header("Local References")]
    public CharacterController character;

    private void Update()
    {
        GroundCheck();

        Movement();

        Jump();
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        character.Move(move * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        character.Move(velocity * Time.deltaTime);
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
}
