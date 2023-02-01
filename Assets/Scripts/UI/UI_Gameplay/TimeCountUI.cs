using System;
using TMPro;
using UnityEngine;

public class TimeCountUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    public SoundEffectSO beepSound;

    private bool isPlaying;

    private int mm, ss;

    private string o;

    public void _SetTime(float seconds)
    {
        ss = (int)seconds;

        if (ss >= 60)
        {
            mm = 1;
            ss = 0;
        }
        else
        {
            mm = 0;
        }

        o = ss > 9 ? "" : "0";
        
        // TimeSpan time = TimeSpan.FromSeconds(seconds);
        //
        // DateTime dateTime = DateTime.Today.Add(time);
        
        timeText.text = $"0{mm}:{o}{ss}";
        
        timeText.color = seconds > 4 ? Color.white : Color.red;
        
        if (seconds == 0) isPlaying = false;
        
        if (seconds > 4 || isPlaying) return;
        
        isPlaying = true;
        beepSound.Play();
    }
}
