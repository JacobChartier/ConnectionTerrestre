using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shop : InteractableObjectBase
{
    public static Shop Instance;

    [Header("Interaction label")]
    [SerializeField] private GameObject label;
    [SerializeField] private Sprite icon;
    [SerializeField] private string text;

    public Inventory inventory;

    [Header("Shop")]
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private CinemachineVirtualCamera playerVCAM;
    [SerializeField] private CinemachineVirtualCamera shopVCAM;

    private void Start()
    {
        if (Instance != null)
        {
            Instance = this;
        }

        for (int i = 0; i < inventory.slots.Length; i++)
        {
            var generatedItem = inventory.GenerateRandomItem();

            generatedItem.price = generatedItem.GeneratePrice(5, 15);
            inventory.Add(generatedItem);
        }
    }

    public override void Interact()
    {
        CameraController.Instance.EnableCursor();
        //EnableFreeCameraMovement(false);

        shopMenu.GetComponent<ShopUI>().Show();
    }

    private void OnEnable()
    {
        InputManager.controls.Player.Closemenu.performed += CloseMenu;
    }

    private void CloseMenu(InputAction.CallbackContext ctx)
    {
        CameraController.Instance.DisableCursor();
        //EnableFreeCameraMovement(true);

        //shopMenu.GetComponent<ShopUI>().Hide();
    }

    public override void EnableFreeCameraMovement(bool isEnable)
    {
        base.EnableFreeCameraMovement(isEnable);

        if (isEnable)
        {
            playerVCAM.Priority = 0;
            shopVCAM.Priority = 1;
        }
        else
        {
            playerVCAM.Priority = 1;
            shopVCAM.Priority = 0;
        }
    }

    public override void ShowContextLabel()
    {
        TMP_Text labelText = label.GetComponentInChildren<TMP_Text>();

        labelText.text = text;
        label.SetActive(true);
    }

    public override void HideContextLabel()
    {
        label.SetActive(false);
    }
}
