using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAlphabetItem : MonoBehaviour
{
    public PlayerController onHandPlayer;
    public PlayerAIController onHandAIPlayer;

    public bool onHand = false;
    public bool collected = false;

    public Transform meshContainer;

    private void OnEnable()
    {
        _OnGroundSet();
    }

    private void Update()
    {
        _CheckDropItem();
    }

    void _OnGroundSet()
    {
        onHand = false;
        collected = false;

        _ResetOnHandPlayer();

        _ActiveMesh(true);
    }

    public void _ResetOnHandPlayer()
    {
        onHandPlayer = null;
        onHandAIPlayer = null;
    }

    public void _ActiveMesh(bool active)
    {
        meshContainer.gameObject.SetActive(active);

        GetComponent<Collider>().enabled = active;
    }

    void _CheckDropItem()
    {
        if (onHand)
        {
            if(onHandAIPlayer != null && onHandAIPlayer.isDead)
            {
                transform.position = onHandAIPlayer.transform.position;

                transform.parent = AlphabetCollectMissionController.instance.collectItemSpawner.transform;

                _OnGroundSet();
            }
            else
            if (onHandPlayer != null && onHandPlayer.isDead)
            {
                transform.position = onHandPlayer.transform.position;

                transform.parent = AlphabetCollectMissionController.instance.collectItemSpawner.transform;

                _OnGroundSet();
            }
        }
    }

    public void _SetCollected()
    {
        collected = true;
        _ActiveMesh(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(onHand == false)
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            PlayerAIController playerAIController = other.GetComponent<PlayerAIController>();

            if (playerController != null && playerController.isDead == false && playerController.catched == false)
            {
                onHand = true;

                transform.parent = playerController.OnHandItemContainer_Right.transform;

                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;

                foreach (Transform go in playerController.rightHandCollectedList)
                {
                    CollectAlphabetItem otherBox = go.GetComponent<CollectAlphabetItem>();

                    if (otherBox != null)
                    {
                        otherBox._ActiveMesh(false);
                    }
                }

                playerController._AddRightHandColectItem(transform);

                onHandPlayer = playerController;
            }
            else if (playerAIController != null && playerAIController.isDead == false && playerAIController.catched == false)
            {
                onHand = true;

                transform.parent = playerAIController.OnHandItemContainer_Right.transform;

                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;

                foreach (Transform go in playerAIController.rightHandCollectedList)
                {
                    CollectAlphabetItem otherBox = go.GetComponent<CollectAlphabetItem>();

                    if (otherBox != null)
                    {
                        otherBox._ActiveMesh(false);
                    }
                }

                playerAIController._AddRightHandColectItem(transform);

                onHandAIPlayer = playerAIController;
            }
        }
    }
}