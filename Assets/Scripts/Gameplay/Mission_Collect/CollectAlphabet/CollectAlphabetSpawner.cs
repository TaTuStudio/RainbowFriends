using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAlphabetSpawner : MonoBehaviour
{
    public CollectMissionController collectMissionController;

    public bool gameplaySet = false;

    public List<ReuseGO> alphabetPrefabs = new List<ReuseGO>();

    public List<Transform> spawnPoints = new List<Transform>();

    public List<ReuseGO> spawnedAlphabetBoxes = new List<ReuseGO>();

    private void Start()
    {
        collectMissionController.collectNum = alphabetPrefabs.Count;

        collectMissionController._AddCollectItemNum(0);
    }

    private void Update()
    {
        _GameplaySetup();
    }

    private void OnDisable()
    {
        if (Application.isEditor) return;

        _Clean();
    }

    public void _GameplaySetup()
    {
        if (MapManager.instance.spawnedMap.loadMapDone == true && gameplaySet == false)
        {
            gameplaySet = true;

            _SpawnAlphabetBoxes();
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

            spawnedAlphabetBoxes.Add(selected);
        }
        else
        {
            selected = Instantiate(prefab, spawnPos, Quaternion.identity, transform);

            spawnedAlphabetBoxes.Add(selected);
        }
    }

    void _Clean()
    {
        while(spawnedAlphabetBoxes.Count > 0)
        {
            UnusedManager.instance._AddToUnusedGO(spawnedAlphabetBoxes[0]);

            spawnedAlphabetBoxes.RemoveAt(0);
        }
    }

    void _SpawnAlphabetBoxes()
    {
        _Clean();

        List<Transform> canSpawnPoints = new List<Transform>();
        canSpawnPoints.AddRange(spawnPoints);

        foreach (ReuseGO go in alphabetPrefabs)
        {
            int ranIndex = Random.Range(0, canSpawnPoints.Count);

            _SpawnBox(go, canSpawnPoints[ranIndex].position);

            canSpawnPoints.Remove(canSpawnPoints[ranIndex]);
        }
    }
}
