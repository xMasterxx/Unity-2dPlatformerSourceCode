using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FinishMenuScript : MonoBehaviour
{
    public GameObject starOne;
    public GameObject starTwo;
    public GameObject starThree;
    public GameObject stageStatus;
    public GameObject haloYellow;
    public GameObject haloTwo;
    public Button restartButton;
    public Button nextStageButton;
    public Text revardValue;
    new ParticleSystem particleSystem;
    GameManager gameManagerScript;
    [SerializeField] float time = 3.7f;
    [SerializeField] bool invoke;
    [SerializeField] float minimum = 0.6F;
    [SerializeField] float maximum = 1.0F;
    public bool mult;
    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        gameManagerScript = GameObject.FindWithTag("MainCamera").GetComponent<GameManager>();
        invoke = gameObject.activeSelf;
    }

    void Update()
    {
        if (invoke)
        {
            StarsStatus();
            ScaleAnimation();
        }
        StartCoroutine(ScaleAnimationTimer(time));
        Reward();
    }

    void Reward()
    {
        revardValue.text = gameManagerScript.lvlCoinsRevard.ToString();
        if (mult)
        {
            revardValue.text = (gameManagerScript.lvlCoinsRevard * 2).ToString();
        }
    }

    void StarsStatus()
    {
        if (gameManagerScript.score > 0)
        {
            starOne.SetActive(true);
        }
        if (gameManagerScript.score > 50)
        {
            starTwo.SetActive(true);
        }
        if (gameManagerScript.score >= 100)
        {
            starThree.SetActive(true);
        }
    }

    void ScaleAnimation()
    {

        haloTwo.transform.Rotate(Vector3.forward * 1);
        haloYellow.transform.localScale = new Vector3(Mathf.PingPong(Time.time, maximum - minimum) + minimum, Mathf.PingPong(Time.time, maximum - minimum) + minimum);
        haloTwo.transform.localScale = new Vector3(Mathf.PingPong(Time.time, maximum - minimum) + minimum, Mathf.PingPong(Time.time, maximum - minimum) + minimum);
        stageStatus.transform.localScale = new Vector3(Mathf.PingPong(Time.time, maximum - minimum) + minimum, Mathf.PingPong(Time.time, maximum - minimum) + minimum);
    }

    IEnumerator ScaleAnimationTimer(float value)
    {
        yield return new WaitForSeconds(value);
        particleSystem.Play();
        invoke = false;
        haloTwo.transform.DOScale(maximum, 2f);
        haloTwo.transform.DOScale(maximum, 2f);
        stageStatus.transform.DOScale(maximum, 2f);
        haloYellow.transform.DOScale(maximum, 2f);
    }
}
