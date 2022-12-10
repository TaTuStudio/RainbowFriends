using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMissionController : MonoBehaviour
{
    public bool gameplaySet = false;

    public ReuseGO blueMonsterPrefab;
    public Transform blueSpawnPoint;

    public int collectNum = 0;
    public int curCollectedNum = 0;

    public bool win = false;
    public bool lose = false;

    int aiSpawnNum = 9;

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

    public void _GameplaySetup()
    {
        if(MapManager.instance.spawnedMap.loadMapDone == true && gameplaySet == false)
        {
            gameplaySet = true;

            _SpawnPlayers();

            MonsterSpawner.instance._SpawnAllMonsters();

            GameController.instance._SetGameTime(60f);

            GameController.instance._SetPlaying(true);
        }
    }

    public void _AddCollectItemNum(int num)
    {
        curCollectedNum += num;

        GameplayUI.instance.alphabetCollectCountUI._SetCount(curCollectedNum, collectNum);
    }

    void _WinLoseCheck()
    {
        if(win == false && lose == false && gameplaySet == true && GameController.instance.isPlaying == true && PlayerManager.instance.spawnedPlayer != null && PlayerManager.instance.spawnedPlayer.setDefault == false)
        {
            if (curCollectedNum > 0 && curCollectedNum >= collectNum)
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
