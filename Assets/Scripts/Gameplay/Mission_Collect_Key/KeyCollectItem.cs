using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPOOutline;

public class KeyCollectItem : MonoBehaviour
{
    Outlinable outlinable;

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

        _ActiveOutLine(true);
    }

    void _ActiveOutLine(bool active)
    {
        if (outlinable == null)
        {
            outlinable = GetComponent<Outlinable>();
        }

        if (outlinable != null)
        {
            outlinable.enabled = active;
        }
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
            if (onHandAIPlayer != null && onHandAIPlayer.isDead)
            {
                transform.position = onHandAIPlayer.transform.position;

                transform.parent = KeyCollectMissionController.instance.collectItemSpawner.transform;

                onHandAIPlayer._RemoveRightHandColectItem(transform);

                _OnGroundSet();
            }
            else
            if (onHandPlayer != null && onHandPlayer.isDead)
            {
                transform.position = onHandPlayer.transform.position;

                transform.parent = KeyCollectMissionController.instance.collectItemSpawner.transform;

                onHandPlayer._RemoveRightHandColectItem(transform);

                _OnGroundSet();
            }
        }
    }

    public void _SetCollected()
    {
        collected = true;
        _ActiveMesh(true);

        _ActiveOutLine(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (onHand == false)
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
                    KeyCollectItem otherItem = go.GetComponent<KeyCollectItem>();

                    if (otherItem != null)
                    {
                        otherItem._ActiveMesh(false);
                    }
                }

                playerController._AddRightHandColectItem(transform);

                onHandPlayer = playerController;

                _ActiveOutLine(false);
            }
            else if (playerAIController != null && playerAIController.isDead == false && playerAIController.catched == false)
            {
                onHand = true;

                transform.parent = playerAIController.OnHandItemContainer_Right.transform;

                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;

                foreach (Transform go in playerAIController.rightHandCollectedList)
                {
                    KeyCollectItem otherItem = go.GetComponent<KeyCollectItem>();

                    if (otherItem != null)
                    {
                        otherItem._ActiveMesh(false);
                    }
                }

                playerAIController._AddRightHandColectItem(transform);

                onHandAIPlayer = playerAIController;

                _ActiveOutLine(false);
            }
        }
    }
}
