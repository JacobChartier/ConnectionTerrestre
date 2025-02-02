using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject shopMenu;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        inventoryMenu = FindObjectOfType<InventoryUI>(true).gameObject;
        shopMenu = FindObjectOfType<ShopUI>(true).gameObject;
    }

    public static PlayersControls controls;

    public static Vector2 mouvementInput;
    public static Vector2 rotationInput;
    public static bool jumpInput;
    public static float scrollWheelInput;
    [field: ReadOnly] public float scrollWheelInput_t;

    private void Awake()
    {
        inventoryMenu = FindObjectOfType<InventoryUI>(true).gameObject;
        shopMenu = FindObjectOfType<ShopUI>(true).gameObject;

        controls = new PlayersControls();
        controls.Player.Enable();
    }

    private void Update()
    {
    }

    public static void EnableMenuInputs(bool value)
    {
        if (value)
        {
            controls.Player.Disable();
            controls.Menus.Enable();
        }
        else
        {
            controls.Player.Enable();
            controls.Menus.Disable();
        }
    }

    private void OnEnable()
    {
        controls.Player.Movement.performed += ctx => mouvementInput = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => mouvementInput = ctx.ReadValue<Vector2>();

        controls.Player.Rotation.performed += ctx => rotationInput = ctx.ReadValue<Vector2>();
        controls.Player.Rotation.canceled += ctx => rotationInput = ctx.ReadValue<Vector2>();

        controls.Player.Sauter.performed += ctx => jumpInput = ctx.ReadValueAsButton();
        controls.Player.Sauter.canceled += ctx => jumpInput = ctx.ReadValueAsButton();

        controls.Player.HotbarSelection.performed += ctx => scrollWheelInput = ctx.ReadValue<float>();
        controls.Player.HotbarSelection.canceled += ctx => scrollWheelInput = ctx.ReadValue<float>();

        controls.Player.HotbarSelection.performed += ctx => scrollWheelInput_t = ctx.ReadValue<float>();
        controls.Player.HotbarSelection.canceled += ctx => scrollWheelInput_t = ctx.ReadValue<float>();

        controls.Player.OpenInventory.performed += OpenInventory;

        controls.Menus.Close.performed += HideMenus;

    }

    private void OpenInventory(InputAction.CallbackContext ctx)
    {
        if (MenuHandler.GetMenu<PauseMenuUI>().isActiveAndEnabled)
            return;

        MenuHandler.GetMenu<InventoryUI>().Show();
    }

    private void HideMenus(InputAction.CallbackContext ctx)
    {
        if (MenuHandler.GetMenu<PauseMenuUI>().isActiveAndEnabled)
        {
            MenuHandler.GetMenu<PauseMenuUI>().Hide();
        }
        else
        {
            MenuHandler.GetMenu<ShopUI>().Hide();

            if (!MenuHandler.GetMenu<InventoryUI>().isActiveAndEnabled) return;

            InventoryLoader.Save(Player.Instance.inventory);

            MenuHandler.GetMenu<InventoryUI>().Hide();
        }
    }
}
