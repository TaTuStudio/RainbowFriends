using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerSkinShopItem : MonoBehaviour
{
    public ShopUI shopUI;

    public string playerSkinID = "";
    public GameObject lockGO;
    public GameObject selectGO;
    public GameObject usingGO;

    public Image skinImage;

    public bool unlocked = false;

    public void _SetInfo(ShopUI shop, string skinID, Sprite skinSprite)
    {
        shopUI = shop;
        playerSkinID = skinID;
        skinImage.sprite = skinSprite;

        _CheckUnlock();

        _CheckUsing();

        gameObject.SetActive(true);

        gameObject.name = "Player Skin " + playerSkinID;
    }

    public void _SetDeactive()
    {
        gameObject.name = "none Skin ";
        playerSkinID = "";
        gameObject.SetActive(false);
    }

    public void _CheckUnlock()
    {
        PlayerStats stats = PlayerStats.instance;

        if (stats.playerUnlockedSkinList.Contains(playerSkinID))
        {
            lockGO.SetActive(false);
            selectGO.SetActive(false);
            usingGO.SetActive(false);
        }
        else
        {
            lockGO.SetActive(true);
            selectGO.SetActive(false);
            usingGO.SetActive(false);
        }
    }

    public void _Unusing()
    {
        usingGO.SetActive(false);
    }

    public void _CheckUsing()
    {
        PlayerStats stats = PlayerStats.instance;

        if (stats.currentPlayerSkinID == playerSkinID)
        {
            if (shopUI.usingItem != null)
            {
                shopUI.usingItem._Unusing();
            }

            shopUI.usingItem = this;

            usingGO.SetActive(true);

            _SetSelected();
        }
    }

    public void _UnSelected()
    {
        selectGO.SetActive(false);
    }

    public void _SetSelected()
    {
        if (shopUI.selectedItem != null)
        {
            shopUI.selectedItem._UnSelected();
        }

        selectGO.SetActive(true);

        shopUI.selectedItem = this;

        shopUI._CheckButtonsStats();

        shopUI._ChangeSkin(playerSkinID);
    }
}
