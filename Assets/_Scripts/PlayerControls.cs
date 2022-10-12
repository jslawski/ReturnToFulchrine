//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.3
//     from Assets/_Scripts/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""33e2501e-fbf7-4277-ace9-fb94ba43730e"",
            ""actions"": [
                {
                    ""name"": ""LeftAnalog"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7ea656b2-8236-49c7-811f-bcccd5661c69"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightAnalog"",
                    ""type"": ""Value"",
                    ""id"": ""3a43c333-b63f-44f0-acfa-df4534540c63"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LeftBumper"",
                    ""type"": ""Button"",
                    ""id"": ""a333d4a6-7709-46a0-b7cb-15e8e281ccea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightBumper"",
                    ""type"": ""Button"",
                    ""id"": ""e154e7ec-6ce1-42cd-bbf3-b614801a3cd0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SouthButton"",
                    ""type"": ""Button"",
                    ""id"": ""28a85c18-a784-4fc6-9f73-f61c80860729"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""WestButton"",
                    ""type"": ""Button"",
                    ""id"": ""6878f65a-67dc-4519-86ac-f34b93efdc48"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fb69c976-091e-4863-a3bf-64255e0f956a"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftAnalog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49c07e27-baef-4682-9ccd-92604924ea4d"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightAnalog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f5aef3d-3652-4e81-8287-0e677c2cc4a9"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftBumper"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74381db0-2152-4fe9-896b-6f9811d7d186"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightBumper"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be221a57-a860-41b7-b34d-36fff4d8dbdc"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SouthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e4f67e3-b93a-4998-8bb5-8c4f7e7b8776"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WestButton"",
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
        m_Player_LeftAnalog = m_Player.FindAction("LeftAnalog", throwIfNotFound: true);
        m_Player_RightAnalog = m_Player.FindAction("RightAnalog", throwIfNotFound: true);
        m_Player_LeftBumper = m_Player.FindAction("LeftBumper", throwIfNotFound: true);
        m_Player_RightBumper = m_Player.FindAction("RightBumper", throwIfNotFound: true);
        m_Player_SouthButton = m_Player.FindAction("SouthButton", throwIfNotFound: true);
        m_Player_WestButton = m_Player.FindAction("WestButton", throwIfNotFound: true);
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
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_LeftAnalog;
    private readonly InputAction m_Player_RightAnalog;
    private readonly InputAction m_Player_LeftBumper;
    private readonly InputAction m_Player_RightBumper;
    private readonly InputAction m_Player_SouthButton;
    private readonly InputAction m_Player_WestButton;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftAnalog => m_Wrapper.m_Player_LeftAnalog;
        public InputAction @RightAnalog => m_Wrapper.m_Player_RightAnalog;
        public InputAction @LeftBumper => m_Wrapper.m_Player_LeftBumper;
        public InputAction @RightBumper => m_Wrapper.m_Player_RightBumper;
        public InputAction @SouthButton => m_Wrapper.m_Player_SouthButton;
        public InputAction @WestButton => m_Wrapper.m_Player_WestButton;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @LeftAnalog.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftAnalog;
                @LeftAnalog.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftAnalog;
                @LeftAnalog.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftAnalog;
                @RightAnalog.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightAnalog;
                @RightAnalog.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightAnalog;
                @RightAnalog.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightAnalog;
                @LeftBumper.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftBumper;
                @LeftBumper.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftBumper;
                @LeftBumper.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftBumper;
                @RightBumper.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightBumper;
                @RightBumper.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightBumper;
                @RightBumper.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightBumper;
                @SouthButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSouthButton;
                @SouthButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSouthButton;
                @SouthButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSouthButton;
                @WestButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWestButton;
                @WestButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWestButton;
                @WestButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWestButton;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftAnalog.started += instance.OnLeftAnalog;
                @LeftAnalog.performed += instance.OnLeftAnalog;
                @LeftAnalog.canceled += instance.OnLeftAnalog;
                @RightAnalog.started += instance.OnRightAnalog;
                @RightAnalog.performed += instance.OnRightAnalog;
                @RightAnalog.canceled += instance.OnRightAnalog;
                @LeftBumper.started += instance.OnLeftBumper;
                @LeftBumper.performed += instance.OnLeftBumper;
                @LeftBumper.canceled += instance.OnLeftBumper;
                @RightBumper.started += instance.OnRightBumper;
                @RightBumper.performed += instance.OnRightBumper;
                @RightBumper.canceled += instance.OnRightBumper;
                @SouthButton.started += instance.OnSouthButton;
                @SouthButton.performed += instance.OnSouthButton;
                @SouthButton.canceled += instance.OnSouthButton;
                @WestButton.started += instance.OnWestButton;
                @WestButton.performed += instance.OnWestButton;
                @WestButton.canceled += instance.OnWestButton;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnLeftAnalog(InputAction.CallbackContext context);
        void OnRightAnalog(InputAction.CallbackContext context);
        void OnLeftBumper(InputAction.CallbackContext context);
        void OnRightBumper(InputAction.CallbackContext context);
        void OnSouthButton(InputAction.CallbackContext context);
        void OnWestButton(InputAction.CallbackContext context);
    }
}
