using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdsManager Instance;
    string gameId = "5625831";

    string interstitialAd = "Interstitial_Android";
    string bannedAdId = "";

    private void Awake()
    {
        Instance = this;
        InitializeAds();
    }

    void InitializeAds()
    {
        Advertisement.Initialize(gameId, false, this);
    }

    public void showAds()
    {
        Advertisement.Load(interstitialAd, this);
        Advertisement.Show(interstitialAd, this);
    }

    public void OnInitializationComplete()
    {
        //Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad Loaded: " + placementId);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Ad Failed to Load: " + placementId);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Ad Show Start: " + placementId);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Ad Show Completed: " + placementId);
        if (placementId == interstitialAd && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            GameManager.instance.ResumeGame();
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Ad Show Failed: " + placementId);
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Ad Show Start: " + placementId);
    }
}
