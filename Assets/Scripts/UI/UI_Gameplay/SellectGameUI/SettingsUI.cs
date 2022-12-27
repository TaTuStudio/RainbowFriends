using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public bool sound = true;
    public bool vibrate = true;
    public bool music = true;

    public Toggle soundSwitch, /*vibrateSwitch,*/ musicSwitch;

    public GameObject settingPopup;

    private void OnEnable()
    {
        _CloseSettingPopup();

        _GetStats();
    }

    private void Update()
    {
        _UpdateButtonStats();
    }

    void _UpdateButtonStats()
    {
        if (settingPopup.activeSelf)
        {
            soundSwitch.isOn = sound;

            //vibrateSwitch.isOn = vibrate;

            musicSwitch.isOn = music;
        }
    }

    public void _GetStats()
    {
        int soundStats = PlayerPrefs.GetInt("sound"); // = 0 mean no set, = 1 set true, = 2 set false

        if (soundStats == 0)
        {
            PlayerPrefs.SetInt("sound", 1);

            soundStats = 1;
        }

        if (soundStats == 1)
        {
            sound = true;
        }
        else
        {
            sound = false;
        }

        int vibrateStats = PlayerPrefs.GetInt("vibrate"); // = 0 mean no set, = 1 set true, = 2 set false

        if (vibrateStats == 0)
        {
            PlayerPrefs.SetInt("vibrate", 1);

            vibrateStats = 1;
        }

        if (vibrateStats == 1)
        {
            vibrate = true;
        }
        else
        {
            vibrate = false;
        }

        int musicStats = PlayerPrefs.GetInt("music"); // = 0 mean no set, = 1 set true, = 2 set false

        if (musicStats == 0)
        {
            PlayerPrefs.SetInt("music", 1);

            musicStats = 1;
        }

        if (musicStats == 1)
        {
            music = true;
        }
        else
        {
            music = false;
        }
    }

    public void _VibrateButton()
    {
        //Debug.Log("_VibrateButton");

        if (vibrate)
        {
            PlayerPrefs.SetInt("vibrate", 2);
        }
        else
        {
            PlayerPrefs.SetInt("vibrate", 1);
        }

        _GetStats();
    }

    public void _SoundButton()
    {
        //Debug.Log("_SoundButton");

        if (sound)
        {
            PlayerPrefs.SetInt("sound", 2);
        }
        else
        {
            PlayerPrefs.SetInt("sound", 1);
        }

        _GetStats();
    }

    public void _MusicButton()
    {
        //Debug.Log("_MusicButton");

        if (music)
        {
            PlayerPrefs.SetInt("music", 2);
        }
        else
        {
            PlayerPrefs.SetInt("music", 1);
        }

        _GetStats();
    }

    public void _OpenSettingPopup()
    {
        settingPopup.SetActive(true);
    }

    public void _CloseSettingPopup()
    {
        settingPopup.SetActive(false);
    }
}
