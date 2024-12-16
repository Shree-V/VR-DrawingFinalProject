using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{

    [SerializeField] private InputActionProperty jumpTrigger;
    [SerializeField] private InputActionProperty jumpTrigger2;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private CharacterController capsule;
    [SerializeField] private LayerMask groundLayers;

    private float gravity = Physics.gravity.y;
    private Vector3 movement;
    private void Update()
    {
        bool _isGrounded = IsGrounded();

        if (jumpTrigger.action.WasPressedThisFrame() && jumpTrigger2.action.WasPressedThisFrame() && _isGrounded)
        {
            Jump();
        }

        movement.y += gravity * Time.deltaTime;

        capsule.Move(movement * Time.deltaTime);

    }

    private void Jump()
    {
        movement.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.2f, groundLayers);
    }



}
