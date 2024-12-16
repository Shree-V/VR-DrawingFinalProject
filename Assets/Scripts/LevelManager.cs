using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int totalTargets;
    private int destroyedTargets = 0;

    // Update is called once per frame
    void Update()
    {
       if (destroyedTargets >= totalTargets)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } 
    }

    public void TargetDestroyed()
    {
        destroyedTargets++;
    }
}
