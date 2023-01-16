using UnityEngine;

public class RoomCollider : MonoBehaviour
{
    private int roomNumber;
    // private PlayerPosSO playerPos;
    // private AsyncOperationHandle<PlayerPosSO> handle;
    
    private void OnEnable()
    {
        // handle = Addressables.LoadAssetAsync<PlayerPosSO>("NewPlayerPos");
        // handle.Completed += (_) => playerPos = _.Result;

        // roomNumber = int.Parse(Regex.Match(gameObject.name, @"\d+").Value);
    }

    // private IEnumerator OnTriggerEnter(Collider other)
    // {
    //     yield return handle.WaitForCompletion();
    //     if (other.gameObject.CompareTag("Player"))
    //         playerPos.Pos = roomNumber;
    // }
}

