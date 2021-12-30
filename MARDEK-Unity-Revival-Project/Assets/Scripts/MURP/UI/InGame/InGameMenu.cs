using UnityEngine;
using UnityEngine.InputSystem;
using MURP.Core;
using MURP.CharacterSystem;
using MURP.Audio;
using MURP.EventSystem;

namespace MURP.UI
{
    public class InGameMenu : MonoBehaviour
    {
        SubMenuButton activeButton;
        bool isFocussing;

        [SerializeField] Party party;
        [SerializeField] Canvas canvas;
        [SerializeField] SubMenuButton[] subMenuButtons;
        [SerializeField] AudioObject openMenuSound;
        [SerializeField] AudioObject verticalMenuScrollSound;
        [SerializeField] AudioObject focusMenuSound;
        [SerializeField] AudioObject stopFocusMenuSound;

        void Start()
        {
            foreach (SubMenuButton subMenuButton in this.subMenuButtons)
            {
                subMenuButton.SetParty(this.party);
                subMenuButton.SetForceFocus(ForceFocus);
            }
        }

        void SetActiveSubMenu(SubMenuButton buttonToActivate)
        {
            foreach (SubMenuButton button in subMenuButtons)
            {
                if (button != buttonToActivate)
                {
                    button.SetInactive();
                }
            }
            buttonToActivate.SetActive();
            activeButton = buttonToActivate;
        }

        void Awake()
        {
            SetActiveSubMenu(subMenuButtons[0]);
            canvas.enabled = false;
        }

        void LeaveInGameMenu()
        {
            if (this.isFocussing) {
                if (!this.activeButton.StopFocus()) {
                    return;
                }
            }
            canvas.enabled = false;
            PlayerLocks.UISystemLock--;
        }

        public void OnToggleMenu(InputAction.CallbackContext ctx)
        {
            if (canvas.enabled)
            {
                LeaveInGameMenu();
            }
            else
            {
                if (PlayerLocks.EventSystemLock == 0)
                {
                    canvas.enabled = true;
                    PlayerLocks.UISystemLock++;
                    AudioManager.PlaySoundEffect(openMenuSound);
                }
            }
        }

        public void OnMovementInput(InputAction.CallbackContext ctx)
        {
            if (canvas.enabled)
            {
                Vector2 inputDirection = ctx.ReadValue<Vector2>();
                if (inputDirection.y != 0f)
                {

                    if (!isFocussing)
                    {
                        int currentSubMenuIndex = 0;
                        for (int candidateIndex = 0; candidateIndex < subMenuButtons.Length; candidateIndex++)
                        {
                            if (subMenuButtons[candidateIndex] == activeButton)
                            {
                                currentSubMenuIndex = candidateIndex;
                                break;
                            }
                        }

                        int nextSubMenuIndex = currentSubMenuIndex + (inputDirection.y < 0f ? 1 : -1);
                        if (nextSubMenuIndex < 0) nextSubMenuIndex += subMenuButtons.Length;
                        if (nextSubMenuIndex >= subMenuButtons.Length) nextSubMenuIndex -= subMenuButtons.Length;
                        SetActiveSubMenu(subMenuButtons[nextSubMenuIndex]);
                        AudioManager.PlaySoundEffect(verticalMenuScrollSound);
                    }
                    else
                    {
                        this.activeButton.PropagateVerticalMovement(inputDirection.y);
                    }
                }
                else
                {
                    this.activeButton.PropagateHorizontalMovement(inputDirection.x);
                }
            }
        }

        void ForceFocus()
        {
            if (!this.isFocussing)
            {
                foreach (SubMenuButton subMenuButton in subMenuButtons)
                {
                    subMenuButton.StartFade();
                }
                activeButton.Focus();
                AudioManager.PlaySoundEffect(focusMenuSound);
                isFocussing = true;
            }
        }

        public void FocusOnSubMenu(InputAction.CallbackContext ctx)
        {
            if (canvas.enabled)
            {
                if (!isFocussing)
                {
                    if (activeButton.IsDeep())
                    {
                        this.ForceFocus();
                    }
                }
                else
                {
                    // TODO Propagate event to sub menu?
                }
            }
        }

        public void LeaveSubMenu(InputAction.CallbackContext ctx)
        {
            if (canvas.enabled)
            {
                if (isFocussing)
                {
                    if (activeButton.StopFocus()) {
                        foreach (SubMenuButton subMenuButton in subMenuButtons)
                        {
                            subMenuButton.StopFade();
                        }
                        AudioManager.PlaySoundEffect(stopFocusMenuSound);
                        isFocussing = false;
                    }
                }
                else
                {
                    LeaveInGameMenu();
                }
            }
        }
    }
}