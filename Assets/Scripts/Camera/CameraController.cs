using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] private CinemachineVirtualCamera playerVCam;
    [SerializeField] private List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        EnableFreeCameraMovement(true);
    }

    public void EnableFreeCameraMovement(bool isEnable)
    {
        if (isEnable)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void FreezeCamera(bool isFrozen)
    {
        if (isFrozen)
            playerVCam.GetComponent<CinemachineInputProvider>().enabled = false;
        else
            playerVCam.GetComponent<CinemachineInputProvider>().enabled = true;
    }

    public void SetCamera(CinemachineVirtualCamera vCam)
    {
        foreach (var camera in cameras)
            camera.Priority = 0;

        playerVCam.Priority = 0;

        vCam.Priority = 1;
    }

    public void ResetCameras()
    {
        playerVCam.Priority = 1;

        foreach (var camera in cameras)
            camera.Priority = 0;
    }
}
