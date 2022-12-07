using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMissionController : MonoBehaviour
{
    public bool gameplaySet = false;

    public ReuseGO blueMonsterPrefab;
    public Transform blueSpawnPoint;

    int aiSpawnNum = 9;

    private void Update()
    {
        _GameplaySetup();
    }

    public void _GameplaySetup()
    {
        if(MapManager.instance.spawnedMap.loadMapDone == true && gameplaySet == false)
        {
            gameplaySet = true;

            _SpawnPlayers();

            _SpawnMonster();
        }
    }

    void _SpawnPlayers()
    {
        List<Transform> tempSpawnPoints = new List<Transform>();

        tempSpawnPoints.AddRange(MapManager.instance.spawnedMap.playerSpawnPoints);

        Transform selectedPos = tempSpawnPoints[Random.Range(0, tempSpawnPoints.Count)];

        PlayerManager.instance._SpawnPlayer(PlayerManager.instance._GetCurrentPlayer(), selectedPos.position);

        tempSpawnPoints.Remove(selectedPos);

        for (int i = 0; i < aiSpawnNum; i++)
        {
            selectedPos = tempSpawnPoints[Random.Range(0, tempSpawnPoints.Count)];

            PlayerManager.instance._SpawnPlayerAI(PlayerManager.instance._GetRandomPlayerAI(), selectedPos.position);

            tempSpawnPoints.Remove(selectedPos);
        }
    }

    void _SpawnMonster()
    {
        PlayerManager.instance._SpawnMonster(blueMonsterPrefab, blueSpawnPoint.position);
    }
}
