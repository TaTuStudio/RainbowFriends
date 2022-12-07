using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private SceneLoaderSO loadMainMenu;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        loadMainMenu.LoadScene();
        slider.DOValue(1, 2f).SetEase(Ease.InCubic).OnComplete(()=>SceneManager.UnloadSceneAsync(0));
    }
    
}
