// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""DefaultMap"",
            ""id"": ""91701254-3be3-4473-bd82-7255747922e1"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""6c609b02-eeaf-4e17-ac5a-aeb8ed3508b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""51f5ebf4-417f-4b8d-b098-451de96cf095"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""a4c9ce8e-496a-4d82-b332-eabd6858b61a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Enter Key"",
                    ""type"": ""Button"",
                    ""id"": ""6d420b4f-1953-474d-b0e2-3a2493033763"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Escape Key"",
                    ""type"": ""Button"",
                    ""id"": ""e75c94af-2049-4532-93a4-87451de49d9f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Party Dialogue Key"",
                    ""type"": ""Button"",
                    ""id"": ""832c1633-597b-43f7-b15b-095fcf364a7b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Chat Log Key"",
                    ""type"": ""Button"",
                    ""id"": ""2734c0fd-92b9-4821-b197-95a9cb40cbdd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Split Item Stack"",
                    ""type"": ""Button"",
                    ""id"": ""ad7502da-2e4b-4cc2-b0b5-4b496f3ce079"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UI/LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""735404bc-0821-4882-99be-c03b369fd9f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UI/Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4ca5cfc3-6b6b-4f32-b371-1db56682f12b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""2e6bc784-a808-4209-a15d-b1d3e8f7266d"",
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
                    ""id"": ""45536c8e-1a49-4072-bbaa-a95d2921b34d"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4129b195-891c-4ff7-9fd4-531e4aec7354"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b48f331a-4d03-48b8-93dc-fcc57e8b6151"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a09041d6-e65d-4b6e-8345-990ff0785321"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""2a33995f-5b9e-4267-ba01-1e1005124021"",
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
                    ""id"": ""4ff0ee31-01d3-465b-bc55-dff8ca29eb6e"",
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
                    ""id"": ""76b312d4-127e-4eed-a192-c74d2ede7d12"",
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
                    ""id"": ""208a8a1f-d9f9-49ee-97d0-8b111e090d43"",
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
                    ""id"": ""7d22babf-89d2-4c34-92d6-b21b55548c1e"",
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
                    ""id"": ""505a3d4e-dfca-4ddb-add0-f61112ae57cc"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b49e376-4ced-4dc2-9150-ea0485a62324"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fcf49389-c036-41f1-b72d-0b55d17daa9d"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53c6530e-8cdc-439c-bc70-929588903406"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5dfcc5c6-6c85-4b6c-ab44-3d7ae71f86fa"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enter Key"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""395be9f3-8a68-4ac8-a3c6-b507d23f6b83"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape Key"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d97eb40-e54a-4afc-9c34-0f91322502bf"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Party Dialogue Key"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe4fc418-5641-4ae6-becb-df9b876cfd50"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Chat Log Key"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23bedeb8-569b-4472-82ef-efd29792cc4e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Split Item Stack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8739b7bd-10a3-46d9-9c9e-2af4660b3d9a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UI/LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6eaac8f1-201d-41fa-8d4e-fad0174ac342"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UI/Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // DefaultMap
        m_DefaultMap = asset.FindActionMap("DefaultMap", throwIfNotFound: true);
        m_DefaultMap_Movement = m_DefaultMap.FindAction("Movement", throwIfNotFound: true);
        m_DefaultMap_Interaction = m_DefaultMap.FindAction("Interaction", throwIfNotFound: true);
        m_DefaultMap_Cancel = m_DefaultMap.FindAction("Cancel", throwIfNotFound: true);
        m_DefaultMap_EnterKey = m_DefaultMap.FindAction("Enter Key", throwIfNotFound: true);
        m_DefaultMap_EscapeKey = m_DefaultMap.FindAction("Escape Key", throwIfNotFound: true);
        m_DefaultMap_PartyDialogueKey = m_DefaultMap.FindAction("Party Dialogue Key", throwIfNotFound: true);
        m_DefaultMap_ChatLogKey = m_DefaultMap.FindAction("Chat Log Key", throwIfNotFound: true);
        m_DefaultMap_SplitItemStack = m_DefaultMap.FindAction("Split Item Stack", throwIfNotFound: true);
        m_DefaultMap_UILeftClick = m_DefaultMap.FindAction("UI/LeftClick", throwIfNotFound: true);
        m_DefaultMap_UIPoint = m_DefaultMap.FindAction("UI/Point", throwIfNotFound: true);
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

    // DefaultMap
    private readonly InputActionMap m_DefaultMap;
    private IDefaultMapActions m_DefaultMapActionsCallbackInterface;
    private readonly InputAction m_DefaultMap_Movement;
    private readonly InputAction m_DefaultMap_Interaction;
    private readonly InputAction m_DefaultMap_Cancel;
    private readonly InputAction m_DefaultMap_EnterKey;
    private readonly InputAction m_DefaultMap_EscapeKey;
    private readonly InputAction m_DefaultMap_PartyDialogueKey;
    private readonly InputAction m_DefaultMap_ChatLogKey;
    private readonly InputAction m_DefaultMap_SplitItemStack;
    private readonly InputAction m_DefaultMap_UILeftClick;
    private readonly InputAction m_DefaultMap_UIPoint;
    public struct DefaultMapActions
    {
        private @PlayerControls m_Wrapper;
        public DefaultMapActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_DefaultMap_Movement;
        public InputAction @Interaction => m_Wrapper.m_DefaultMap_Interaction;
        public InputAction @Cancel => m_Wrapper.m_DefaultMap_Cancel;
        public InputAction @EnterKey => m_Wrapper.m_DefaultMap_EnterKey;
        public InputAction @EscapeKey => m_Wrapper.m_DefaultMap_EscapeKey;
        public InputAction @PartyDialogueKey => m_Wrapper.m_DefaultMap_PartyDialogueKey;
        public InputAction @ChatLogKey => m_Wrapper.m_DefaultMap_ChatLogKey;
        public InputAction @SplitItemStack => m_Wrapper.m_DefaultMap_SplitItemStack;
        public InputAction @UILeftClick => m_Wrapper.m_DefaultMap_UILeftClick;
        public InputAction @UIPoint => m_Wrapper.m_DefaultMap_UIPoint;
        public InputActionMap Get() { return m_Wrapper.m_DefaultMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultMapActions set) { return set.Get(); }
        public void SetCallbacks(IDefaultMapActions instance)
        {
            if (m_Wrapper.m_DefaultMapActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnMovement;
                @Interaction.started -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnInteraction;
                @Cancel.started -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnCancel;
                @EnterKey.started -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnEnterKey;
                @EnterKey.performed -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnEnterKey;
                @EnterKey.canceled -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnEnterKey;
                @EscapeKey.started -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnEscapeKey;
                @EscapeKey.performed -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnEscapeKey;
                @EscapeKey.canceled -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnEscapeKey;
                @PartyDialogueKey.started -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnPartyDialogueKey;
                @PartyDialogueKey.performed -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnPartyDialogueKey;
                @PartyDialogueKey.canceled -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnPartyDialogueKey;
                @ChatLogKey.started -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnChatLogKey;
                @ChatLogKey.performed -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnChatLogKey;
                @ChatLogKey.canceled -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnChatLogKey;
                @SplitItemStack.started -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnSplitItemStack;
                @SplitItemStack.performed -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnSplitItemStack;
                @SplitItemStack.canceled -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnSplitItemStack;
                @UILeftClick.started -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnUILeftClick;
                @UILeftClick.performed -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnUILeftClick;
                @UILeftClick.canceled -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnUILeftClick;
                @UIPoint.started -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnUIPoint;
                @UIPoint.performed -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnUIPoint;
                @UIPoint.canceled -= m_Wrapper.m_DefaultMapActionsCallbackInterface.OnUIPoint;
            }
            m_Wrapper.m_DefaultMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @EnterKey.started += instance.OnEnterKey;
                @EnterKey.performed += instance.OnEnterKey;
                @EnterKey.canceled += instance.OnEnterKey;
                @EscapeKey.started += instance.OnEscapeKey;
                @EscapeKey.performed += instance.OnEscapeKey;
                @EscapeKey.canceled += instance.OnEscapeKey;
                @PartyDialogueKey.started += instance.OnPartyDialogueKey;
                @PartyDialogueKey.performed += instance.OnPartyDialogueKey;
                @PartyDialogueKey.canceled += instance.OnPartyDialogueKey;
                @ChatLogKey.started += instance.OnChatLogKey;
                @ChatLogKey.performed += instance.OnChatLogKey;
                @ChatLogKey.canceled += instance.OnChatLogKey;
                @SplitItemStack.started += instance.OnSplitItemStack;
                @SplitItemStack.performed += instance.OnSplitItemStack;
                @SplitItemStack.canceled += instance.OnSplitItemStack;
                @UILeftClick.started += instance.OnUILeftClick;
                @UILeftClick.performed += instance.OnUILeftClick;
                @UILeftClick.canceled += instance.OnUILeftClick;
                @UIPoint.started += instance.OnUIPoint;
                @UIPoint.performed += instance.OnUIPoint;
                @UIPoint.canceled += instance.OnUIPoint;
            }
        }
    }
    public DefaultMapActions @DefaultMap => new DefaultMapActions(this);
    public interface IDefaultMapActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnEnterKey(InputAction.CallbackContext context);
        void OnEscapeKey(InputAction.CallbackContext context);
        void OnPartyDialogueKey(InputAction.CallbackContext context);
        void OnChatLogKey(InputAction.CallbackContext context);
        void OnSplitItemStack(InputAction.CallbackContext context);
        void OnUILeftClick(InputAction.CallbackContext context);
        void OnUIPoint(InputAction.CallbackContext context);
    }
}
