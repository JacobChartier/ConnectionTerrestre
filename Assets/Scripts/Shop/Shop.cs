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
    [SerializeField] private string text;

    public Inventory inventory;

    [Header("Shop")]
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private CinemachineVirtualCamera playerVCAM;
    [SerializeField] private CinemachineVirtualCamera shopVCAM;

    private void Start()
    {
        if (Instance == null)
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
        shopMenu.GetComponent<ShopUI>().Show();

        CameraController.Instance?.EnableFreeCameraMovement(false);
        CameraController.Instance?.SetCamera(shopVCAM);

        CameraController.Instance?.FreezeCamera(true);

        inventoryMenu.gameObject.transform.position = new Vector3((Camera.main.scaledPixelWidth / 2) - 155, inventoryMenu.transform.position.y, inventoryMenu.transform.position.z);
        inventoryMenu.GetComponent<InventoryUI>().Show();
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
