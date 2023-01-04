using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    //public Map selectedMap;

    //public int selectedMapIndex = 0;
    public string selectedMap = "";

    public Map spawnedMap;

    public List<Vector3> allPoints = new();

    public AudioSource BGMAudioSource;

    public SoundEffectSO bgm;

    private bool loadingMap;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //_SpawnMap();
    
        //StartCoroutine(_GetAllNodesPositions());

        bgm.Play(null, true, BGMAudioSource);
    }

    public void _SpawnMap()
    {
        if(loadingMap) return;
        StartCoroutine(_CheckSceneSpawned());
        BGMAudioSource.Pause();
    }

    IEnumerator _LoadMapScene()
    {
        loadingMap = true;
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(selectedMap, LoadSceneMode.Additive);

        //operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            //float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            yield return null;
        }

        Debug.Log("Load map " + selectedMap + " done");

        //operation.allowSceneActivation = true;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(selectedMap));

        spawnedMap.loadMapDone = true;
        loadingMap = false;
    }

    IEnumerator _CheckSceneSpawned()
    {
        if (spawnedMap != null)
        {
            AsyncOperation operation = SceneManager.UnloadSceneAsync(selectedMap);

            while (!operation.isDone)
            {
                //float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

                yield return null;
            }

            StartCoroutine(_LoadMapScene());
        }
        else
        {
            StartCoroutine(_LoadMapScene());
        }
    }

    public void _UnloadScene()
    {
        if (spawnedMap == null) return;
        
        SceneManager.UnloadSceneAsync(selectedMap);
            
        BGMAudioSource.Play();
    }

    // public IEnumerator _GetAllNodesPositions()
    // {
    //     allPoints.Clear();
    //
    //     while(AstarPath.active == null)
    //     {
    //         yield return null;
    //     }
    //
    //     var gg = AstarPath.active.data.layerGridGraph;
    //     List<GraphNode> nodes = new List<GraphNode>();
    //     gg.GetNodes((System.Action<GraphNode>)nodes.Add);
    //
    //     foreach (GraphNode n in nodes)
    //     {
    //         allPoints.Add((Vector3)n.position);
    //     }
    //
    //     yield break;
    // }
}
