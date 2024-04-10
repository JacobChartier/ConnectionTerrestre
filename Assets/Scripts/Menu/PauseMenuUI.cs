using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MenuHandler
{
    public void Show()
    {
        gameObject.SetActive(true);
        CameraManager.Instance.FreezeCamera(true);
        CameraManager.Instance.EnableFreeCameraMovement(false);

        InputManager.controls.Player.Disable();
        InputManager.controls.Menus.Enable();
        Time.timeScale = 0;
    }

    public void Hide()
    {
        Time.timeScale = 1;
        InputManager.controls.Player.Enable();
        InputManager.controls.Menus.Disable();

        CameraManager.Instance.FreezeCamera(false);
        CameraManager.Instance.EnableFreeCameraMovement(true);
        gameObject.SetActive(false);
    }
}
