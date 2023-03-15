using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections.Generic;
using System.Collections;

public class CustomRewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms

    [SerializeField] float waitSecondsForAds = 60f;
    [SerializeField] int rewardPoints = 5;

    public static CustomRewardedAdsButton instance;
    public GameObject Player;
    private bool isBtnClicked = false;

    void Awake()
    {
        instance = this;
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        //Disable the button until the ad is ready to show:
        _showAdButton.interactable = false;
    }

    void Start()
    {
        LoadAd();
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId.Equals(_adUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            _showAdButton.onClick.AddListener(ShowAd);

            // Enable the button for users to click:
            _showAdButton.interactable = true;
        }
    }

    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // Disable the button:
        _showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);

        List<AudioSource> sources = new List<AudioSource>(FindObjectsOfType<AudioSource>());
        foreach (AudioSource source in sources)
        {
            source.mute = true;
        }

        Time.timeScale = 0.0f;
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete() method called.");
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            List<AudioSource> sources = new List<AudioSource>(FindObjectsOfType<AudioSource>());

            if (!isBtnClicked)
            {
                isBtnClicked = true;
                StartCoroutine(WaitForAccess());

                Time.timeScale = 1.0f;

                foreach (AudioSource source in sources)
                {
                    source.mute = false;
                }

                // Grant a reward.
                Player.GetComponent<ScoreSystem>().AddScore(rewardPoints);
            }
        }
    }

    private IEnumerator WaitForAccess()
    {
        yield return new WaitForSecondsRealtime(waitSecondsForAds);
        // Code to execute after waiting for 10 seconds
        // Load another ad:
        if (this != null)
        {
            Advertisement.Load(_adUnitId, this);
        }
        isBtnClicked = false;
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }
}