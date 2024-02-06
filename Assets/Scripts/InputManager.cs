using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayersControls controls;

    public static Vector2 mouvementInput;

    public static bool isAimingInput = false;

    // Start is called before the first frame update
    private void Awake()
    {
        controls = new PlayersControls();
        controls.Player.Enable();
    }

    private void OnEnable()
    {
        controls.Player.Movement.performed += Move;
        controls.Player.Movement.canceled += Move;


    }

    private void OnDisable()
    {
        controls.Player.Movement.performed -= Move;
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        mouvementInput = ctx.ReadValue<Vector2>();
        Debug.Log(mouvementInput);
    }


}
