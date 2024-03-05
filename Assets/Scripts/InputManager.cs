using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    ShopUI shopUI = new ShopUI();

    private void Start()
    {
        if (Instance != null)
        {
            Instance = this;
        }
    }

    public static PlayersControls controls;

    public static Vector2 mouvementInput;
    public static Vector2 rotationInput;

    private void Awake()
    {
        controls = new PlayersControls();
        controls.Player.Enable();
    }

    private void OnEnable()
    {
        controls.Player.Movement.performed += ctx => mouvementInput = ctx.ReadValue<Vector2>(); 
        controls.Player.Movement.canceled += ctx => mouvementInput = ctx.ReadValue<Vector2>();

        controls.Player.Rotation.performed += ctx => rotationInput = ctx.ReadValue<Vector2>();
        controls.Player.Rotation.canceled += ctx => rotationInput = ctx.ReadValue<Vector2>();

        controls.Player.Closemenu.performed += shopUI.Hide;
    }
}
