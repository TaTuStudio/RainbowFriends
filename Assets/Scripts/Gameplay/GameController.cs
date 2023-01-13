using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public bool isPlaying = false;
    public bool gameplaySetupDone = false;
    public bool resetWinLose = false;

    public float gameTime = 0f;
    public float curGameTime = 0f;

    public int totalPlayer = 0;
    public int curPlayer = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        GameplayUI.instance._HomeUISetup();
    }

    private void Update()
    {
        _TimeCount();
        _PlayerCount();
    }

    public void _SetPlaying(bool active)
    {
        isPlaying = active;
    }

    public void _SetGameplaySetupDone(bool active)
    {
        gameplaySetupDone = active;
    }

    public void _SetGameTime(float time)
    {
        gameTime = time;

        curGameTime = gameTime;

        GameplayUI.instance.timeCountUI._SetTime(curGameTime);
    }

    void _TimeCount()
    {
        if (isPlaying == false || gameplaySetupDone == false) return;

        if (curGameTime > 0f)
        {
            curGameTime -= Time.deltaTime;

            if (curGameTime <= 0f)
            {
                curGameTime = 0f;
            }

            GameplayUI.instance.timeCountUI._SetTime(curGameTime);
        }
    }

    public void _PlayerDeathCount()
    {
        curPlayer -= 1;

        if(curPlayer < 0)
        {
            curPlayer = 0;
        }
    }

    void _PlayerCount()
    {
        if (gameplaySetupDone == false) return;

        GameplayUI.instance.playerCountUI._SetCount(curPlayer, totalPlayer);
    }

    public void _GameplayReadySetup()
    {
        _SetPlaying(false);

        _SetGameplaySetupDone(false);

        totalPlayer = 0;

        curPlayer = 0;

        resetWinLose = false;

        PlayerManager.instance._Clean();
    }

    public void _GameplayBackToHome()
    {
        _GameplayReadySetup();

        MapManager.instance._UnloadScene();

        GameplayUI.instance._HomeUISetup();
    }

    public void _AddWinArchivement()
    {
        PlayerStats.MapArchivements archivement = PlayerStats.instance.mapArchivements.Find(item => item.mapSceneName == MapManager.instance.selectedMap);

        if (archivement != null) // check item isn't null
        {
            archivement.winCount += 1;
        }
        else
        {
            PlayerStats.MapArchivements newArchivement = new()
            {
                mapSceneName = MapManager.instance.selectedMap,
                winCount = 1
            };

            PlayerStats.instance.mapArchivements.Add(newArchivement);
        }

        PlayerStats.instance.save = true;
    }

    public void _AddLoseArchivement()
    {
        PlayerStats.MapArchivements archivement = PlayerStats.instance.mapArchivements.Find(item => item.mapSceneName == MapManager.instance.selectedMap);

        if (archivement != null) // check item isn't null
        {
            archivement.loseCount += 1;
        }
        else
        {
            PlayerStats.MapArchivements newArchivement = new PlayerStats.MapArchivements();

            newArchivement.mapSceneName = MapManager.instance.selectedMap;
            newArchivement.loseCount = 1;

            PlayerStats.instance.mapArchivements.Add(newArchivement);
        }

        PlayerStats.instance.save = true;
    }
}
