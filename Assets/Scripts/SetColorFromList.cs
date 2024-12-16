using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorFromList : MonoBehaviour
{
    public List<Color> colorList;

    public void SetColor(Color c)
    {
        GetComponent<Renderer>().material.color = c;
    }

}
