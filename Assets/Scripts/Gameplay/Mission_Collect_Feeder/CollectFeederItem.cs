using System.Runtime.InteropServices;
using EPOOutline;
using UnityEngine;

public class CollectFeederItem : MonoBehaviour
{
    Outlinable outlinable;

    public PlayerController onHandPlayer;
    public PlayerAIController onHandAIPlayer;

    public bool onHand;
    public bool collected;

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
        outlinable ??= GetComponent<Outlinable>();

        if (!ReferenceEquals(outlinable , null))
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
        if (!onHand) return;
        if (onHandAIPlayer is { isDead: true })
        {
            var _transform = transform;
                
            _transform.position = onHandAIPlayer.transform.position;

            _transform.parent = FeederCollectMissionController.instance.collectItemSpawner.transform;

            onHandAIPlayer._RemoveRightHandColectItem(_transform);

            _OnGroundSet();
        }
        else
        if (onHandPlayer is { isDead: true })
        {
            var _transform = transform;
                
            _transform.position = onHandPlayer.transform.position;

            _transform.parent = FeederCollectMissionController.instance.collectItemSpawner.transform;

            onHandPlayer._RemoveRightHandCollectItem(_transform);

            _OnGroundSet();
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
        if (onHand) return;
        PlayerController playerController = other.GetComponent<PlayerController>();

        PlayerAIController playerAIController = other.GetComponent<PlayerAIController>();

        if (playerController != null && playerController.isDead == false && playerController.catched == false)
        {
            onHand = true;

            var _transform = transform;
            _transform.parent = playerController.OnHandItemContainer_Right.transform;

            _transform.localPosition = Vector3.zero;
            _transform.localRotation = Quaternion.identity;

            foreach (Transform go in CollectionMarshal.AsSpan(playerController.rightHandCollectedList))
            {
                CollectFeederItem otherItem = go.GetComponent<CollectFeederItem>();

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

            var _transform = transform;
            _transform.parent = playerAIController.OnHandItemContainer_Right.transform;

            _transform.localPosition = Vector3.zero;
            _transform.localRotation = Quaternion.identity;

            foreach (Transform go in CollectionMarshal.AsSpan(playerAIController.rightHandCollectedList))
            {
                CollectFeederItem otherItem = go.GetComponent<CollectFeederItem>();

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
