using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public GameObject homePopup;

    private void OnEnable()
    {
        homePopup.gameObject.SetActive(false);
    }

    public void _OpenBackHomePopup()
    {
        Time.timeScale = 0f;

        homePopup.gameObject.SetActive(true);
    }

    public void _YesButton()
    {
        Time.timeScale = 1f;

        homePopup.gameObject.SetActive(false);

        GameController.instance._GameplayBackToHome();
    }

    public void _NoButton()
    {
        Time.timeScale = 1f;

        homePopup.gameObject.SetActive(false);
    }
}
