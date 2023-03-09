using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float powerUpDown = -2f;
    public AudioClip powerUpSound;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            audioSource.PlayOneShot(powerUpSound);
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.up * powerUpDown * Time.deltaTime);
    }
}
