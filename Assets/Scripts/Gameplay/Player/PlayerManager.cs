using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public PlayerSkinScriptObj playerSkinScriptObj;

    public string selectedPlayerID = "";
    public List<PlayerController> playerPrefabs = new List<PlayerController>();
    public List<PlayerAIController> aIPlayerPrefabs = new List<PlayerAIController>();

    public List<ReuseGO> spawnedMonsters = new List<ReuseGO>();

    public PlayerController spawnedPlayer;

    public List<PlayerAIController> spawnedAIPlayers = new List<PlayerAIController>();

    private void Awake()
    {
        instance = this;
    }

    public void _Clean()
    {
        List<ReuseGO> tempMonsters = new List<ReuseGO>();

        tempMonsters.AddRange(spawnedMonsters);

        foreach(ReuseGO go in tempMonsters)
        {
            UnusedManager.instance._AddToUnusedMonster(go);
        }

        List<PlayerAIController> tempAIPlayers = new List<PlayerAIController>();

        tempAIPlayers.AddRange(spawnedAIPlayers);

        foreach (PlayerAIController go in tempAIPlayers)
        {
            UnusedManager.instance._AddToUnusedMonster(go.GetComponent<ReuseGO>());
        }

        if(spawnedPlayer != null)
        {
            UnusedManager.instance._AddToUnusedPlayer(spawnedPlayer);
        }

    }

    public ReuseGO _SpawnMonster(ReuseGO prefab, Vector3 spawnPos)
    {
        ReuseGO selected = UnusedManager.instance._GetReuseGO(prefab.itemID);

        if(selected != null)
        {
            selected.transform.parent = transform;
            selected.transform.position = spawnPos;

            selected.gameObject.SetActive(true);

            spawnedMonsters.Add(selected);
        }
        else
        {
            selected = Instantiate(prefab, spawnPos, Quaternion.identity, transform);

            spawnedMonsters.Add(selected);
        }

        return selected;
    }

    public ReuseGO _SpawnMonster(ReuseGO prefab, Quaternion spawnRot ,Vector3 spawnPos)
    {
        ReuseGO selected = UnusedManager.instance._GetReuseGO(prefab.itemID);

        if (selected != null)
        {
            selected.transform.parent = transform;
            selected.transform.position = spawnPos;

            selected.gameObject.SetActive(true);

            spawnedMonsters.Add(selected);
        }
        else
        {
            selected = Instantiate(prefab, spawnPos, spawnRot, transform);

            spawnedMonsters.Add(selected);
        }

        return selected;
    }

    public PlayerAIController _GetRandomPlayerAI()
    {
        int ranIndex = Random.Range(0, aIPlayerPrefabs.Count);

        return aIPlayerPrefabs[ranIndex];
    }

    public PlayerAIController _SpawnPlayerAI(PlayerAIController prefab, Vector3 spawnPos)
    {
        PlayerAIController selected = UnusedManager.instance._GetUnusedPlayerAI(prefab.GetComponent<ReuseGO>().itemID);

        if (selected != null)
        {
            selected.transform.parent = transform;
            selected.transform.position = spawnPos;

            selected.gameObject.SetActive(true);

            spawnedAIPlayers.Add(selected);

            GameController.instance.totalPlayer += 1;
        }
        else
        {
            selected = Instantiate(prefab, spawnPos, Quaternion.identity, transform);

            spawnedAIPlayers.Add(selected);

            GameController.instance.totalPlayer += 1;
        }

        return selected;
    }

    public PlayerController _GetCurrentPlayer()
    {
        PlayerController selected = null;

        foreach(PlayerController p in playerPrefabs)
        {
            if(p.GetComponent<ReuseGO>().itemID == selectedPlayerID)
            {
                selected = p;

                break;
            }
        }

        if(selected == null)
        {
            Debug.Log("Player prefab null");
        }

        return selected;
    }

    public void _SpawnPlayer(PlayerController prefab, Vector3 spawnPos)
    {
        PlayerController selected = UnusedManager.instance._GetUnusedPlayer(prefab.GetComponent<ReuseGO>().itemID);

        if (selected != null)
        {
            selected.transform.parent = transform;
            selected.transform.position = spawnPos;

            selected.gameObject.SetActive(true);

            spawnedPlayer = selected;

            GameController.instance.totalPlayer += 1;
        }
        else
        {
            selected = Instantiate(prefab, spawnPos, Quaternion.identity, transform);

            spawnedPlayer = selected;

            GameController.instance.totalPlayer += 1;
        }
    }

    public void _SpawnPlayerAndAIPlayers(int SpawnNum)
    {
        List<Transform> tempSpawnPoints = new List<Transform>();

        tempSpawnPoints.AddRange(MapManager.instance.spawnedMap.playerSpawnPoints);

        Transform selectedPos = tempSpawnPoints[Random.Range(0, tempSpawnPoints.Count)];

        PlayerManager.instance._SpawnPlayer(PlayerManager.instance._GetCurrentPlayer(), selectedPos.position);

        tempSpawnPoints.Remove(selectedPos);

        for (int i = 0; i < SpawnNum; i++)
        {
            selectedPos = tempSpawnPoints[Random.Range(0, tempSpawnPoints.Count)];

            PlayerManager.instance._SpawnPlayerAI(PlayerManager.instance._GetRandomPlayerAI(), selectedPos.position);

            tempSpawnPoints.Remove(selectedPos);
        }
    }
}
