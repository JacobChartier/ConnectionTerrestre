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
            Destroy(gameObject);

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

    public static MouseButton mouseButtonInput;

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
        controls.Player.OpenInventory.canceled += OpenInventory;

        controls.Menus.Close.performed += HideMenus;
        controls.Menus.Close.canceled += HideMenus;

        controls.Menus.SplitStackInHalf.performed += ctx => mouseButtonInput = MouseButton.RIGHT;
        controls.Menus.TakeOneItemFromStack.performed += ctx => mouseButtonInput = MouseButton.MIDDLE;
    }

    private void OpenInventory(InputAction.CallbackContext ctx)
    {
        inventoryMenu.GetComponent<InventoryUI>()?.Show();
    }

    private void HideMenus(InputAction.CallbackContext ctx)
    {
        inventoryMenu.GetComponent<InventoryUI>()?.Hide();
        shopMenu.GetComponent<ShopUI>()?.Hide();
    }
}

public enum MouseButton
{
    LEFT,
    MIDDLE,
    RIGHT
}
