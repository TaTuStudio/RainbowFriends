using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeederCollector : MonoBehaviour
{
    public List<Transform> collectPositions = new List<Transform>();

    List<Transform> positionLeftList = new List<Transform>();

    public List<CollectFeederItem> collectedItems = new List<CollectFeederItem>();

    private void Start()
    {
        positionLeftList.Clear();

        positionLeftList.AddRange(collectPositions);

        collectedItems.Clear();
    }

    private void Update()
    {
        if (FeederCollectMissionController.instance.gameplaySet == false) return;
        GameplayUI.instance.foodCollectCountUI._SetCount(collectedItems.Count, FeederCollectMissionController.instance.collectItemSpawner.spawnedItems.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        PlayerAIController playerAIController = other.GetComponent<PlayerAIController>();

        if (playerController != null)
        {
            List<Transform> tempList = new List<Transform>();

            tempList.AddRange(playerController.rightHandCollectedList);

            while (tempList.Count > 0)
            {
                CollectFeederItem collectItem = tempList[0].GetComponent<CollectFeederItem>();

                if (collectItem != null)
                {
                    collectedItems.Add(collectItem);

                    collectItem.transform.position = positionLeftList[0].position;
                    collectItem.transform.rotation = positionLeftList[0].rotation;
                    collectItem.transform.parent = transform;

                    collectItem._SetCollected();

                    FeederCollectMissionController.instance.collectItemSpawner._AddToCollected(tempList[0].GetComponent<ReuseGO>());

                    positionLeftList.RemoveAt(0);

                    playerController._RemoveRightHandColectItem(tempList[0]);

                    collectItem._ResetOnHandPlayer();
                }

                tempList.RemoveAt(0);
            }
        }
        else if (playerAIController != null)
        {
            List<Transform> tempList = new List<Transform>();

            tempList.AddRange(playerAIController.rightHandCollectedList);

            while (tempList.Count > 0)
            {
                CollectFeederItem collectItem = tempList[0].GetComponent<CollectFeederItem>();

                if (collectItem != null)
                {
                    collectedItems.Add(collectItem);

                    collectItem.transform.position = positionLeftList[0].position;
                    collectItem.transform.rotation = positionLeftList[0].rotation;
                    collectItem.transform.parent = transform;

                    collectItem._SetCollected();

                    FeederCollectMissionController.instance.collectItemSpawner._AddToCollected(tempList[0].GetComponent<ReuseGO>());

                    positionLeftList.RemoveAt(0);

                    playerAIController._RemoveRightHandColectItem(tempList[0]);

                    collectItem._ResetOnHandPlayer();
                }

                tempList.RemoveAt(0);
            }
        }
    }
}
