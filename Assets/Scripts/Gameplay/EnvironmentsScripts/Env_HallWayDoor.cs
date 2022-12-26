using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Env_HallWayDoor : MonoBehaviour
{
    public GameObject doors;

    float openDoorDelay = 6f;
    float curOpenDoorDelay = 0f;

    private void OnEnable()
    {
        curOpenDoorDelay = openDoorDelay;

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
