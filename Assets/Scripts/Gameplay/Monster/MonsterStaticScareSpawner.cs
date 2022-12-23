using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStaticScareSpawner : MonoBehaviour
{
    public static MonsterStaticScareSpawner instance;

    public int spawnNum = 0;

    public List<ReuseGO> staticMonsterPrefabs = new List<ReuseGO>();

    public List<Transform> spawnPoints = new List<Transform>();

    private void Awake()
    {
        _MakeReplaceSingleton();
    }

    void _MakeReplaceSingleton()
    {
        if (MonsterStaticScareSpawner.instance != null && MonsterStaticScareSpawner.instance != this)
        {
            MonsterStaticScareSpawner old = MonsterStaticScareSpawner.instance;

            MonsterStaticScareSpawner.instance = this;

            Destroy(old);
        }
        else
        {
            MonsterStaticScareSpawner.instance = this;
        }
    }

    public void _SpawnAllMonsters()
    {
        List<Transform> canUseSpawnPoints = new List<Transform>();

        canUseSpawnPoints.AddRange(spawnPoints);

        for (int i = 0; i < spawnNum; i++)
        {
            if(canUseSpawnPoints.Count > 0)
            {
                int prefabIndex = Random.Range(0, staticMonsterPrefabs.Count);

                int spawnPosIndex = Random.Range(0, canUseSpawnPoints.Count);

                Transform spawnPos = canUseSpawnPoints[spawnPosIndex];

                ReuseGO spawnedMonster = PlayerManager.instance._SpawnMonster(staticMonsterPrefabs[prefabIndex], spawnPos.position);

                canUseSpawnPoints.Remove(spawnPos);
            }
        }
    }
}
