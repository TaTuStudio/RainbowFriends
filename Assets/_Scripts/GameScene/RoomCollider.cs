using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class RoomCollider : MonoBehaviour
{
    private int roomNumber;
    private PlayerPosSO playerPos;

    private void OnEnable()
    {
        Addressables.LoadAssetAsync<PlayerPosSO>("NewPlayerPos").Completed += (_) => playerPos = _.Result;

        roomNumber = int.Parse(Regex.Match(gameObject.name, @"\d+").Value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerPos.Pos = roomNumber;
    }
}

