using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TesAds : MonoBehaviour {
    private static TesAds instance;
    public static TesAds TheInstanceOfTesAds
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TesAds>();
            }
            return instance;
        }
    }
    public Text Result, EventName, Message;

    [ContextMenu("UWU")]
    public void RewardVideo(string Name, string Msg )
    {
        EventName.text = Name;
        Message.text = Msg;
        Result.text = "you Get Reward UwU";
    }

    //public static void EventVideoBokep(string R,string E,string M)
    //{
    //    TheInstanceOfTesAds.RewardVideo(R, E, M);
    //}
    public void ShowVideo()
    {
        AdsController.TheInstanceOfAdsController.ShowAdsVideo();
    }

    public void ShowInterstitial()
    {
        AdsController.TheInstanceOfAdsController.ShowInterstitial();
    }

    public void ShowBanner()
    {
        AdsController.TheInstanceOfAdsController.ShowBanner();
    }
    public void Restart()
    {
        SceneManager.LoadScene(Application.loadedLevel);
    }
}
