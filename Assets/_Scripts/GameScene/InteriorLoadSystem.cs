using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class InteriorLoadSystem : MonoBehaviour
{
    //private LinkedList<GameObject> rooms;
    private readonly GameObject[] hallWays = new GameObject[2];
    private int lastPos;
    private GameObject currentRoom;
    
    private PlayerPosSO playerPos;
    [SerializeField] private AssetReferenceGameObject[] room;
    [SerializeField] private AssetReferenceGameObject[] hallWay;
    [SerializeField] private SceneLoaderSO loadTestPlayer;

    
    // Start is called before the first frame update
    private void Start()
    {
        //rooms = new LinkedList<GameObject>();
    }

    private void OnEnable()
    {
        Addressables.LoadAssetAsync<PlayerPosSO>("NewPlayerPos").Completed += (_) =>
        {
            loadTestPlayer.LoadScene(true);
            playerPos = _.Result;
            playerPos.OnPosChange += GetPos;
        };

    }

    private void OnDisable()
    {
        playerPos.OnPosChange -= GetPos;
        Resources.UnloadUnusedAssets();
    }
    

    private void GetPos(int currentPosition)
    {
        print(currentPosition);
        
        InitHallWay(currentPosition);
        
        // Load current room
        if (currentRoom != null)
            room[lastPos].ReleaseInstance(currentRoom);
        
        room[currentPosition].InstantiateAsync().Completed += (_) => currentRoom = _.Result;
        lastPos = currentPosition;
        
        //CheckPos(currentPosition);
    }

    private void InitHallWay(int currentPosition)
    {
        switch (currentPosition)
        {
            case < 7:
            {
                if (hallWays[0] == null)
                    hallWay[0].InstantiateAsync().Completed += (_) => hallWays[0] = _.Result;
                if (hallWays[1] != null)
                    hallWay[1].ReleaseInstance(hallWays[1]);
                break;
            }
            case >= 7:
            {
                if (hallWays[1] == null)
                    hallWay[1].InstantiateAsync().Completed += (_) => hallWays[1] = _.Result;
                if (hallWays[0] != null)
                    hallWay[0].ReleaseInstance(hallWays[0]);
                break;
            }
        }
    }

    /*
    #region Load3RoomAtOnce
    
    private void CheckPos(int currentPosition)
    {
        if (lastPos == 0)
        {
            LoadRoomInit(currentPosition);
        }
        else
        {
            switch (currentPosition - lastPos)
            {
                case > 0:
                    LoadRoomForward(currentPosition);
                    break;
                case < 0:
                    LoadRoomBackward(currentPosition);
                    break;
                default:
                    return;
            }
        }

        lastPos = currentPosition;
    }

    private void LoadRoomInit(int pos)
    {
        if (pos > 0)
            room[pos-1].InstantiateAsync().Completed += (_) => rooms.AddLast(_.Result);
        
        room[pos].InstantiateAsync().Completed += (_) => rooms.AddLast(_.Result);
        
        if (pos < 13)
            room[pos+1].InstantiateAsync().Completed += (_) => rooms.AddLast(_.Result);
    }
    private void LoadRoomForward(int pos)
    {
        if (pos < 13)
            room[pos+1].InstantiateAsync().Completed += (_) => rooms.AddLast(_.Result);
        room[pos-2].ReleaseInstance(rooms.First());
        rooms.RemoveFirst();
    }

    private void LoadRoomBackward(int pos)
    {
        room[pos + 2].ReleaseInstance(rooms.Last());
        rooms.RemoveLast();
        if (pos > 0) 
            room[pos-1].InstantiateAsync().Completed += (_) => rooms.AddFirst(_.Result);
    }
    
    #endregion
    */
}
