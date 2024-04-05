using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Show()
    {
        CameraManager.Instance.FreezeCamera(true);
    }

    private void OnDisable()
    {
        CameraManager.Instance.FreezeCamera(false);
    }
}
