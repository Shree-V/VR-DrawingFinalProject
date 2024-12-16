using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PenWidth : MonoBehaviour
{
    public GameObject sphere; // Assign your sphere GameObject in the Inspector, would suggest putting the sphere under a parent, then put the parent here
    public float minValue = 1f;
    public float maxValue = 10f;
    public float adjustmentSpeed = 5f;

    private float currentValue = 1f;
    private float thumbstickIdleTimer = 0f;
    private bool isThumbstickMoving = false;

    private void Start()
    {
        sphere.SetActive(false); // Start with the sphere hidden
    }
    void Update()
    {
        if (InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 thumbstickInput))
        {
            float horizontalInput = thumbstickInput.x;

            // Check if thumbstick is being moved
            isThumbstickMoving = Mathf.Abs(horizontalInput) > 0.01f;

            if (isThumbstickMoving)
            {
                // Reset idle timer
                thumbstickIdleTimer = 0f;

                // Adjust the current value based on input
                currentValue += horizontalInput * adjustmentSpeed * Time.deltaTime;

                // Clamp the value to stay within the min and max range
                currentValue = Mathf.Clamp(currentValue, minValue, maxValue);

                Pen2 script = FindObjectOfType<Pen2>();
                script.penWidth = currentValue / 100;

                //sphere.transform.localScale = Vector3.one * script.penWidth;

                sphere.transform.localScale = Vector3.one * currentValue;
                sphere.SetActive(true); // Ensure the sphere is visible while moving

            }
            else
            {
                thumbstickIdleTimer += Time.deltaTime;

                // Hide the sphere after 1 second of idle
                if (thumbstickIdleTimer >= 1f)
                {
                    sphere.SetActive(false);
                }
            }
        }
    }
}

