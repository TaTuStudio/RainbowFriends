using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Toggle toggleSfx;

    [SerializeField] private Toggle toggleBgm;

    [SerializeField] private Button changeName;

    [SerializeField] private TextMeshProUGUI playerName;

    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private GameObject bgmOffIcon;
    [SerializeField] private GameObject sfxOffIcon;
    // Start is called before the first frame update
    private void OnEnable()
    {
        toggleBgm.isOn = playerStats.toggleBgm;
        toggleSfx.isOn = playerStats.toggleSfx;
        playerName.text = playerStats.playerName;
        
        sfxOffIcon.SetActive(!playerStats.toggleSfx);
        bgmOffIcon.SetActive(!playerStats.toggleBgm);
        
        toggleBgm.onValueChanged.AddListener(ChangeBgm);
        toggleSfx.onValueChanged.AddListener(ChangeSfx);
        changeName.onClick.AddListener(ChangeName);
    }

    private void ChangeSfx(bool arg0)
    {
        sfxOffIcon.SetActive(!arg0);
        playerStats.toggleSfx = arg0;
        playerStats.save = true;
    }

    private void ChangeBgm(bool arg0)
    {
        bgmOffIcon.SetActive(!arg0);
        playerStats.toggleBgm = arg0;
        playerStats.save = true;
    }

    private void ChangeName()
    {
        playerStats.playerName = playerName.text.Substring(0,10);
        playerStats.save = true;
    }
    
}
