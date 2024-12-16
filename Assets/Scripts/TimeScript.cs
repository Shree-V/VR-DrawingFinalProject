using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    public GameObject rightHandReference;
    private Vector3 previousRightHandPosition;
    // Start is called before the first frame update
    void Start()
    {
        previousRightHandPosition = rightHandReference.transform.position;
    }
    

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(previousRightHandPosition, rightHandReference.transform.position);

        Time.timeScale = Mathf.Clamp((1500 * distance), 0.5f, 2.0f);

        previousRightHandPosition = rightHandReference.transform.position;
    }
}
