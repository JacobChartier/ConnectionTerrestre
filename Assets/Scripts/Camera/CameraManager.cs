using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private CinemachineVirtualCamera playerVCam;
    public List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    public List<IMenuHandler> menus = new List<IMenuHandler>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        EnableFreeCameraMovement(true);
        FreezeCamera(false);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    public void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        playerVCam = GameObject.Find("vCam (1st Person View)").GetComponent<CinemachineVirtualCamera>();    // Set player vCam

        foreach (var cam in cameras)
            if (cam == null) cameras.Remove(cam);
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
