using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    //public Map selectedMap;

    public string selectedMap = "";

    public Map spawnedMap;

    public List<Vector3> allPoints = new List<Vector3>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _SpawnMap();

        //StartCoroutine(_GetAllNodesPositions());
    }

    public void _SpawnMap()
    {
        StartCoroutine(_UnloadMapScene());
    }

    IEnumerator _LoadMapScene()
    {
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

        yield break;
    }

    IEnumerator _UnloadMapScene()
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

        yield break;
    }

    public IEnumerator _GetAllNodesPositions()
    {
        allPoints.Clear();

        while(AstarPath.active == null)
        {
            yield return null;
        }

        var gg = AstarPath.active.data.layerGridGraph;
        List<GraphNode> nodes = new List<GraphNode>();
        gg.GetNodes((System.Action<GraphNode>)nodes.Add);

        foreach (GraphNode n in nodes)
        {
            allPoints.Add((Vector3)n.position);
        }

        yield break;
    }
}
