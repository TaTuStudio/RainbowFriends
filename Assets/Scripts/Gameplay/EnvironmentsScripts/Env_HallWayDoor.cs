using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Env_HallWayDoor : MonoBehaviour
{
    public GameObject doors;

    float curOpenDoorDelay = 0f;

    private void OnEnable()
    {
        curOpenDoorDelay = GameplayUI.instance.startGameDelayUI._GetDelayTime();

        doors.SetActive(true);
    }

    private void Update()
    {
        _OpenDoorDelay();
    }

    void _OpenDoorDelay()
    {
        if(curOpenDoorDelay >= 0f)
        {
            curOpenDoorDelay -= Time.deltaTime;

            if(curOpenDoorDelay < 0f)
            {
                doors.SetActive(false);
            }
        }
    }
}
