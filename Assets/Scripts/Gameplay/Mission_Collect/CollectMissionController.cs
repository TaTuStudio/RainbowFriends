using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMissionController : MonoBehaviour
{
    public static CollectMissionController instance;

    public PlayerAIBrains_CollectController playerAIBrains_CollectController;

    public CollectItemSpawner collectItemSpawner;

    public Transform collector;

    public bool gameplaySet = false;

    public ReuseGO blueMonsterPrefab;
    public Transform blueSpawnPoint;

    public bool win = false;
    public bool lose = false;

    int aiSpawnNum = 9;

    private void Awake()
    {
        _MakeReplaceSingleton();
    }

    private void Start()
    {
        GameController.instance._SetPlaying(false);

        PlayerManager.instance._Clean();

        GameplayUI.instance._GameplayAlphabetCollectSetup();
    }

    private void Update()
    {
        _GameplaySetup();

        _WinLoseCheck();
    }

    void _MakeReplaceSingleton()
    {
        if (CollectMissionController.instance != null && CollectMissionController.instance != this)
        {
            CollectMissionController old = CollectMissionController.instance;

            CollectMissionController.instance = this;

            Destroy(old);
        }
        else
        {
            CollectMissionController.instance = this;
        }
    }

    public void _GameplaySetup()
    {
        if(MapManager.instance.spawnedMap.loadMapDone == true && gameplaySet == false)
        {
            gameplaySet = true;

            _SpawnPlayers();

            MonsterSpawner.instance._SpawnAllMonsters();

            playerAIBrains_CollectController._SetBrainToAIPlayer();

            GameController.instance._SetGameTime(60f);

            GameController.instance._SetPlaying(true);
        }
    }

    void _WinLoseCheck()
    {
        if(win == false && lose == false && gameplaySet == true && GameController.instance.isPlaying == true && PlayerManager.instance.spawnedPlayer != null && PlayerManager.instance.spawnedPlayer.setDefault == false)
        {
            if (collectItemSpawner.spawnedItems.Count > 0 && collectItemSpawner.collectedItems.Count >= collectItemSpawner.spawnedItems.Count)
            {
                win = true;

                GameplayUI.instance._ActiveWinUI(true);

                return;
            }

            if (PlayerManager.instance.spawnedPlayer != null && PlayerManager.instance.spawnedPlayer.isDead)
            {
                lose = true;

                GameplayUI.instance._ActiveDeadUI(true);

                return;
            }

            if (GameController.instance.curGameTime >= GameController.instance.gameTime)
            {
                lose = true;

                GameplayUI.instance._ActiveOutTimeUI(true);

                return;
            }
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
}
