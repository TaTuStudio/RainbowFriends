using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private SceneLoaderSO loadMainMenu;

    private void Start()
    {
        //Init();
        StartCoroutine(WaitAll());
    }

    private IEnumerator SliderChangeValue()
    {
        yield return slider.DOValue(1, 2f).SetEase(Ease.InCubic).WaitForCompletion();
    }

    private IEnumerator LoadScene()
    {
        loadMainMenu.LoadScene();
        yield return loadMainMenu.Handle.WaitForCompletion();
    }

    private IEnumerator WaitAll()
    {
        var _a = StartCoroutine(SliderChangeValue());
        var _b = StartCoroutine(LoadScene());
        
        yield return _a;
        yield return _b;
        
        loadMainMenu.Handle.Result.ActivateAsync().completed += (_) =>
        {
            SceneManager.UnloadSceneAsync(0);
        };
    }
    // private async void Init()
    // {
    //     loadMainMenu.LoadScene();
    //     loadTestPlayer.LoadScene();
    //
    //     var _tasks = new List<Task>
    //     {
    //         loadMainMenu.Handle.Task,
    //         slider.DOValue(1, 2f).SetEase(Ease.InCubic).AsyncWaitForCompletion()
    //     };
    //     
    //     await Task.WhenAll(_tasks);
    //     
    //     loadMainMenu.Handle.Result.ActivateAsync().completed += (_) =>
    //     {
    //         loadTestPlayer.Handle.Result.ActivateAsync();
    //         SceneManager.UnloadSceneAsync(0);
    //     };
    // }
}
