using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public GameObject trapChain;
    public GameObject saw;
    new public ParticleSystem particleSystem;
    AudioSource audioSource;
    bool active;
    [SerializeField] float sawRotationSpeed = 5;
    public float firstBoundX = 13f;
    public float secondboundX = 20f;

    private void Awake()
    {
        active = gameObject.activeSelf;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        saw.transform.Rotate(Vector3.forward * sawRotationSpeed);

        if (saw.transform.position.x < firstBoundX)
        {
            saw.transform.position = new Vector2(firstBoundX, saw.transform.position.y);
        }
        else if (saw.transform.position.x > secondboundX)
        {
            saw.transform.position = new Vector2(secondboundX, saw.transform.position.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            trapChain.SetActive(true);
            if (active)
            {
                audioSource.Play();
                particleSystem.Play();
                active = false;

            }
        }
    }
}
