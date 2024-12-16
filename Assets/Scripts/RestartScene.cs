using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RestartScene : MonoBehaviour
{

    public InputActionReference innerLefttrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //bool isTriggered = innerLefttrigger.action.IsPressed;

        if (innerLefttrigger.action.IsPressed()) 
        {
            RestartingScene();
        }
    }

    public void RestartingScene () 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
