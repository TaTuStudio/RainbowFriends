using AppsFlyerSDK;
using Firebase.Analytics;
using System.Collections.Generic;
using UnityEngine;
using static MaxSdkBase;

public class SdkManager : MonoBehaviour
{
    public static SdkManager Instance;

    private string currentGame;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                // app = Firebase.FirebaseApp.DefaultInstance;
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            } else {
                Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
        SendFAOpenApp();
    }
    
    #region Firebase Analytics
    //Log events
    public void SendFAStartLevel(string minigame_type)
    {
        FirebaseAnalytics.LogEvent("play_mode", "mode", minigame_type);
        currentGame = minigame_type;
    }

    public void SendFALoseLevel() => FirebaseAnalytics.LogEvent("lose_mode", "minigame_type", currentGame);
    public void SendFAWinLevel() => FirebaseAnalytics.LogEvent("win_mode", "minigame_type", currentGame);
    public void SendFAOpenApp() => FirebaseAnalytics.LogEvent("open_app", "", "");
    public void SendFAInterAttempt() => FirebaseAnalytics.LogEvent("inter_attempt", "", "");
    public void SendFARewardSuccess() => FirebaseAnalytics.LogEvent("reward_success", "", "");
    public void SendFARewardFail() => FirebaseAnalytics.LogEvent("reward_fail", "", "");
    public void SendFARewardError() => FirebaseAnalytics.LogEvent("reward_not_available", "", "");
    public void SendFARewardAttempt() => FirebaseAnalytics.LogEvent("reward_attempt", "", "");
    public void SendFAReward(string reward_type)
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventSelectContent,
          new Parameter("af_reward", ""),
          new Parameter("reward_type", reward_type));
    }
    
    public void OnAdRevenuePaidEvent(string adUnitId, AdInfo impressionData)
    {
        double revenue = impressionData.Revenue;
        var impressionParameters = new[] {
            new Parameter("ad_platform", "AppLovin"),
            new Parameter("ad_source", impressionData.NetworkName),
            new Parameter("ad_unit_name", impressionData.AdUnitIdentifier),
            new Parameter("ad_format", impressionData.AdFormat),
            new Parameter("value", revenue),
            new Parameter("currency", "USD"), // All AppLovin revenue is sent in USD
        };
        FirebaseAnalytics.LogEvent("ad_impression", impressionParameters);
    
        SendAdRevenueAppsflyerEvent(impressionData);
    }
    
    // //Set user properties
    // public void SetFAPropertiesRetentType (int retent_type) => FirebaseAnalytics.SetUserProperty("retent_type", retent_type.ToString());
    // public void SetFAPropertiesDaysPlayed(int days_played) => FirebaseAnalytics.SetUserProperty("days_played", days_played.ToString());
    // public void SetFAPropertiesPayingType (int paying_type) => FirebaseAnalytics.SetUserProperty("paying_type", paying_type.ToString());
    // public void SetFAPropertiesLevel (int level) => FirebaseAnalytics.SetUserProperty("level", level.ToString());
    #endregion
    
    #region Appsflyer
    public void SendOpenInterAppsflyerEvent()
    {
        Dictionary<string, string> eventValues = new Dictionary<string, string>();
        //eventValues.Add("name", "af_inters");
        AppsFlyer.sendEvent("af_inters ", eventValues);
    }
    
    public void SendOpenRewardAppsflyerEvent()
    {
        Dictionary<string, string> eventValues = new Dictionary<string, string>();
        //eventValues.Add("name", "af_rewarded");
        AppsFlyer.sendEvent("af_rewarded ", eventValues);
    }
    
    public void SendAdRevenueAppsflyerEvent(AdInfo impressionData)
    {
        Dictionary<string, string> eventValues = new Dictionary<string, string>();
        eventValues.Add("ad_platform", "AppLovin");
        eventValues.Add("ad_source", impressionData.NetworkName);
        eventValues.Add("ad_unit_name", impressionData.AdUnitIdentifier);
        eventValues.Add("ad_format", impressionData.AdFormat);
        eventValues.Add("value", impressionData.Revenue.ToString());
        eventValues.Add("currency", "USD");
        AppsFlyer.sendEvent("af_ad_revenue ", eventValues);
    }
    #endregion

}