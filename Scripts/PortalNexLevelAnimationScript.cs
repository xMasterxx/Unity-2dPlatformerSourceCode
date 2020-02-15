using UnityEngine;

public class PortalNexLevelAnimationScript : MonoBehaviour
{
    new ParticleSystem particleSystem;
    AudioSource audioSource;
    private void Awake()
    {
       
        particleSystem = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            particleSystem.Play();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Stop();
            particleSystem.Stop();
        }
    }

}
