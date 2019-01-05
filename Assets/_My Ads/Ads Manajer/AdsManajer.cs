using UnityEngine;
using admob;

// To Call This Ads manajer Write like This :  AdsManajer.AdsInstance."Call What Function You Want";
public class AdsManajer : MonoBehaviour {
    public static AdsManajer AdsInstance;
    //[Tooltip("This Option Use For Demo Testing Uncheck This If You Want To Buid Your Apk")]
    //public bool Test;
    //public Text VideoTextResult;
    // Id For Admob app
    [Header("Admob App ID")]
    [Tooltip("The default Id using Test ID from Google So if you want to publish this App Change this id to your app id")]
    public string AppID;
    // Id For Admob banner
    [Header("Banner Ads Manajer")]
    public string BannerID;
    public enum BannerPos {TOP,CENTER,BOTTOM};
    public BannerPos Position = new BannerPos();
    // Id For Admob interstitial
    [Header("Intertitial Ads Manajer")]
    public string IntertisialID;
    // Id For Admob Video
    [Header("Video Ads Manajer")]
    public string VideoID;

    private void Awake()
    {
        if(AdsInstance == null)
        {
            AdsInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
#if UNITY_EDITOR

#elif UNITY_ANDROID
        InitializeAds();
        ShowAdsBanner();
#endif
    }
    // For Initialize Ads ID
    public void InitializeAds()
    {
        // Initialize App, Banner, Inter, Video Rewarded ID
        Admob.Instance().initSDK(AppID);
        Admob.Instance().initAdmob(BannerID, IntertisialID);

        // Loading Ads inter & Video
        if (!Admob.Instance().isInterstitialReady())
        {
            Admob.Instance().loadInterstitial();
        }

        if (!Admob.Instance().isRewardedVideoReady())
        {
            Admob.Instance().loadRewardedVideo(VideoID);
        }
        // Delegate Event 
        //Admob.Instance().rewardedVideoEventHandler += RewardedVideoEvent;
    }

    [ContextMenu("Show Banner")]
    // for Showing ads Banner
    public void ShowAdsBanner()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        switch (Position)
        {
            case BannerPos.BOTTOM:
                Admob.Instance().showBannerRelative(AdSize.Banner, AdPosition.BOTTOM_CENTER, 5);
                Debug.Log(Position);
                break;
            case BannerPos.CENTER:
                Admob.Instance().showBannerRelative(AdSize.Banner, AdPosition.MIDDLE_CENTER, 5);
                Debug.Log(Position);
                break;
            case BannerPos.TOP:
                Admob.Instance().showBannerRelative(AdSize.Banner, AdPosition.TOP_CENTER, 5);
                Debug.Log(Position);
                break;
        }
#endif
    }

    [ContextMenu("Show Interstitial")]
    // For Showing Interstitial
    public void ShowAdsInterstitial()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        if (Admob.Instance().isInterstitialReady())
            Admob.Instance().showInterstitial();
        else
            Admob.Instance().loadInterstitial();
#endif
    }
    [ContextMenu("Show Video")]
    // For Showing Video Ads
    public void ShowVideoRewarded()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        if (Admob.Instance().isRewardedVideoReady())
            Admob.Instance().showRewardedVideo();
        else
            Admob.Instance().loadRewardedVideo(VideoID);
#endif
    }

    // to Handler rewarded video event 
    //public void RewardedVideoEvent(string Name,string massage)
    //{
    //    if (Name == AdmobEvent.onRewarded)
    //    {
    //        Debug.Log("You Get Reward");
    //        if(Test)
    //        VideoTextResult.text = "You Get Reward UwU ";
    //    }
    //}
}
