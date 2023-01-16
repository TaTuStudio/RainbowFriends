using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner instance;

    [Serializable]
    public class MonsterSpawnInfo
    {
        public ReuseGO monsterPrefab;

        [Header("Behavior Rate settings")]
        public bool avoidHide;
        public float moveToAnyWhereRate = 20f;
        public float moveToCheckPointRate = 40f;
        public float moveToPlayerRate = 40f;

        [Header("Check points Spawn Points settings")]
        public List<Transform> checkPoints = new();
        public List<Transform> spawnPoints = new();
    }

    public List<MonsterSpawnInfo> monsterSpawnInfos = new();

    private void Awake()
    {
        _MakeReplaceSingleton();
    }

    void _MakeReplaceSingleton()
    {
        if (instance != null && instance != this)
        {
            MonsterSpawner old = instance;

            instance = this;

            Destroy(old);
        }
        else
        {
            instance = this;
        }
    }

    public void _SpawnAllMonsters()
    {
        foreach (MonsterSpawnInfo m in CollectionMarshal.AsSpan(monsterSpawnInfos))
        {
            int spawnPosIndex = Random.Range(0, m.spawnPoints.Count);

            Vector3 spawnPos = m.spawnPoints[spawnPosIndex].position;

            ReuseGO spawnedMonster = PlayerManager.instance._SpawnMonster(m.monsterPrefab, spawnPos);

            MonsterController monsterController = spawnedMonster.GetComponent<MonsterController>();

            if(!ReferenceEquals(monsterController,null))
            {
                monsterController.monsterInfo = m;
            }

            ImpostorMonsterController impostorMonsterController = spawnedMonster.GetComponent<ImpostorMonsterController>();

            if (!ReferenceEquals(impostorMonsterController,null))
            {
                impostorMonsterController.monsterInfo = m;
            }
        }
    }
}
