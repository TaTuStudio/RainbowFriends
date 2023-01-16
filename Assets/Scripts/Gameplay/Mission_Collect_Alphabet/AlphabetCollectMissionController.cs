using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphabetCollectMissionController : MonoBehaviour
{
    public static AlphabetCollectMissionController instance;

    public PlayerAIBrains_CollectController playerAIBrains_CollectController;

    public CollectItemSpawner collectItemSpawner;

    public Transform collector;

    public bool gameplaySet = false;

    public ReuseGO blueMonsterPrefab;
    public Transform blueSpawnPoint;

    public bool win = false;
    public bool lose = false;

    int aiSpawnNum = 9;
    private void OnEnable()
    {
        MapManager.OnMapLoaded += _GameplaySetup;
    }

    private void OnDisable()
    {
        MapManager.OnMapLoaded -= _GameplaySetup;
    }
    private void Awake()
    {
        _MakeReplaceSingleton();
    }

    private void Start()
    {
        GameController.instance._GameplayReadySetup();
        GameplayUI.instance._GameplayAlphabetCollectSetup();
    }

    private void Update()
    {
        // _GameplaySetup();

        _ResetWinLose();

        _WinLoseCheck();
    }

    void _MakeReplaceSingleton()
    {
        if (AlphabetCollectMissionController.instance != null && AlphabetCollectMissionController.instance != this)
        {
            AlphabetCollectMissionController old = AlphabetCollectMissionController.instance;

            AlphabetCollectMissionController.instance = this;

            Destroy(old);
        }
        else
        {
            AlphabetCollectMissionController.instance = this;
        }
    }

    public void _GameplaySetup()
    {
        // if(MapManager.instance.spawnedMap.loadMapDone == true && gameplaySet == false)
        {
            GameController.instance._SetGameplaySetupDone(true);

            gameplaySet = true;

            PlayerManager.instance._SpawnPlayerAndAIPlayers(aiSpawnNum);

            MonsterSpawner.instance._SpawnAllMonsters();
            MonsterStaticScareSpawner.instance._SpawnAllMonsters();

            playerAIBrains_CollectController._SetBrainToAIPlayer();

            GameController.instance._SetGameTime(60f);

            //GameController.instance._SetPlaying(true);
        }
    }

    void _ResetWinLose()
    {
        if (GameController.instance.resetWinLose)
        {
            GameController.instance.resetWinLose = false;

            win = false;
            lose = false;
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

            if (GameController.instance.curGameTime <= 0f)
            {
                lose = true;

                GameplayUI.instance._ActiveOutTimeUI(true);

                return;
            }
        }
    }
}
