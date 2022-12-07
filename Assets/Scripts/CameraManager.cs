using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public List<CinemachineVirtualCamera> registeredVirtualCameras = new List<CinemachineVirtualCamera>();

    private void Awake()
    {
        instance = this;
    }

    void _CheckNull()
    {
        registeredVirtualCameras.RemoveAll(item => item == null);
    }

    public void _GameplaySwitchCam(CinemachineVirtualCamera activeCam)
    {
        _CheckNull();

        //camera active = 1, camera inActive = 0

        foreach(CinemachineVirtualCamera c in registeredVirtualCameras)
        {
            if(c == activeCam)
            {
                c.Priority = 1;
            }
            else
            {
                c.Priority = 0;
            }
        }
    }

    public void _DeactiveCamera(CinemachineVirtualCamera cam)
    {
        cam.Priority = 0;
    }

    public void _RegisterVirtualCamera(CinemachineVirtualCamera cam)
    {
        if(registeredVirtualCameras.Contains(cam) == false)
        {
            registeredVirtualCameras.Add(cam);
        }
    }
}
