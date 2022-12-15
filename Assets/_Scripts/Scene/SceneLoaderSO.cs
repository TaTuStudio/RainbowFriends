using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

[CreateAssetMenu(fileName = "SceneLoader", menuName = "SceneLoader")]
public class SceneLoaderSO : ScriptableObject
{
    public AssetReference Scene;

    public AsyncOperationHandle<SceneInstance> Handle { get; private set; }

    // Start is called before the first frame update
    public void LoadScene(bool activate = false)
    {
        Addressables.LoadSceneAsync(Scene, UnityEngine.SceneManagement.LoadSceneMode.Additive, activate).Completed +=
            SceneLoadCompleted;
    }

    private void SceneLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status != AsyncOperationStatus.Succeeded) return;
        Debug.Log("Successfully loaded Scene.");
        Handle = obj;
    }

    public void UnloadScene()
    {
        Addressables.UnloadSceneAsync(Handle).Completed += (_) =>
        {
            if (_.Status == AsyncOperationStatus.Succeeded)
                Debug.Log("Successfully unloaded Scene.");
        };
    }
}