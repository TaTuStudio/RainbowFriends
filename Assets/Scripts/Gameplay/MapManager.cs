using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    public Map selectedMap;

    public Map spawnedMap;

    public List<Vector3> allPoints = new List<Vector3>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //StartCoroutine(_GetAllNodesPositions());
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
