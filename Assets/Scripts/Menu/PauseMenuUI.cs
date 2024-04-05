using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MenuHandler
{
    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        CameraManager.Instance.FreezeCamera(true);

        InputManager.controls.Player.Disable();
        InputManager.controls.Menus.Enable();
    }

    public void Hide()
    {
        Time.timeScale = 1;
        InputManager.controls.Player.Enable();
        InputManager.controls.Menus.Disable();

        CameraManager.Instance.FreezeCamera(false);
        gameObject.SetActive(false);
    }
}
