using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using admob;

public class AdsController : MonoBehaviour {
    public static AdsController  TheInstanceOfAdsController;
    public string IdApp = "ca-app-pub-3940256099942544~3347511713";
    public string IdBanner = "ca-app-pub-3940256099942544/6300978111";
    public enum BannerPos { TOP, CENTER, BOTTOM };
    public BannerPos Position = new BannerPos();
    public string IdIntertisial = "ca-app-pub-3940256099942544/1033173712";
    public string IdVideo = "ca-app-pub-3940256099942544/5224354917";

    public Admob Ad;

    private void Start()
    {
        if (TheInstanceOfAdsController == null)
        {
            TheInstanceOfAdsController = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        initAds();
    }

    public void initAds()
    {
        Ad = Admob.Instance();
        // Delegate event harus yg paling awal
        Ad.rewardedVideoEventHandler += VideoEventHandler;
        // kemudian initialize id app
        Ad.initSDK(IdApp);
        // kemudian baru initialize id banner/inter/video/native banner
        Ad.initAdmob(IdBanner, IdIntertisial);
        // terakhir Load Ads
        Ad.loadRewardedVideo(IdVideo);
        Ad.loadInterstitial();
    }

    #region Video Ads
    public void ShowAdsVideo()
    {
        if (Ad.isRewardedVideoReady())
        {
            Ad.showRewardedVideo();
        }
        else
        {
            Ad.loadRewardedVideo(IdVideo);
        }
    }

    // this function must call static function from other script 
    public void VideoEventHandler(string eventName, string msg)
    {
        if (eventName == AdmobEvent.onRewarded)
        {
            // like this ... this static function call from other script 
            //TesAds.TheInstanceOfTesAds.RewardVideo(eventName, msg);
            GameMaster.TheInstanceOfGameMaster.WinGameConditions();
        }
    }
    #endregion

    #region Interstitial Ads
    public void ShowInterstitial()
    {
        if (Ad.isInterstitialReady())
        {
            Ad.showInterstitial();
        }else
        {
            Ad.loadInterstitial();
        }
    }
    #endregion

    #region Banner Ads
    public void ShowBanner()
    {
        switch (Position)
        {
            case BannerPos.BOTTOM:
                Ad.showBannerRelative(AdSize.Banner, AdPosition.BOTTOM_CENTER, 5);
                Debug.Log(Position);
                break;
            case BannerPos.CENTER:
                Ad.showBannerRelative(AdSize.Banner, AdPosition.MIDDLE_CENTER, 5);
                Debug.Log(Position);
                break;
            case BannerPos.TOP:
                Ad.showBannerRelative(AdSize.Banner, AdPosition.TOP_CENTER, 5);
                Debug.Log(Position);
                break;
        }
    }

    #endregion
}
