//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/PlayersControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayersControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayersControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayersControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""90fa2a1c-3b47-43aa-ad03-a1550fd31e78"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""0b6fd241-ce66-4eb7-8240-82067185043e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""55c351ed-5a82-42f1-94a8-7b3171a204bb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""selection gauche"",
                    ""type"": ""Button"",
                    ""id"": ""3971ae1a-913e-4d81-a005-c17fcf2b5535"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""selection droite"",
                    ""type"": ""Button"",
                    ""id"": ""4c8cb963-925c-4af4-84b7-4ec0ffae4c52"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""selection enter"",
                    ""type"": ""Button"",
                    ""id"": ""cb2fe07f-a27b-4bf9-96ca-f2d29ea62f35"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""selection haut"",
                    ""type"": ""Button"",
                    ""id"": ""ad2e550f-09d3-492c-809f-39099741041b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""selection bas"",
                    ""type"": ""Button"",
                    ""id"": ""a3ad6599-3379-4d33-86ab-c2f30a101df0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""97787cd8-091a-458a-96ad-558b9949c0c7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0d396841-6372-4025-9bdf-89b0f34db4c5"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ef0ff1f6-f378-4e9f-8518-e1ac1890cedb"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a32e6b58-0904-48c7-b117-5c07d5b0cb33"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b04e3aaf-9a09-456a-a63c-d8254aaa1cee"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a0a96e52-be18-4d34-a8cb-ffee5c822b9f"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57196761-441c-43dc-b346-28eb27710b22"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""selection gauche"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""586da016-9a6e-4d34-841d-8a89049b300c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""selection gauche"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b99a6489-f01c-4f73-bf44-267fed33ad5e"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""selection droite"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66e2a861-1e15-4c40-81aa-1f1cfa91cd1d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""selection droite"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0634661-6abb-42f5-949a-e871d8ac4103"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""selection enter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2191627a-7668-45a1-81a9-2768a67b6d97"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""selection haut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a91d085-0433-454a-9ffd-087f9cb11f7f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""selection haut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ada8a5f-55a6-4b62-b586-2e86b5184b4d"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""selection bas"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c041464d-0710-41c7-8548-9b6f13a075d4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""selection bas"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Inventory"",
            ""id"": ""bb7956ee-3856-4de0-83e9-a0e367dd4bd8"",
            ""actions"": [
                {
                    ""name"": ""SplitStackInHalf"",
                    ""type"": ""Button"",
                    ""id"": ""fce46460-18cc-469e-92a0-ae899acdabc4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TakeOneItemFromStack"",
                    ""type"": ""Button"",
                    ""id"": ""e84dc22c-a199-4516-a00e-e894273ae697"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b8bc3073-245e-44dc-8859-7027b27982fe"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SplitStackInHalf"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85a31798-7aef-41fc-a66d-d94c69443cc2"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TakeOneItemFromStack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Rotation = m_Player.FindAction("Rotation", throwIfNotFound: true);
        m_Player_selectiongauche = m_Player.FindAction("selection gauche", throwIfNotFound: true);
        m_Player_selectiondroite = m_Player.FindAction("selection droite", throwIfNotFound: true);
        m_Player_selectionenter = m_Player.FindAction("selection enter", throwIfNotFound: true);
        m_Player_selectionhaut = m_Player.FindAction("selection haut", throwIfNotFound: true);
        m_Player_selectionbas = m_Player.FindAction("selection bas", throwIfNotFound: true);
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_SplitStackInHalf = m_Inventory.FindAction("SplitStackInHalf", throwIfNotFound: true);
        m_Inventory_TakeOneItemFromStack = m_Inventory.FindAction("TakeOneItemFromStack", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Rotation;
    private readonly InputAction m_Player_selectiongauche;
    private readonly InputAction m_Player_selectiondroite;
    private readonly InputAction m_Player_selectionenter;
    private readonly InputAction m_Player_selectionhaut;
    private readonly InputAction m_Player_selectionbas;
    public struct PlayerActions
    {
        private @PlayersControls m_Wrapper;
        public PlayerActions(@PlayersControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Rotation => m_Wrapper.m_Player_Rotation;
        public InputAction @selectiongauche => m_Wrapper.m_Player_selectiongauche;
        public InputAction @selectiondroite => m_Wrapper.m_Player_selectiondroite;
        public InputAction @selectionenter => m_Wrapper.m_Player_selectionenter;
        public InputAction @selectionhaut => m_Wrapper.m_Player_selectionhaut;
        public InputAction @selectionbas => m_Wrapper.m_Player_selectionbas;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Rotation.started += instance.OnRotation;
            @Rotation.performed += instance.OnRotation;
            @Rotation.canceled += instance.OnRotation;
            @selectiongauche.started += instance.OnSelectiongauche;
            @selectiongauche.performed += instance.OnSelectiongauche;
            @selectiongauche.canceled += instance.OnSelectiongauche;
            @selectiondroite.started += instance.OnSelectiondroite;
            @selectiondroite.performed += instance.OnSelectiondroite;
            @selectiondroite.canceled += instance.OnSelectiondroite;
            @selectionenter.started += instance.OnSelectionenter;
            @selectionenter.performed += instance.OnSelectionenter;
            @selectionenter.canceled += instance.OnSelectionenter;
            @selectionhaut.started += instance.OnSelectionhaut;
            @selectionhaut.performed += instance.OnSelectionhaut;
            @selectionhaut.canceled += instance.OnSelectionhaut;
            @selectionbas.started += instance.OnSelectionbas;
            @selectionbas.performed += instance.OnSelectionbas;
            @selectionbas.canceled += instance.OnSelectionbas;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Rotation.started -= instance.OnRotation;
            @Rotation.performed -= instance.OnRotation;
            @Rotation.canceled -= instance.OnRotation;
            @selectiongauche.started -= instance.OnSelectiongauche;
            @selectiongauche.performed -= instance.OnSelectiongauche;
            @selectiongauche.canceled -= instance.OnSelectiongauche;
            @selectiondroite.started -= instance.OnSelectiondroite;
            @selectiondroite.performed -= instance.OnSelectiondroite;
            @selectiondroite.canceled -= instance.OnSelectiondroite;
            @selectionenter.started -= instance.OnSelectionenter;
            @selectionenter.performed -= instance.OnSelectionenter;
            @selectionenter.canceled -= instance.OnSelectionenter;
            @selectionhaut.started -= instance.OnSelectionhaut;
            @selectionhaut.performed -= instance.OnSelectionhaut;
            @selectionhaut.canceled -= instance.OnSelectionhaut;
            @selectionbas.started -= instance.OnSelectionbas;
            @selectionbas.performed -= instance.OnSelectionbas;
            @selectionbas.canceled -= instance.OnSelectionbas;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Inventory
    private readonly InputActionMap m_Inventory;
    private List<IInventoryActions> m_InventoryActionsCallbackInterfaces = new List<IInventoryActions>();
    private readonly InputAction m_Inventory_SplitStackInHalf;
    private readonly InputAction m_Inventory_TakeOneItemFromStack;
    public struct InventoryActions
    {
        private @PlayersControls m_Wrapper;
        public InventoryActions(@PlayersControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SplitStackInHalf => m_Wrapper.m_Inventory_SplitStackInHalf;
        public InputAction @TakeOneItemFromStack => m_Wrapper.m_Inventory_TakeOneItemFromStack;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void AddCallbacks(IInventoryActions instance)
        {
            if (instance == null || m_Wrapper.m_InventoryActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_InventoryActionsCallbackInterfaces.Add(instance);
            @SplitStackInHalf.started += instance.OnSplitStackInHalf;
            @SplitStackInHalf.performed += instance.OnSplitStackInHalf;
            @SplitStackInHalf.canceled += instance.OnSplitStackInHalf;
            @TakeOneItemFromStack.started += instance.OnTakeOneItemFromStack;
            @TakeOneItemFromStack.performed += instance.OnTakeOneItemFromStack;
            @TakeOneItemFromStack.canceled += instance.OnTakeOneItemFromStack;
        }

        private void UnregisterCallbacks(IInventoryActions instance)
        {
            @SplitStackInHalf.started -= instance.OnSplitStackInHalf;
            @SplitStackInHalf.performed -= instance.OnSplitStackInHalf;
            @SplitStackInHalf.canceled -= instance.OnSplitStackInHalf;
            @TakeOneItemFromStack.started -= instance.OnTakeOneItemFromStack;
            @TakeOneItemFromStack.performed -= instance.OnTakeOneItemFromStack;
            @TakeOneItemFromStack.canceled -= instance.OnTakeOneItemFromStack;
        }

        public void RemoveCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IInventoryActions instance)
        {
            foreach (var item in m_Wrapper.m_InventoryActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_InventoryActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
        void OnSelectiongauche(InputAction.CallbackContext context);
        void OnSelectiondroite(InputAction.CallbackContext context);
        void OnSelectionenter(InputAction.CallbackContext context);
        void OnSelectionhaut(InputAction.CallbackContext context);
        void OnSelectionbas(InputAction.CallbackContext context);
    }
    public interface IInventoryActions
    {
        void OnSplitStackInHalf(InputAction.CallbackContext context);
        void OnTakeOneItemFromStack(InputAction.CallbackContext context);
    }
}
