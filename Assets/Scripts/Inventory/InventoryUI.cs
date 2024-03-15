using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MenuHandler
{
    [SerializeField] private GameObject shopUI;

    [SerializeField] private EntityStats player;

    [SerializeField] private TMP_Text coins;

    private void Awake()
    {
        GetMenu<InventoryUI>().Refresh();

        Refresh(); 
    }

    public void Refresh()
    {
        this.coins.text = player.Coins.Current.ToString();
    }

    public void Show()
    {
        InputManager.controls?.Player.Disable();
        InputManager.controls?.Menus.Enable();

        this.transform.gameObject.SetActive(true);

        CameraManager.Instance?.FreezeCamera(true);
        CameraManager.Instance?.EnableFreeCameraMovement(false);

        HeadUpDisplay.Instance?.Hide();
    }

    public void Hide()
    {
        InputManager.controls?.Player.Enable();
        InputManager.controls?.Menus.Disable();

        CameraManager.Instance?.FreezeCamera(false);
        CameraManager.Instance?.ResetCameras();
        CameraManager.Instance?.EnableFreeCameraMovement(true);

        Tooltip.Instance?.Hide();

        HeadUpDisplay.Instance?.Show();

        this.gameObject.transform.position = new Vector3((Camera.main.scaledPixelWidth / 2), this.transform.position.y, this.transform.position.z);
        this.transform.gameObject.SetActive(false);
    }
}
