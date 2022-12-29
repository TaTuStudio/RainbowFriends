using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public UI_PlayerSkinShopItem usingItem;

    public UI_PlayerSkinShopItem selectedItem;

    public int skinPrice, rewardCoin;
    public TextMeshProUGUI priceText, rewardText;
    public GameObject buyBttn, watchAdBttn, useBttn;

    public bool getItems = false;

    public ScrollRect itemScrollView;
    public List<UI_PlayerSkinShopItem> skinItems = new List<UI_PlayerSkinShopItem>();

    public string playerSkinMat = "";
    public string boxSkinMat = "";

    public SkinnedMeshRenderer playerSkinnedMesh;
    public SkinnedMeshRenderer boxSkinnedMesh;
    public SkinnedMeshRenderer holdBoxSkinnedMesh;

    public delegate void BuySkin(string skinName);

    public static BuySkin OnBuySkin;

    public void _SetItemInfos(List<UI_PlayerSkinShopItem> itemList, List<PlayerSkinScriptObj.Skin2DInfo> skinInfoList)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            UI_PlayerSkinShopItem item = itemList[i];

            if (i < skinInfoList.Count)
            {
                PlayerSkinScriptObj.Skin2DInfo info = skinInfoList[i];

                item._SetInfo(this, info.skinID, info.skinAvatar);
            }
            else
            {
                item._SetDeactive();
            }
        }

        _CheckButtonsStats();
    }

    public void _CheckButtonsStats()
    {
        if (selectedItem == usingItem)
        {
            buyBttn.SetActive(false);
            watchAdBttn.SetActive(false);
            useBttn.SetActive(false);
        }

        if (selectedItem != usingItem)
        {
            if (PlayerStats.instance.playerUnlockedSkinList.Contains(selectedItem.playerSkinID))
            {
                buyBttn.SetActive(false);
                watchAdBttn.SetActive(false);
                useBttn.SetActive(true);
            }
            else
            {
                buyBttn.SetActive(true);
                watchAdBttn.SetActive(true);
                useBttn.SetActive(false);
            }
        }

        priceText.text = "" + skinPrice;

        rewardText.text = "+" + rewardCoin;
    }

    public void _ChangeSkin(string skinID)
    {
        PlayerSkinScriptObj.Skin2DInfo skinInfo = PlayerManager.instance.playerSkinScriptObj._GetSkinInfo(skinID);

        PlayerManager.instance.playerSkinScriptObj._Change(ref playerSkinnedMesh, playerSkinMat, boxSkinMat, skinInfo);
        PlayerManager.instance.playerSkinScriptObj._Change(ref boxSkinnedMesh, playerSkinMat, boxSkinMat, skinInfo);
        PlayerManager.instance.playerSkinScriptObj._Change(ref holdBoxSkinnedMesh, playerSkinMat, boxSkinMat, skinInfo);
    }

    public void _BuyButton()
    {
        if (PlayerStats.instance.coin < skinPrice) return;
        
        var stats = PlayerStats.instance;

        if (stats.playerUnlockedSkinList.Contains(selectedItem.playerSkinID) != false) return;
        stats.playerUnlockedSkinList.Add(selectedItem.playerSkinID);

        stats._SubCoin(skinPrice);

        OnBuySkin?.Invoke(selectedItem.playerSkinID);
        
        _CheckButtonsStats();
    }

    public void _WatchAdButton()
    {
        _WatchAdButtonDone();
    }

    public void _WatchAdButtonDone()
    {
        PlayerStats stats = PlayerStats.instance;

        stats._AddCoin(rewardCoin);
    }

    public void _UseButton()
    {
        PlayerStats stats = PlayerStats.instance;

        stats.currentPlayerSkinID = selectedItem.playerSkinID;

        stats.save = true;

        selectedItem._CheckUsing();

        _CheckButtonsStats();
    }

    public void _OpenNormalShopButton()
    {
        _SetItemInfos(skinItems, PlayerManager.instance.playerSkinScriptObj.playerSkins);

        itemScrollView.normalizedPosition = new Vector2(0f, 1f);
    }

    public void _OpenShopUI()
    {
        GameplayUI.instance._ActiveSelectShopUI(true);

        _OpenNormalShopButton();
    }

    public void _BackButton()
    {
        GameplayUI.instance._HomeUISetup();
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying == false)
        {
            if (getItems)
            {
                getItems = false;

                skinItems.Clear();

                UI_PlayerSkinShopItem[] items = transform.GetComponentsInChildren<UI_PlayerSkinShopItem>();

                foreach (UI_PlayerSkinShopItem item in items)
                {
                    skinItems.Add(item);
                }
            }
        }
    }
#endif
}
