using UnityEngine;
using UnityEngine.InputSystem;
using MURP.Audio;
using MURP.EventSystem;

namespace MURP.UI
{
    public class InGameMenu : MonoBehaviour
    {
        SubMenuButton activeButton;
        bool isFocussing;
        public static bool lockValue { get; private set; }

        [SerializeField] Canvas canvas;
        [SerializeField] SubMenuButton[] subMenuButtons;
        [SerializeField] AudioObject openMenuSound;
        [SerializeField] AudioObject verticalMenuScrollSound;
        [SerializeField] AudioObject focusMenuSound;
        [SerializeField] AudioObject stopFocusMenuSound;

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
            canvas.enabled = false;
            lockValue = false;
        }

        public void OnToggleMenu(InputAction.CallbackContext ctx)
        {
            if (canvas.enabled)
            {
                LeaveInGameMenu();
            }
            else
            {
                if (CommandQueue.lockValue == 0)
                {
                    canvas.enabled = true;
                    AudioManager.PlaySoundEffect(openMenuSound);
                    lockValue = true;
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
                        // TODO Handle vertical navigation in focussed sub menu
                    }
                }
                else
                {
                    // TODO Handle horizontal navigation in sub menu
                }
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
                        foreach (SubMenuButton subMenuButton in subMenuButtons)
                        {
                            subMenuButton.StartFade();
                        }
                        activeButton.Focus();
                        AudioManager.PlaySoundEffect(focusMenuSound);
                        isFocussing = true;
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
                    activeButton.StopFocus();
                    foreach (SubMenuButton subMenuButton in subMenuButtons)
                    {
                        subMenuButton.StopFade();
                    }
                    AudioManager.PlaySoundEffect(stopFocusMenuSound);
                    isFocussing = false;
                }
                else
                {
                    LeaveInGameMenu();
                }
            }
        }
    }
}