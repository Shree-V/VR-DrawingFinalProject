using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RadialSelection : MonoBehaviour
{
    public InputActionReference BButton;
    public XRDirectInteractor rightHand;
    public XRDirectInteractor leftHand;

    [Range(2,10)]
    public int numberOfRadialPart;
    public GameObject radialPartPrefab;
    public Transform radialPartCanvas;
    public float angleBetweenPart = 10;
    public Transform handTransform;

    public UnityEvent<Color> OnPartSelected;

    private List<GameObject> spawnedParts = new List<GameObject>();
    public int currentSelectedRadialPart = -1;


    public List<Color> colorList;
    public Pen2 colors;
    private bool wasRadialButtonPressed = false;
    public GameObject PenTip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BButton.action.IsPressed() && !wasRadialButtonPressed)
        {
            SpawnRadialPart();
        }
        wasRadialButtonPressed = BButton.action.IsPressed();

        if (BButton.action.IsInProgress())
        {
            GetSelectedRadialPart();
        }

        if (BButton.action.WasReleasedThisFrame())
        {
            HideAndTriggerSelected();
        }

        PenTip.GetComponent<Renderer>().material.color = colorList[currentSelectedRadialPart];
        

        //SpawnRadialPart();
        //GetSelectedRadialPart();
    }

    public void HideAndTriggerSelected()
    {
        //OnPartSelected.Invoke(colorList[currentSelectedRadialPart]);
        radialPartCanvas.gameObject.SetActive(false);
    }

    public void GetSelectedRadialPart() 
    {
        Vector3 centerToHand = handTransform.position - radialPartCanvas.position;
        Vector3 centerToHandProjected = Vector3.ProjectOnPlane(centerToHand, radialPartCanvas.forward);

        float angle = Vector3.SignedAngle(radialPartCanvas.up, centerToHandProjected, radialPartCanvas.forward);

        if (angle < 0)
            angle += 360;
        angle += 360/numberOfRadialPart;
        angle %= 360;


        currentSelectedRadialPart = (int) angle * numberOfRadialPart / 360;

        for (int i = 0; i < spawnedParts.Count; i++)
        {
            if(i == currentSelectedRadialPart)
            {
                spawnedParts[i].GetComponent<Image>().color = colorList[currentSelectedRadialPart];
                spawnedParts[i].transform.localScale = 1.1f * Vector3.one;
            }
            else
            {
                spawnedParts[i].GetComponent<Image>().color = Color.white;
                spawnedParts[i].transform.localScale = Vector3.one;
            }
        }
        //PenTip.GetComponent<Renderer>().material.color = colorList[currentSelectedRadialPart];
    }

    public void SpawnRadialPart()
    {
        radialPartCanvas.gameObject.SetActive(true);
        radialPartCanvas.position = handTransform.position;
        radialPartCanvas.rotation = handTransform.rotation;


        foreach (var item in spawnedParts) 
        {
            Destroy(item);
        }

        spawnedParts.Clear();

        for (int i = 0; i < numberOfRadialPart; i++)
        {
            float angle = i * 360 / numberOfRadialPart - angleBetweenPart / 2;
            Vector3 radialPartEulerAngle = new Vector3(0, 0, angle);

            GameObject spawnedRadialPart = Instantiate(radialPartPrefab, radialPartCanvas);
            spawnedRadialPart.transform.position = radialPartCanvas.position;
            spawnedRadialPart.transform.localEulerAngles = radialPartEulerAngle;

            spawnedRadialPart.GetComponent<Image>().fillAmount = (1 / (float)numberOfRadialPart) - (angleBetweenPart/360);

            spawnedParts.Add(spawnedRadialPart);

        }
    }
}
