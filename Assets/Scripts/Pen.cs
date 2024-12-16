using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Pen : MonoBehaviour
{
    [Header("Pen Properties")]
    public Transform tip;
    public Material drawingMaterial;
    public Material tipMaterial;
    [Range(0.01f, 0.1f)]
    public float penWidth = 0.01f;
    public Color[] penColors;

    [Header("Input Events")]
    public InputActionReference rightTrigger, leftTrigger, AButton; // Event action reference
    public XRDirectInteractor rightHand;
    public XRDirectInteractor leftHand;
    public XRGrabInteractable grabbable;
    private LineRenderer currentDrawing;
    private List<Vector3> positions = new();
    private int index;
    private int currentColorIndex;


    private void Start()
    {
        currentColorIndex = 0; 
        tipMaterial.color = penColors[currentColorIndex];
    }


    private void Update()
    {
        bool isGrabbed = grabbable.isSelected;
        bool isRightHandDrawing = isGrabbed && grabbable.interactorsSelecting[0] == rightHand && rightTrigger.action.IsPressed();
        bool isLeftHandDrawing = isGrabbed && grabbable.interactorsSelecting[0] == leftHand && leftTrigger.action.IsPressed();
        //bool isDrawing = trigger.action.IsPressed();
        // check if button is held

        if (isRightHandDrawing || isLeftHandDrawing)
        {
            Draw();
        }
        else if (currentDrawing != null)
        {
            currentDrawing = null;
        }
        else if (AButton.action.IsPressed())
        {
            SwitchColor();
        }
    }


    private void Draw()
    {
        if (currentDrawing == null)
        {
            index = 0;
            currentDrawing = new GameObject().AddComponent<LineRenderer>();
            currentDrawing.material = drawingMaterial;
            currentDrawing.startColor = currentDrawing.endColor = penColors[currentColorIndex];
            currentDrawing.startWidth = currentDrawing.endWidth = penWidth;
            currentDrawing.SetPosition(0, tip.transform.position);
        }
        else
        {
            var currentPosition = currentDrawing.GetPosition(index);
            if (Vector3.Distance(currentPosition, tip.transform.position) > 0.01f)
            {
                index++;
                currentDrawing.positionCount = index + 1;
                currentDrawing.SetPosition(index, tip.transform.position);
            }
        }
    }


    private void SwitchColor()
    {
        if (currentColorIndex == penColors.Length - 1)
        {
            currentColorIndex = 0;
        }
        else
        {
            currentColorIndex++;
        }

        tipMaterial.color = penColors[currentColorIndex];
    } 
}
