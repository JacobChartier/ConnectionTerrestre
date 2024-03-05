using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    private void Awake()
    {
        Instance = this;

        DisableCursor();
    }

    public void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        this.gameObject.GetComponent<CinemachineInputProvider>().enabled = false;
    }

    public void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        this.gameObject.GetComponent<CinemachineInputProvider>().enabled = true;
    }
}
