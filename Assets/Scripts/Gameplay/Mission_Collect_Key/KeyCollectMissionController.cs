using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectMissionController : MonoBehaviour
{
    public static KeyCollectMissionController instance;

    public PlayerAIBrains_Key_Collect_Controller playerAIBrains_CollectController;

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
        GameplayUI.instance._GameplayKeyCollectSetup();
    }

    private void Update()
    {
        _GameplaySetup();

        _WinLoseCheck();
    }

    void _MakeReplaceSingleton()
    {
        if (KeyCollectMissionController.instance != null && KeyCollectMissionController.instance != this)
        {
            KeyCollectMissionController old = KeyCollectMissionController.instance;

            KeyCollectMissionController.instance = this;

            Destroy(old);
        }
        else
        {
            KeyCollectMissionController.instance = this;
        }
    }

    public void _GameplaySetup()
    {
        if (MapManager.instance.spawnedMap.loadMapDone == true && gameplaySet == false)
        {
            gameplaySet = true;

            PlayerManager.instance._SpawnPlayerAndAIPlayers(aiSpawnNum);

            MonsterSpawner.instance._SpawnAllMonsters();

            playerAIBrains_CollectController._SetBrainToAIPlayer();

            GameController.instance._SetGameTime(60f);

            GameController.instance._SetPlaying(true);
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
