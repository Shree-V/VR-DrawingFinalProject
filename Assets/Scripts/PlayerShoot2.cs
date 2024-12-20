using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class PlayerShoot2 : MonoBehaviour
{
    public GameObject BulletTemplate;
    public float shootPower = 100f;

    public InputActionReference trigger;
    public AudioClip shootSoundSFX;
    // Start is called before the first frame update
    void Start()
    {
        trigger.action.performed += Shoot;
    }

    void Shoot(InputAction.CallbackContext __)
    {
        GameObject newBullet = Instantiate(BulletTemplate, transform.position, transform.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);
        GetComponent<AudioSource>().PlayOneShot(shootSoundSFX);
    }
}
