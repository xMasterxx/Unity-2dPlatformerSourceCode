using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayer;
    public List<Image> hearts;
    public GameObject player;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public int score;
    public bool gameOver;
    public bool gameFinish;
    public int maxLivesNumber = 3;
    public float playerSpeed;
    public float playerMaxSpeed = 5f;
    public float boundXminus = -11;
    public float boundX = 58;
    public float boundY = 20;
    [SerializeField] int livesNumber;
    [SerializeField] float jumpForce;
    [SerializeField] float moveX;
    public int coinsCollected;

    Vector3 checkPointPosition;
    Vector3 firstcheckPointPosition;
    GameObject playerShadow;
    GameObject skinEquipped;
    AudioSource painAudioSource;
    Animator playerAnimator;
    Rigidbody2D playerRb;
    CapsuleCollider2D capsuleCollider2D;
    ParticleSystem blood;

    bool isMoving;
    bool stunned;
    bool lookLeft = true;
    readonly float pushForce = 10;




    void Awake()
    {
        skinEquipped = Resources.Load<GameObject>($"PlayersSkins/{SaveGame.Load<string>("ActiveSkin")}");
        firstcheckPointPosition = GameObject.Find("PlayerStartPosition").GetComponent<Transform>().position;
        painAudioSource = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        Instantiate(skinEquipped, player.transform);
        PlayerStatsInitialization();
        blood = GetComponentInChildren<ParticleSystem>();
        playerAnimator = GetComponentInChildren<Animator>();
        playerShadow = gameObject.transform.GetChild(0).GetChild(0).gameObject;
    }


    void FixedUpdate()
    {
        PlayerStatusController();
        MoveController();
        HealthController();
    }

    void MoveController()
    {
        if (!stunned)
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                Jump();
            }
            playerRb.velocity = new Vector2(moveX, playerRb.velocity.y);
            moveX = CrossPlatformInputManager.GetAxis("Horizontal") * (playerSpeed * Time.fixedDeltaTime * 100f);
            isMoving = moveX > 0.7 || moveX < -0.7;


            if (moveX > 0 && lookLeft || moveX < 0 && !lookLeft)
            {
                FlipPlayer();
            }
        }
    }

    void HealthController()
    {
        if (livesNumber > maxLivesNumber)
        {
            livesNumber = maxLivesNumber;
        }
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].sprite = i < livesNumber ? fullHeart : emptyHeart;

            hearts[i].enabled = i < maxLivesNumber;

        }

        if (livesNumber <= 0)
        {
            gameOver = true;
            playerRb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }

    bool IsGrounded()
    {
        Vector2 origin = capsuleCollider2D.bounds.center;
        Vector2 direction = Vector2.down;
        float distance = capsuleCollider2D.bounds.extents.y + 0.2f;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, groundLayer);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Star"))
        {
            score += 35;
            collision.gameObject.SetActive(false);

        }

        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            checkPointPosition = collision.gameObject.transform.position;
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            coinsCollected++;
        }
    }
    public void CheckPointLoad()
    {
        livesNumber = maxLivesNumber;
        gameOver = false;
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        gameObject.transform.position = checkPointPosition;
        gameObject.transform.rotation = Quaternion.identity;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            livesNumber -= 99999999;
        }

        if (collision.gameObject.CompareTag("Trap"))
        {
            if (livesNumber > 0)
            {
                painAudioSource.Play();
            }
            playerRb.AddForce(collision.contacts[0].normal * playerRb.mass * pushForce, ForceMode2D.Impulse);
            blood.Play();
            stunned = true;
            livesNumber--;
            StartCoroutine(StunTimer(0.7f));

        }

        if (collision.gameObject.CompareTag("Weapon"))
        {
            if (livesNumber > 0)
            {
                painAudioSource.Play();
            }
            blood.Play();
            playerRb.AddForce(collision.contacts[0].normal * playerRb.mass * pushForce, ForceMode2D.Impulse);
            stunned = true;
            livesNumber--;
            StartCoroutine(StunTimer(0.5f));
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            gameFinish = true;
            playerRb.constraints = RigidbodyConstraints2D.FreezePosition;
        }

        if (collision.gameObject.CompareTag("Ground") && !IsGrounded() && isMoving)
        {
            playerRb.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
            stunned = true;
            StartCoroutine(StunTimer(0.3f));
        }
    }

    IEnumerator StunTimer(float number)
    {
        yield return new WaitForSeconds(number);
        stunned = false;
        playerSpeed = playerMaxSpeed;
    }
    
    void PlayerStatusController()
    {
        if (isMoving && IsGrounded())
        {
            playerAnimator.SetInteger("State", 1);
        }
        else if (IsGrounded())
        {
            playerAnimator.SetInteger("State", 0);
        }
        else
        {
            playerAnimator.SetInteger("State", 2);
        }

        if (IsGrounded())
        {
            playerShadow.SetActive(true);
        }
        else
        {
            playerShadow.SetActive(false);
        }

        if (stunned)
        {
            moveX = 0;
            playerSpeed = 0;
            isMoving = false;
        }

        if (transform.position.x < boundXminus)
        {
            transform.position = new Vector2(boundXminus, transform.position.y);
        }
        else if (transform.position.y > boundY)
        {
            transform.position = new Vector2(transform.position.x, boundY);
        }
        else if (transform.position.x > boundX)
        {
            transform.position = new Vector2(boundX, transform.position.y);
        }

    }

    void PlayerStatsInitialization()
    {
        var playerStats = SaveGame.Load<Dictionary<string, float>>("playerStats");
        playerMaxSpeed = playerStats["maxspeed"];
        maxLivesNumber = (int)playerStats["maxlives"];
        playerSpeed = playerMaxSpeed;
        livesNumber = maxLivesNumber;
        checkPointPosition = firstcheckPointPosition;
        jumpForce = 90f;
        playerRb.mass = 5;
        playerRb.gravityScale = 3;

    }

    public void Jump()
    {
        if (IsGrounded() && !stunned)
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FlipPlayer()
    {
        lookLeft = !lookLeft;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}




