using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AdRewardedMultCoins : MonoBehaviour, IUnityAdsListener
{

    Button myButton;
    AdsManager adsManagerScript;
    private readonly string rewardedId = "rewardedVideo";
    bool onetimeReward = true;

    void Start()
    {
        adsManagerScript = GameObject.Find("AdsManager").GetComponent<AdsManager>();
        myButton = GetComponent<Button>();

        // Set interactivity to be dependent on the Placement’s status:
        myButton.interactable = Advertisement.IsReady(rewardedId);

        // Map the ShowRewardedVideo function to the button’s click listener:
        myButton.onClick.AddListener(ShowRewardedVideo);

        Advertisement.AddListener(this);
    }

    private void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo()
    {
        // Initialize the Ads listener and service:

        Debug.Log("The rewardedAd showed");
        Advertisement.Show(rewardedId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == rewardedId)
        {
            myButton.interactable = true;
        }
        else if (!onetimeReward)
        {
            myButton.interactable = false;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            Debug.Log("The rewardedAd finished");
            if (onetimeReward)
            {
                adsManagerScript.AdRewardCoinsMult();
                onetimeReward = false;
            }
        } 
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}
