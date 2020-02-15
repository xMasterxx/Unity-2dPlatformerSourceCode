
using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using BayatGames.SaveGameFree;


public class AdsManager : Singleton<AdsManager>
{
#if UNITY_IOS
    private readonly string gameId = "gameId";
#elif UNITY_ANDROID
    private readonly string gameId = "gameId";
#endif

    private readonly string bannertId = "Banner";
    private readonly bool testMode = false;
    readonly float tiemrTime = 0.5f;
    GameManager gameManagerScript;
    

    void Start()
    {
        gameManagerScript = GameObject.FindWithTag("MainCamera").GetComponent<GameManager>();
        Advertisement.Initialize(gameId, testMode);
    }

    public void DisableBanner()
    {
        Debug.Log("Banner hided");
        Advertisement.Banner.Hide();
    }

    public void ShowBanner()
    {
        StartCoroutine(ShowBannerWhenReady(tiemrTime));
    }

    IEnumerator ShowBannerWhenReady(float time)
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        while (!Advertisement.IsReady(bannertId))
        {
            yield return new WaitForSeconds(time);
        }
        Debug.Log("Banner showed");
        Advertisement.Banner.Show(bannertId);
    }

    public void AdRewardCheckpoint()
    {
        if (!gameManagerScript.stageClear)
        {
            gameManagerScript.CheckPointSystem();
        }
            
    }

    public void AdRewardCoins(int amount)
    {
        var coinsNumber = SaveGame.Load<int>("Coins");
        SaveGame.Save<int>("Coins", coinsNumber += amount);
    }

    public void AdRewardCoinsMult()
    {
        gameManagerScript.mult = true;
    }

}


