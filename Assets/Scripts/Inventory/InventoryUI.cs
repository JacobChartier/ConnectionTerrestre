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

    private void OnEnable()
    {
        if (shopUI.activeInHierarchy)
        {
            transform.localPosition = new Vector3(-160, transform.localPosition.y, transform.localPosition.z);
        }
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

        Player.Instance.LoadInventory();
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

        this.gameObject.transform.localPosition = new Vector3(0, this.transform.localPosition.y, this.transform.localPosition.z);


        foreach (var slot in Player.Instance.inventory.slots)
        {
            if (slot.name.Contains("Hotbar Slot")) continue;
            Player.Instance.inventory.items.Remove(slot.GetItem());

            foreach (var item in slot.GetComponentsInChildren<Item>())
            {
                Player.Instance.inventory.Remove(item.GetComponent<Item>());
                ItemManager.Instance.items.Remove(item);
                Destroy(item.gameObject);
            }

            foreach (Transform item in GameObject.Find("Items").gameObject.transform)
            {
                Player.Instance.inventory.Remove(item.GetComponent<Item>());
                ItemManager.Instance.items.Remove(item.GetComponent<Item>());
                Destroy(item.gameObject);
            }
        }
        this.transform.gameObject.SetActive(false);
    }
}
