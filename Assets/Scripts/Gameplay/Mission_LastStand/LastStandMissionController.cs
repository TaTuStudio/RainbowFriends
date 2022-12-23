using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStandMissionController : MonoBehaviour
{
    public static LastStandMissionController instance;

    public PlayerAIBrain_LastStand_Controller playerAIBrain_LastStand_Controller;

    public bool gameplaySet = false;

    public bool win = false;
    public bool lose = false;

    int aiSpawnNum = 9;

    private void Awake()
    {
        _MakeReplaceSingleton();
    }

    // Start is called before the first frame update
    private void Start()
    {
        GameController.instance._GameplayReadySetup();

        GameplayUI.instance._GameplayLastStandSetup();
    }

    // Update is called once per frame
    void Update()
    {
        _GameplaySetup();

        _WinLoseCheck();
    }

    void _MakeReplaceSingleton()
    {
        if (LastStandMissionController.instance != null && LastStandMissionController.instance != this)
        {
            LastStandMissionController old = LastStandMissionController.instance;

            LastStandMissionController.instance = this;

            Destroy(old);
        }
        else
        {
            LastStandMissionController.instance = this;
        }
    }

    public void _GameplaySetup()
    {
        if (MapManager.instance.spawnedMap.loadMapDone == true && gameplaySet == false)
        {
            gameplaySet = true;

            PlayerManager.instance._SpawnPlayerAndAIPlayers(aiSpawnNum);

            MonsterSpawner.instance._SpawnAllMonsters();
            MonsterStaticScareSpawner.instance._SpawnAllMonsters();

            playerAIBrain_LastStand_Controller._SetBrainToAIPlayer();

            GameController.instance._SetGameTime(60f);

            GameController.instance._SetPlaying(true);
        }
    }

    void _WinLoseCheck()
    {
        if (win == false && lose == false && gameplaySet == true && GameController.instance.isPlaying == true && PlayerManager.instance.spawnedPlayer != null && PlayerManager.instance.spawnedPlayer.setDefault == false)
        {
            if (GameController.instance.curGameTime >= GameController.instance.gameTime && PlayerManager.instance.spawnedPlayer.isDead == false)
            {
                win = true;

                GameplayUI.instance._ActiveWinUI(true);

                return;
            }

            if (PlayerManager.instance.spawnedPlayer.isDead)
            {
                lose = true;

                GameplayUI.instance._ActiveDeadUI(true);

                return;
            }
        }
    }
}
