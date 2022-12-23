using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner instance;

    [System.Serializable]
    public class MonsterSpawnInfo
    {
        public ReuseGO monsterPrefab;

        [Header("Behavior Rate settings")]
        public bool avoidHide = false;
        public float moveToAnyWhereRate = 20f;
        public float moveToCheckPointRate = 40f;
        public float moveToPlayerRate = 40f;

        [Header("Check points Spawn Points settings")]
        public List<Transform> checkPoints = new List<Transform>();
        public List<Transform> spawnPoints = new List<Transform>();
    }

    public List<MonsterSpawnInfo> monsterSpawnInfos = new List<MonsterSpawnInfo>();

    private void Awake()
    {
        _MakeReplaceSingleton();
    }

    void _MakeReplaceSingleton()
    {
        if (MonsterSpawner.instance != null && MonsterSpawner.instance != this)
        {
            MonsterSpawner old = MonsterSpawner.instance;

            MonsterSpawner.instance = this;

            Destroy(old);
        }
        else
        {
            MonsterSpawner.instance = this;
        }
    }

    public void _SpawnAllMonsters()
    {
        foreach (MonsterSpawnInfo m in monsterSpawnInfos)
        {
            int spawnPosIndex = Random.Range(0, m.spawnPoints.Count);

            Vector3 spawnPos = m.spawnPoints[spawnPosIndex].position;

            ReuseGO spawnedMonster = PlayerManager.instance._SpawnMonster(m.monsterPrefab, spawnPos);

            MonsterController monsterController = spawnedMonster.GetComponent<MonsterController>();

            if(monsterController != null)
            {
                monsterController.monsterInfo = m;
            }

            ImpostorMonsterController impostorMonsterController = spawnedMonster.GetComponent<ImpostorMonsterController>();

            if (impostorMonsterController != null)
            {
                impostorMonsterController.monsterInfo = m;
            }
        }
    }
}
