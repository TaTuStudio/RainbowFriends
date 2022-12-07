using System.Text.RegularExpressions;
using UnityEngine;

public class RoomCollider : MonoBehaviour
{
    private int roomNumber;
    [SerializeField] private PlayerPosSO playerPos;

    private void OnEnable()
    {
        roomNumber = int.Parse(Regex.Match(gameObject.name, @"\d+").Value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerPos.Pos = roomNumber;
    }
}
