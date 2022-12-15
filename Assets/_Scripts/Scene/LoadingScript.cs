using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    [SerializeField] private Slider sliderReal;
    [SerializeField] private Slider sliderFake;
    
    [SerializeField] private SceneLoaderSO loadMainMenu;

    private bool load;
    
    private IEnumerator Start()
    {
        var _a = StartCoroutine(SliderChangeValue());
        var _b = StartCoroutine(LoadScene());
    
        yield return _a;
        yield return _b;
    
        loadMainMenu.Handle.Result.ActivateAsync().completed += (_) => SceneManager.UnloadSceneAsync(0);
    }
    
    private IEnumerator SliderChangeValue()
    {
        yield return sliderFake.DOValue(1, 2f).SetEase(Ease.InCubic).WaitForCompletion();
    }
    
    private IEnumerator LoadScene()
    {
        loadMainMenu.LoadScene();
        load = true;
        yield return loadMainMenu.Handle.WaitForCompletion();
    }

    private void Update()
    {
        if (!load) return;
        sliderReal.value = loadMainMenu.Handle.PercentComplete;
    }

    // private async void Start()
    // {
    //     loadMainMenu.LoadScene();
    //     //loadTestPlayer.LoadScene();
    //
    //     var _tasks = new List<UniTask>
    //     {
    //         loadMainMenu.Handle.Task.AsUniTask(),
    //         slider.DOValue(1, 2f).SetEase(Ease.InCubic).AsyncWaitForCompletion().AsUniTask()
    //     };
    //     
    //     await UniTask.WhenAll(_tasks);
    //     
    //     loadMainMenu.Handle.Result.ActivateAsync().completed += (_) =>
    //     {
    //         //loadTestPlayer.Handle.Result.ActivateAsync();
    //         SceneManager.UnloadSceneAsync(0);
    //     };
    // }
}