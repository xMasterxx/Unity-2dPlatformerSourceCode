using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceTrap : MonoBehaviour
{
    public Rigidbody2D maceRigidbody2D;
    public AudioSource audioSource;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            maceRigidbody2D.gravityScale = 1;
            gameObject.SetActive(false);
        }
    }
}
