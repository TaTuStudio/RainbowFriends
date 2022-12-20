using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevelMenu : MonoBehaviour
{
    [SerializeField] PlayerDataSO playerData;
    [SerializeField] GameObject Notification;
    [SerializeField] Text textNotification;
    [SerializeField] GameObject iconLock;
    [SerializeField] UiController uiController;
    private bool isUnlock =false;
    public int _level;
    public int _cost;
    int cost;
    [SerializeField] private int index;
    public void LoadMap(int level)
    {
        
        Notification.SetActive(true);
        level = _level;
        index = level;
        uiController.selectCurrent = level-1;
        Debug.Log(level);
    }
    public void UnlockMap()
    {
        switch (index)
        {
            case 1:
                cost = 1000;

                break;
            case 2:
                cost = 2000;
                break;
            case 3:
                cost = 3000;
                break;
            case 4:
                cost = 4000;
                break;
            case 5:
                cost = 5000;
                break;

        }
        //cost = _cost;
        //Debug.Log(playerData.Coin);
        Debug.Log(isUnlock);

        if (playerData.Coin >= cost && isUnlock==false)
        {
            playerData.Coin -= cost;
            iconLock.SetActive(false);
            isUnlock = true;
            Notification.SetActive(false);
        }
    }
    
    
}
