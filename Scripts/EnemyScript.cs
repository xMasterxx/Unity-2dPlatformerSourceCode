using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public LayerMask playerLayer;
    public GameObject enemyMoveFirstPoint;
    public GameObject enemyMoveSecondPoint;
    Vector2 firstPoint;
    Vector2 secondPoint;
    public float rayDistance = 2.0f; /*HammerKnight = 2; BlackKnight = 3; Rogue = 1; */
    public float enemySpeed = 1; /*HammerKnight = 1; BlackKnight = 1,5; Rogue = 8 */
    CapsuleCollider2D capsuleCollider2D;
    Animator enemyAnimator;
    AudioSource[] audios;
    bool canPlay = true;

    void Start()
    {
        AudioInit();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        enemyAnimator = GetComponentInChildren<Animator>();
        firstPoint = enemyMoveFirstPoint.GetComponent<Transform>().position;
        secondPoint = enemyMoveSecondPoint.GetComponent<Transform>().position;
    }


    void Update()
    {
        EnemyController();
    }


    void EnemyController()
    {
        transform.Translate(Vector2.left * enemySpeed * Time.deltaTime);
        if (gameObject.transform.position.x < firstPoint.x)
        {
            transform.position = new Vector2(firstPoint.x, transform.position.y);
            Flip();
            enemySpeed *= -1;
        }
        else if (gameObject.transform.position.x > secondPoint.x)
        {
            transform.position = new Vector2(secondPoint.x, transform.position.y);
            Flip();
            enemySpeed *= -1;
        }

        if (IsAttacking((int)enemySpeed))
        {
            if (canPlay)
            {
                StartCoroutine(TimerAudio());
                canPlay = false;
            }

            enemyAnimator.SetBool("Attack", true);

        }
        else
        {

            enemyAnimator.SetBool("Attack", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !IsAttacking((int)enemySpeed))
        {
            Flip();
            enemySpeed *= -1;
        }
    }
    bool IsAttacking(int directionX)
    {
        Vector2 origin = capsuleCollider2D.bounds.center;
        Vector2 direction = Vector2.left * directionX;
        float distance = capsuleCollider2D.bounds.extents.x + rayDistance;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, playerLayer);
        Color rayColor;
        if (hit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(origin, direction * (distance), rayColor);

        return hit.collider != null;
    }

    void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }


    IEnumerator TimerAudio()
    {
        audios[Random.Range(0, audios.Length)].Play();
        yield return new WaitWhile(() => audios[Random.Range(0, audios.Length)].isPlaying);
        canPlay = true;
    }

    void AudioInit()
    {
        audios = GetComponents<AudioSource>();
    }
}
