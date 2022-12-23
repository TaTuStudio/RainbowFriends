using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerAIBrain_Collect : MonoBehaviour
{
    public PlayerAIBrains_CollectController playerAIBrains_CollectController;

    public PlayerAIController playerAIController;

    [Header("Turn settings")]

    public float turnTime = 0f;

    public int turnType = -1;
    // turnType = 0 -> stay
    // turnType = 1 -> move to one of Collect item position
    // turnType = 2 -> move to Random pos in range in a time

    public int lastItemOnhand = 0;

    private float randomRange = 10f;
    public ReuseGO selectedItem;

    public bool defaultSet = false;

    private void OnEnable()
    {
        defaultSet = true;
    }

    // Update is called once per frame
    void Update()
    {
        _Default();

        _CheckTurn();

        _CheckHide();
    }

    void _Default()
    {
        if (defaultSet == false) return;

        defaultSet = false;

        turnTime = 0f;

        turnType = -1;

        selectedItem = null;
        lastItemOnhand = 0;
    }

    void _CheckTurn()
    {
        if (AlphabetCollectMissionController.instance.gameplaySet == false || playerAIController.catched || playerAIController.isDead)
            return;

        _CheckRightHandItemsChange();

        if (turnType == -1)
        {
            _GetTurn();
        }

        if (turnType == 0 || turnType == 2)
        {
            if(turnTime >= 0f)
            {
                turnTime -= Time.deltaTime;

                if(turnTime < 0f)
                {
                    _GetTurn();
                }
            }
        }

        if (turnType == 1)
        {
            if(selectedItem == null)
            {
                _GetTurn();
            }
            else
            {
                if (AlphabetCollectMissionController.instance.collectItemSpawner.collectedItems.Contains(selectedItem))
                {
                    selectedItem = null;
                }
                else
                {
                    playerAIController.aIPath._SetMoveToPosition(selectedItem.transform.position);
                }
            }
        }

        if (playerAIController.rightHandCollectedList.Count > 0)
        {
            playerAIController.aIPath._SetMoveToPosition(AlphabetCollectMissionController.instance.collector.position);
        }
    }

    void _GetTurn()
    {
        List<int> ranNumList = new List<int>() { 0, 2 };

        if(AlphabetCollectMissionController.instance.collectItemSpawner.collectedItems.Count < AlphabetCollectMissionController.instance.collectItemSpawner.spawnedItems.Count)
        {
            ranNumList.Add(1);
        }

        turnType = Random.Range(0, ranNumList.Count);

        if (turnType == 0)
        {
            turnTime = (float)Random.RandomRange(1, 3);

            playerAIController.aIPath._SetMoveToPosition(playerAIController.transform.position);

            //Debug.Log("Player AI Stay");
        }
        else if (turnType == 1)
        {
            selectedItem = null;

            List<ReuseGO> tempList = new List<ReuseGO>();

            tempList.AddRange(AlphabetCollectMissionController.instance.collectItemSpawner.spawnedItems);

            foreach (ReuseGO reuseGO in CollectionMarshal.AsSpan(AlphabetCollectMissionController.instance.collectItemSpawner.collectedItems))
            {
                if (tempList.Contains(reuseGO) || _CheckCanCollectItem(reuseGO) == false)
                {
                    tempList.Remove(reuseGO);
                }
            }

            if(tempList.Count > 0)
            {
                int ranItemIndex = Random.Range(0, tempList.Count);

                selectedItem = tempList[ranItemIndex];
            }

        }
        else if (turnType == 2)
        {
            turnTime = (float)Random.RandomRange(3, 5);

            Vector3 ranPos = new Vector3((float)Random.RandomRange(-randomRange, randomRange), playerAIController.transform.position.y, (float)Random.RandomRange(-randomRange, randomRange));

            ranPos = transform.position + ranPos;

            //var info = AstarPath.active.GetNearest(ranPos);
            //var node = info.node;
            //Vector3 closestPoint = info.position;

            //Debug.Log("ranPos = " + ranPos);

            playerAIController.aIPath._SetMoveToPosition(ranPos);

            //Debug.Log("Player AI move to random position");
        }
    }

    bool _CheckCanCollectItem(ReuseGO checkGO)
    {
        foreach (PlayerAIBrain_Collect brain in CollectionMarshal.AsSpan(playerAIBrains_CollectController.playerAIBrain_Collects))
        {
            List<ReuseGO> onHandItems = new List<ReuseGO>();

            foreach (Transform t in CollectionMarshal.AsSpan(playerAIController.rightHandCollectedList))
            {
                ReuseGO go = t.GetComponent<ReuseGO>();

                if (go != null)
                {
                    onHandItems.Add(go);
                }
            }

            if (onHandItems.Contains(checkGO))
            {
                Debug.Log("check go " + checkGO + " on hand " + brain.name);
            }

            if (brain != this && brain.selectedItem != null && brain.selectedItem == checkGO || onHandItems.Contains(checkGO))
            {
                return false;
            }
        }

        return true;
    }

    void _CheckRightHandItemsChange()
    {
        if(lastItemOnhand != playerAIController.rightHandCollectedList.Count)
        {
            lastItemOnhand = playerAIController.rightHandCollectedList.Count;

            _SetOtherBrainAvoidOnHandItem();
        }
    }

    void _SetOtherBrainAvoidOnHandItem()
    {
        if (selectedItem == null || playerAIController.rightHandCollectedList.Count == 0) return;
        
        List<ReuseGO> onHandItems = new List<ReuseGO>();

        foreach (Transform t in CollectionMarshal.AsSpan(playerAIController.rightHandCollectedList))
        {
            ReuseGO go = t.GetComponent<ReuseGO>();

            if (go != null)
            {
                onHandItems.Add(go);
            }
        }

        foreach (PlayerAIBrain_Collect brain in CollectionMarshal.AsSpan(playerAIBrains_CollectController.playerAIBrain_Collects))
        {
            if (brain != this && brain.selectedItem != null && onHandItems.Contains(brain.selectedItem))
            {
                //Debug.Log(brain.name + " got same item " + brain.selectedItem.itemID);

                brain.selectedItem = null;
            }
        }
    }

    float hideDelay = 0f;
    void _CheckHide()
    {
        if (AlphabetCollectMissionController.instance.gameplaySet == false || playerAIController.catched || playerAIController.isDead)
            return;

        if (hideDelay >= 0f)
        {
            hideDelay -= Time.deltaTime;

            if(hideDelay < 0f)
            {
                if (playerAIController.isHiding)
                {
                    hideDelay = (float)Random.Range(5, 15);
                }
                else
                {
                    hideDelay = (float)Random.Range(5, 10);
                }

                playerAIController._SetHide();
            }
        }
    }
}
