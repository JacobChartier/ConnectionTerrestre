using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour, IMenuHandler
{
    public static InventoryUI Instance;

    [SerializeField] private GameObject shopUI;

    [SerializeField] private EntityStats player;

    [SerializeField] private TMP_Text coins;

    private void Awake()
    {
        if (Instance != null)
            Instance = this;
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

        CameraController.Instance?.FreezeCamera(true);
        CameraController.Instance?.EnableFreeCameraMovement(false);
    }

    public void Hide()
    {
        InputManager.controls?.Player.Enable();
        InputManager.controls?.Menus.Disable();

        CameraController.Instance?.FreezeCamera(false);
        CameraController.Instance?.ResetCameras();
        CameraController.Instance?.EnableFreeCameraMovement(true);

        Tooltip.Instance?.Hide();

        this.gameObject.transform.position = new Vector3((Camera.main.scaledPixelWidth / 2), this.transform.position.y, this.transform.position.z);
        this.transform.gameObject.SetActive(false);
    }
}
