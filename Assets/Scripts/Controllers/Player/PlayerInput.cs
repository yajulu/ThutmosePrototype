// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controllers/Player/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""CharacterActionMap"",
            ""id"": ""eca5a9df-3c7e-4a1f-87e4-30e459df5f1a"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""af317773-3381-4cbe-9a68-79860d04834b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""3be7fbf5-3723-4f58-8c4e-0662dcc72967"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TimeAbility"",
                    ""type"": ""Button"",
                    ""id"": ""885757ef-6980-4e0e-a391-5a98bc1ebece"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""81b12048-4f70-47b0-8a65-055d33da3f1e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cc91fced-5a14-473b-8e36-1b6bc8c343f8"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""93b1b3bc-48ff-4fa6-9abb-8b5fa6c787f8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""60466ce8-df97-46f9-b9f2-a58397407a80"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8fba28d7-06e4-40d7-8d34-6379000a0b20"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3ae063a5-5d83-49de-991d-9f1fc6780ea2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f1900a09-b4a9-4e65-8e21-dad2edb97d02"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9d9f2d6b-39aa-4b7c-8c1a-0ac3fd44d17e"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c36f24a-6b75-40dd-8909-9a4b7959232f"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8fde1eb-a729-43a9-aa9b-a30af853c609"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TimeAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c4dd167-30a0-449b-8606-ef9139e46ebc"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TimeAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dfe8304b-e119-4e7e-8872-8d22ee8fd354"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bbcf8020-a834-4a6d-8850-a41cd3b655fa"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterActionMap
        m_CharacterActionMap = asset.FindActionMap("CharacterActionMap", throwIfNotFound: true);
        m_CharacterActionMap_Move = m_CharacterActionMap.FindAction("Move", throwIfNotFound: true);
        m_CharacterActionMap_Run = m_CharacterActionMap.FindAction("Run", throwIfNotFound: true);
        m_CharacterActionMap_TimeAbility = m_CharacterActionMap.FindAction("TimeAbility", throwIfNotFound: true);
        m_CharacterActionMap_Dodge = m_CharacterActionMap.FindAction("Dodge", throwIfNotFound: true);
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

    // CharacterActionMap
    private readonly InputActionMap m_CharacterActionMap;
    private ICharacterActionMapActions m_CharacterActionMapActionsCallbackInterface;
    private readonly InputAction m_CharacterActionMap_Move;
    private readonly InputAction m_CharacterActionMap_Run;
    private readonly InputAction m_CharacterActionMap_TimeAbility;
    private readonly InputAction m_CharacterActionMap_Dodge;
    public struct CharacterActionMapActions
    {
        private @PlayerInput m_Wrapper;
        public CharacterActionMapActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_CharacterActionMap_Move;
        public InputAction @Run => m_Wrapper.m_CharacterActionMap_Run;
        public InputAction @TimeAbility => m_Wrapper.m_CharacterActionMap_TimeAbility;
        public InputAction @Dodge => m_Wrapper.m_CharacterActionMap_Dodge;
        public InputActionMap Get() { return m_Wrapper.m_CharacterActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterActionMapActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterActionMapActions instance)
        {
            if (m_Wrapper.m_CharacterActionMapActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CharacterActionMapActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CharacterActionMapActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CharacterActionMapActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_CharacterActionMapActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_CharacterActionMapActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_CharacterActionMapActionsCallbackInterface.OnRun;
                @TimeAbility.started -= m_Wrapper.m_CharacterActionMapActionsCallbackInterface.OnTimeAbility;
                @TimeAbility.performed -= m_Wrapper.m_CharacterActionMapActionsCallbackInterface.OnTimeAbility;
                @TimeAbility.canceled -= m_Wrapper.m_CharacterActionMapActionsCallbackInterface.OnTimeAbility;
                @Dodge.started -= m_Wrapper.m_CharacterActionMapActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_CharacterActionMapActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_CharacterActionMapActionsCallbackInterface.OnDodge;
            }
            m_Wrapper.m_CharacterActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @TimeAbility.started += instance.OnTimeAbility;
                @TimeAbility.performed += instance.OnTimeAbility;
                @TimeAbility.canceled += instance.OnTimeAbility;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
            }
        }
    }
    public CharacterActionMapActions @CharacterActionMap => new CharacterActionMapActions(this);
    public interface ICharacterActionMapActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnTimeAbility(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
    }
}