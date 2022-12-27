using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeederCollectMissionController : MonoBehaviour
{
    public static FeederCollectMissionController instance;

    public PlayerAIBrains_Feeder_CollectController playerAIBrains_CollectController;

    public CollectItemSpawner collectItemSpawner;

    public Transform collector;

    public bool gameplaySet = false;

    public bool win = false;
    public bool lose = false;

    int aiSpawnNum = 9;

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
        _GameplaySetup();

        _ResetWinLose();

        _WinLoseCheck();
    }

    void _MakeReplaceSingleton()
    {
        if (FeederCollectMissionController.instance != null && FeederCollectMissionController.instance != this)
        {
            FeederCollectMissionController old = FeederCollectMissionController.instance;

            FeederCollectMissionController.instance = this;

            Destroy(old);
        }
        else
        {
            FeederCollectMissionController.instance = this;
        }
    }

    public void _GameplaySetup()
    {
        if (MapManager.instance.spawnedMap.loadMapDone == true && gameplaySet == false)
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
        if (win == false && lose == false && gameplaySet == true && GameController.instance.isPlaying == true && PlayerManager.instance.spawnedPlayer != null && PlayerManager.instance.spawnedPlayer.setDefault == false)
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
}
