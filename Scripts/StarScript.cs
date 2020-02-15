using System.Collections;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    public float time = 0.8f;
    new ParticleSystem particleSystem;
    AudioSource audioSource;
    bool played;

    private void Awake()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !played)
        {
            audioSource.Play();
            particleSystem.Play();
            played = true;
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);
        particleSystem.Stop();
        played = false;
        gameObject.SetActive(false);
    }

}
