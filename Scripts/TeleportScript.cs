using System.Collections;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public Transform transformFirstPoint;
    public Transform transformSecondPoint;
    FirstPointActivator pointActivator;
    Vector2 firstPoint;
    Vector2 secondPoint;
    bool isActive = true;
    bool toSecondPoint;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        firstPoint = transformFirstPoint.position;
        secondPoint = transformSecondPoint.position;
        pointActivator = GetComponentInChildren<FirstPointActivator>();
    }

    void Update()
    {
        toSecondPoint = pointActivator.toSecond;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isActive)
        {
            if (toSecondPoint)
            {
                audioSource.Play();
                collision.gameObject.transform.position = secondPoint;
               
            }
            else
            {
                audioSource.Play();
                collision.gameObject.transform.position = firstPoint;
                
            }

            isActive = false;
            StartCoroutine(CooldownTimer(2f));
        }
    }

    IEnumerator CooldownTimer(float time)
    {
        yield return new WaitForSeconds(time);
        isActive = true;
    }
}
