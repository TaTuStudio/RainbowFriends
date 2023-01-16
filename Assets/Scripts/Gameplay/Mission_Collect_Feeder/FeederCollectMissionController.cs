using System;
using UnityEngine;

public class FeederCollectMissionController : MonoBehaviour
{
    public static FeederCollectMissionController instance;

    public PlayerAIBrains_Feeder_CollectController playerAIBrains_CollectController;

    public CollectItemSpawner collectItemSpawner;

    public Transform collector;

    public bool gameplaySet;

    public bool win;
    public bool lose;

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
        GameplayUI.instance._GameplayFoodCollectSetup();
    }

    private void Update()
    {
        //_GameplaySetup();

        _ResetWinLose();

        _WinLoseCheck();
    }

    void _MakeReplaceSingleton()
    {
        if (instance != null && instance != this)
        {
            FeederCollectMissionController old = instance;

            instance = this;

            Destroy(old);
        }
        else
        {
            instance = this;
        }
    }

    public void _GameplaySetup()
    {
        //if (MapManager.instance.spawnedMap.loadMapDone == true && gameplaySet == false)
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
        if (win == false && lose == false && gameplaySet && GameController.instance.isPlaying && PlayerManager.instance.spawnedPlayer != null && PlayerManager.instance.spawnedPlayer.setDefault == false)
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
            }
        }
    }
}
