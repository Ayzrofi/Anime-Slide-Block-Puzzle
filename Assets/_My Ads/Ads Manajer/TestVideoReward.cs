using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using admob;
using UnityEngine.UI;
// this Script for call handle video reward event 
public class TestVideoReward : MonoBehaviour {
    // in this case i use text to display event and also you can change it what you want like setactive game object or load new level
    public Text TextEventResultFromVideoRewarded, eventtxt, massagetxt;

    public void Start()
    {
        // delegate event rewarded video from plugin and subscribe to Videoevent Fuction To get Result
        Admob.Instance().rewardedVideoEventHandler += VideoEvent;
    }


    public void VideoEvent(string EventName, string Massage)
    {
        // displaying result with text in ui canvas
        eventtxt.text = EventName;
        massagetxt.text = Massage;

        // event handle if ads closed
        if (EventName == AdmobEvent.onRewarded )
        {
            TextEventResultFromVideoRewarded.text = "You Get Reward UwU";
        }
        // this failure dont use this 
        //if(EventName == AdmobEvent.onAdClosed)
        //{
        //    // check if ads watch video ads complite 
        //    if (EventName == AdmobEvent.onRewarded)
        //    {
        //        TextEventResultFromVideoRewarded.text = "You Get Reward UwU";
        //    }
        //    //else
        //    //{
        //    //    TextEventResultFromVideoRewarded.text = "You should Finishing watch video to get reward";
        //    //}
        //}

    }
}
