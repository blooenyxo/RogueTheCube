﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Stats stats;
    public float gravity = -9.81f;

    //ground check
    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;

    //privates
    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * stats.MOVESPEED.GetValue() * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}