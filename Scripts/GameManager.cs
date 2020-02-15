using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using BayatGames.SaveGameFree;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Button pauseButton;
    public GameObject failMenu;
    public GameObject finishMenu;
    public GameObject backGroundPanel;
    public int score;
    public int lvlCoinsRevard;
    public bool cameraShake;
    public bool mult;
    public bool isZoomed;
    Vector2 startPlayerPosition;
    [SerializeField] bool gameIsPaused;
    [SerializeField] bool revardStatus = true;
    [SerializeField] float offsetX;
    [SerializeField] float offsetY;
    [SerializeField] float shakeTime = 3.3f;
    GameObject player;
    new Transform camera;
    PlayerController playerControllerScript;
    AudioSource audioSource;
    [SerializeField] int coinsNumber;
    FinishMenuScript finishMenuScript;
    public bool stageClear;

    void Awake()
    {
        if (!SaveGame.Load<bool>("NewGame"))
        {
            SaveGame.Save<bool>($"Player_defaultPurchasedStatus", true);
            SaveGame.Save<string>("ActiveSkin", "Player_default");
            SaveGame.Save<bool>("NewGame", true);
            var playerStats = new Dictionary<string, float>
            {
                { "maxspeed", 5f },
                { "maxlives", 3 }
            };
            SaveGame.Save<Dictionary<string, float>>("playerStats", playerStats);
        }

        GameResume(); //fix time.timescale bug
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            camera = GetComponent<Transform>();
            audioSource = GetComponent<AudioSource>();
            startPlayerPosition = GameObject.Find("PlayerStartPosition").GetComponent<Transform>().position;
            player = GameObject.Find("PlayerGameObject");
            playerControllerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            if (SaveGame.Load<int>($"{SceneManager.GetActiveScene().name}_score") != 0)
            {
                lvlCoinsRevard = (10 * SceneManager.GetActiveScene().buildIndex) / 2;
            }
            else
            {
                lvlCoinsRevard = 10 * SceneManager.GetActiveScene().buildIndex;
            }
            finishMenuScript = finishMenu.GetComponent<FinishMenuScript>();
            player.transform.position = startPlayerPosition;
        }
    }

    void Update()
    {
        coinsNumber = SaveGame.Load<int>("Coins");
        EscapeButtonInvoke();
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            CameraFollowPlayer();
            GameStatus();
        }
    }
    public void CheckPointSystem()
    {
        playerControllerScript.CheckPointLoad();
        backGroundPanel.SetActive(false);
        failMenu.SetActive(false);
        audioSource.Play();
    }
    void GameStatus()
    {
        if (playerControllerScript.gameOver)
        {
            audioSource.Pause();
            backGroundPanel.SetActive(true);
            failMenu.SetActive(true);
            playerControllerScript.gameOver = false; //bug with time.timescale after restart
            CameraShake(shakeTime);
        }
        else if (playerControllerScript.gameFinish)
        {
            score = playerControllerScript.score;
            audioSource.Pause();
            finishMenu.SetActive(true);
            backGroundPanel.SetActive(true);
            stageClear = true;
            if (revardStatus)
            {
                lvlCoinsRevard += playerControllerScript.coinsCollected;
                if (SaveGame.Load<string>("ActiveSkin") == "Player_king")
                {
                    lvlCoinsRevard *= 2;
                    SaveGame.Save<int>("Coins", coinsNumber += lvlCoinsRevard);
                }
                else
                {
                    SaveGame.Save<int>("Coins", coinsNumber += lvlCoinsRevard);
                }
                
                if (SaveGame.Load<int>($"{SceneManager.GetActiveScene().name}_score") < score)
                {
                    SaveGame.Save<int>($"{SceneManager.GetActiveScene().name}_score", score);
                }
                SaveGame.Save<int>($"Level{SceneManager.GetActiveScene().buildIndex + 1}", 1);

                revardStatus = false;
            }

            if (mult)
            {
                Debug.Log("Mult coins");
                finishMenuScript.mult = true;
                SaveGame.Save<int>("Coins", coinsNumber += lvlCoinsRevard);
                mult = false;
            }
        }

        if (cameraShake)
        {
            CameraShake(shakeTime);
        }
    }

    void CameraFollowPlayer()
    {
        transform.rotation = Quaternion.identity;
        if (isZoomed)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -35);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -16);
        }
    }

    public void CameraShake(float time)
    {
        offsetX = 0.75f;
        offsetY = 0.75f;
        Quaternion rotate = Quaternion.Euler(Random.Range(-offsetX, offsetX), Random.Range(-offsetY, offsetY), 0f);
        camera.transform.localRotation = Quaternion.Slerp(camera.localRotation, camera.localRotation * rotate, 0.75f);
        StartCoroutine(ShakeTimer(time));
    }
    IEnumerator ShakeTimer(float time)
    {
        yield return new WaitForSeconds(time);
        offsetX = 0f;
        offsetY = 0f;
    }


    void EscapeButtonInvoke()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Menu")
        {
            pauseButton.onClick.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Menu")
        {
            Application.Quit();
        }
    }

    public void NextLvl()
    {
        if (Application.CanStreamedLevelBeLoaded($"Level{SceneManager.GetActiveScene().buildIndex + 1}"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }


    }
    public void OpenPrivPolUrl()
    {
        Application.OpenURL("https://ohno-gameappsmaster.flycricket.io/privacy.html");
    }

    public void RestartLvl()
    {
        player.transform.position = startPlayerPosition;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (gameIsPaused)
        {
            GameResume();
        }
    }

    public void ReturnToMMenu()
    {
        SceneManager.LoadScene("Menu");
        if (gameIsPaused)
        {
            GameResume();
        }
    }

    public void GamePause()
    {
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void GameResume()
    {
        Time.timeScale = 1;
        gameIsPaused = false;
    }
}


