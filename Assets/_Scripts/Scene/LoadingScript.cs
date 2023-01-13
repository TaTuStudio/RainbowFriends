using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    [SerializeField] private Slider sliderReal;
    [SerializeField] private Slider sliderFake;

    [SerializeField] private float delay;
    //[SerializeField] private SceneLoaderSO loadMainMenu;
    private AsyncOperation loadScene;
    private bool load;
    private void Awake()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif

    }
    private IEnumerator Start()
    {
        var _a = StartCoroutine(SliderChangeValue());
        var _b = StartCoroutine(LoadScene());
    
        yield return _a;
        yield return _b;
    
        //loadMainMenu.Handle.Result.ActivateAsync().completed += _ => SceneManager.UnloadSceneAsync(0);
        SceneManager.UnloadSceneAsync(0).completed += _ => loadScene.allowSceneActivation = true;

    }
    
    private IEnumerator SliderChangeValue()
    {
        yield return sliderFake.DOValue(1, delay).SetEase(Ease.InCubic).WaitForCompletion();
    }
    
    private IEnumerator LoadScene()
    {
        //loadMainMenu.LoadScene();
        loadScene = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        load = true;
        //yield return loadMainMenu.Handle.WaitForCompletion();
        yield return loadScene;
    }

    private void Update()
    {
        if (!load) return;
        sliderReal.value = loadScene.progress;
    }
    
}