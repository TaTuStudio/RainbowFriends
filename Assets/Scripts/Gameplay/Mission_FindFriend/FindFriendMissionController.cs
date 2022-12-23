using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFriendMissionController : MonoBehaviour
{
    public static FindFriendMissionController instance;

    public PlayerAIBrain_FindFriend_Controller playerAIBrain_FindFriend_Controller;

    public FindFriendSafeZone findFriendSafeZone;

    public bool gameplaySet = false;

    public bool win = false;
    public bool lose = false;

    int aiSpawnNum = 9;

    public PlayerAIController friendPrefab;

    public PlayerAIController spawnedFriend;

    public List<Transform> lostFriendSpawnpoints;

    private void Awake()
    {
        _MakeReplaceSingleton();
    }

    // Start is called before the first frame update
    private void Start()
    {
        GameController.instance._GameplayReadySetup();

        GameplayUI.instance._GameplayFindFriendSetup();
    }

    // Update is called once per frame
    void Update()
    {
        _GameplaySetup();

        _WinLoseCheck();
    }

    void _MakeReplaceSingleton()
    {
        if (FindFriendMissionController.instance != null && FindFriendMissionController.instance != this)
        {
            FindFriendMissionController old = FindFriendMissionController.instance;

            FindFriendMissionController.instance = this;

            Destroy(old);
        }
        else
        {
            FindFriendMissionController.instance = this;
        }
    }

    public void _GameplaySetup()
    {
        if (MapManager.instance.spawnedMap.loadMapDone == true && gameplaySet == false)
        {
            gameplaySet = true;

            _SpawnLostFriend();

            PlayerManager.instance._SpawnPlayerAndAIPlayers(aiSpawnNum);

            MonsterSpawner.instance._SpawnAllMonsters();
            MonsterStaticScareSpawner.instance._SpawnAllMonsters();

            playerAIBrain_FindFriend_Controller._SetBrainToAIPlayer();

            GameController.instance._SetGameTime(60f);

            GameController.instance._SetPlaying(true);
        }
    }

    void _WinLoseCheck()
    {
        if (win == false && lose == false && gameplaySet == true && GameController.instance.isPlaying == true && PlayerManager.instance.spawnedPlayer != null && PlayerManager.instance.spawnedPlayer.setDefault == false)
        {
            if (findFriendSafeZone.friendFound != null)
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

    public void _SpawnLostFriend()
    {
        int ranIndex = Random.Range(0, lostFriendSpawnpoints.Count);

        spawnedFriend = Instantiate(friendPrefab, lostFriendSpawnpoints[ranIndex].position, Quaternion.identity, transform);
    }
}
