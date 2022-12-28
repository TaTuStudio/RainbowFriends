using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Pathfinding;

public class Env_HallWayDoor : MonoBehaviour
{
    [Tooltip("Rotate -90 degree")]
    public Transform[] doors1;

    [Tooltip("Rotate 90 degree")]
    public Transform[] doors2;

    float curOpenDoorDelay = 0f;

    private void OnEnable()
    {
        curOpenDoorDelay = GameplayUI.instance.startGameDelayUI._GetDelayTime();

        //doors.SetActive(true);
    }

    private void Update()
    {
        _OpenDoorDelay();
    }

    void _OpenDoorDelay()
    {
        if (curOpenDoorDelay >= 0f)
        {
            curOpenDoorDelay -= Time.deltaTime;

            if (curOpenDoorDelay < 0f)
            {
                foreach (var door in doors1)
                {
                    door.DOLocalRotate(new Vector3(0, 0, -90), 1f).OnComplete(() => _UpdateAStarGridLayer(door));
                }
                foreach (var door in doors2)
                {
                    door.DOLocalRotate(new Vector3(0, 0, 90), 1f).OnComplete(() => _UpdateAStarGridLayer(door));
                }
            }
        }
    }

    private void _UpdateAStarGridLayer(Transform door)
    {
        var guo = new GraphUpdateObject(door.GetComponent<Collider>().bounds);

        AstarPath.active.UpdateGraphs(guo);
    }
}
