using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public MonsterController spawnedMonster;
    public PlayerController spawnedPlayer;
    public List<PlayerAIController> spawnedAIPlayers = new List<PlayerAIController>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {

    }
}
