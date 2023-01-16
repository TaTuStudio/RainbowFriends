using System;
using Facebook.Unity;
using UnityEngine;
using UnityEngine.UI;

public class FBWindowsA2UNotificationsManager : MonoBehaviour {

	public FBWindowsLogsManager Logger;
	public InputField TitleText;
	public InputField BodyText;
	public InputField MediaText;
	public InputField PayloadText;
	public InputField TimeIntervalText;

	public void ScheduleButton () {
		Logger.DebugLog("Scheduling notification ...");
		FB.ScheduleAppToUserNotification(TitleText.text, BodyText.text, new Uri(MediaText.text), int.Parse(TimeIntervalText.text), PayloadText.text, A2UNotificationCallback);
	}

	private void A2UNotificationCallback(IScheduleAppToUserNotificationResult result)
    {
		if (result.Error != null)
        {
			Logger.DebugErrorLog(result.Error);
        }
        else
        {
			Logger.DebugLog("Notification scheduled correctly.");
		}
    }


}
