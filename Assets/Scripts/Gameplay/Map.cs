using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public bool loadMapDone;

    public List<Transform> playerSpawnPoints;

    private void OnEnable()
    {
        MapManager.instance.spawnedMap = this;
    }
}
