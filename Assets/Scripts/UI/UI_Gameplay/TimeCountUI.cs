using System;
using TMPro;
using UnityEngine;

public class TimeCountUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    public SoundEffectSO beepSound;

    private bool isPlaying;

    public void _SetTime(float seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);

        DateTime dateTime = DateTime.Today.Add(time);
    
        timeText.text = dateTime.ToString("mm:ss");

        timeText.color = dateTime.Second > 4 ? Color.white : Color.red;
        
        if (dateTime.Second == 0) isPlaying = false;
        
        if (dateTime.Second != 4 || isPlaying) return;
        isPlaying = true;
        beepSound.Play();

    }
    
}
