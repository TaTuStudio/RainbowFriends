using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject setting;
    [SerializeField] private GameObject skin;
    [SerializeField] private GameObject[] Notification;


    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private LoadLevelMenu[] loadLevelMenu;
    //[SerializeField] private playersett playerSettings;

    [SerializeField] Sprite[] iconAudio;
    [SerializeField] Sprite[] iconMusic;

    [SerializeField] Image imgMusic;
    [SerializeField] Image imgAudio;

    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private Text[] textNotification;

    private int indexIconAudio = 0;
    private int indexIconMusic = 0;
    public int selectCurrent;


    private void Start()
    {
        coinText.text = playerData.Coin.ToString();
        
    }
    private void Update()
    {
        coinText.text = playerData.Coin.ToString();
        textNotification[selectCurrent].text = " UNLOCK LEVEL WITH "+ loadLevelMenu[selectCurrent]._cost.ToString()  +" COIN";
    }
    public void btnEsc()
    {
        setting.SetActive(false);
        skin.SetActive(false);
        

    }
    public void BtnEscNotification()
    {
        Notification[selectCurrent].SetActive(false);
    }
    public void btnShop()
    {
        setting.SetActive(false);
        skin.SetActive(true);
    }
    public void btnSetting()
    {
        setting.SetActive(true);
        skin.SetActive(false);
    }

    public void btnAudio()
    {
        indexIconAudio++; 
        if (indexIconAudio % 2 == 0)
        {
            imgAudio.sprite = iconAudio[0];
        }
        else
        {
            imgAudio.sprite = iconAudio[1];
        }
    }
    public void btnMusic()
    {
        indexIconMusic++;
        if (indexIconMusic % 2 == 0)
        {
            imgMusic.sprite = iconMusic[0];
        }
        else
        {
            imgMusic.sprite = iconMusic[1];
        }
    }
    
}
