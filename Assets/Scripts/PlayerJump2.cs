using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump2 : MonoBehaviour
{
    [SerializeField] private InputActionProperty jumpStick;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private CharacterController capsule;
    [SerializeField] private LayerMask groundLayers;

    private float gravity = Physics.gravity.y;
    private Vector3 movement;
    private void Update()
    {
        bool _isGrounded = IsGrounded();

        if (jumpStick.action.WasPressedThisFrame() && _isGrounded)
        {
            Jump2();
        }

        movement.y += gravity * Time.deltaTime;

        capsule.Move(movement * Time.deltaTime);

    }

    private void Jump2()
    {
        movement.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.2f, groundLayers);
    }



}

