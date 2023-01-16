using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectItemSpawner : MonoBehaviour
{
    public bool gameplaySet = false;

    public List<ReuseGO> collectItemPrefabs = new List<ReuseGO>();

    public List<Transform> spawnPoints = new List<Transform>();

    public List<ReuseGO> spawnedItems = new List<ReuseGO>();

    public List<ReuseGO> collectedItems = new List<ReuseGO>();

    private void OnEnable()
    {
        MapManager.OnMapLoaded += _GameplaySetup;
    }

    private void OnDisable()
    {
        MapManager.OnMapLoaded -= _GameplaySetup;
    }

    private void Update()
    {
        //_GameplaySetup();
    }

    //private void OnDisable()
    //{
    //    _Clean();
    //}

    public void _GameplaySetup()
    {
        //if (MapManager.instance.spawnedMap.loadMapDone == true && gameplaySet == false)
        {
            gameplaySet = true;

            _SpawnCollectItems();
        }
    }

    public void _SpawnBox(ReuseGO prefab, Vector3 spawnPos)
    {
        ReuseGO selected = UnusedManager.instance._GetReuseGO(prefab.itemID);

        if (selected != null)
        {
            selected.transform.parent = transform;
            selected.transform.position = spawnPos;

            selected.gameObject.SetActive(true);

            spawnedItems.Add(selected);
        }
        else
        {
            selected = Instantiate(prefab, spawnPos, Quaternion.identity, transform);

            spawnedItems.Add(selected);
        }
    }

    void _Clean()
    {
        collectedItems.Clear();

        while (spawnedItems.Count > 0)
        {
            UnusedManager.instance._AddToUnusedGO(spawnedItems[0]);

            spawnedItems.RemoveAt(0);
        }
    }

    void _SpawnCollectItems()
    {
        _Clean();

        List<Transform> canSpawnPoints = new List<Transform>();
        canSpawnPoints.AddRange(spawnPoints);

        foreach (ReuseGO go in CollectionMarshal.AsSpan(collectItemPrefabs))
        {
            int ranIndex = Random.Range(0, canSpawnPoints.Count);

            _SpawnBox(go, canSpawnPoints[ranIndex].position);

            canSpawnPoints.Remove(canSpawnPoints[ranIndex]);
        }
    }

    public void _AddToCollected(ReuseGO reuseGO)
    {
        collectedItems.Add(reuseGO);
    }
}
