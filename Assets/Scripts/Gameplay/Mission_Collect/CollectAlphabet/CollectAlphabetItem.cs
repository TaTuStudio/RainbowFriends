using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAlphabetItem : MonoBehaviour
{
    public bool onHand = false;
    public bool collected = false;

    public Transform meshContainer;

    private void OnEnable()
    {
        onHand = false;
        collected = false;

        _ActiveMesh(true);
    }

    public void _ActiveMesh(bool active)
    {
        meshContainer.gameObject.SetActive(active);

        GetComponent<Collider>().enabled = active;
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

            if (playerController != null)
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
            }
            else if (playerAIController != null)
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
            }
        }
    }
}
