using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

[CreateAssetMenu(fileName = "SceneLoader", menuName = "SceneLoader")]
public class SceneLoaderSO : ScriptableObject
{
    public AssetReference Scene;
    private AsyncOperationHandle<SceneInstance> handle;

    // Start is called before the first frame update
    public void LoadScene()
    {
        Addressables.LoadSceneAsync(Scene, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed +=
            SceneLoadCompleted;
    }

    private void SceneLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status != AsyncOperationStatus.Succeeded) return;
        Debug.Log("Successfully loaded Scene.");
        handle = obj;
    }

    public void UnloadScene()
    {
        Addressables.UnloadSceneAsync(handle).Completed += (_) =>
        {
            if (_.Status == AsyncOperationStatus.Succeeded)
                Debug.Log("Successfully unloaded Scene.");
        };
    }
}