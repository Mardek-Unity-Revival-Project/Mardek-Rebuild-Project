using JRPG;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameMenu : MonoBehaviour 
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private SubMenuButton partyButton;

    [SerializeField]
    private SubMenuButton skillsButton;

    [SerializeField]
    private SubMenuButton inventoryButton;

    [SerializeField]
    private SubMenuButton mapButton;

    [SerializeField]
    private SubMenuButton questsButton;

    [SerializeField]
    private SubMenuButton plotItemsButton;

    [SerializeField]
    private SubMenuButton statusButton;

    [SerializeField]
    private SubMenuButton medalsButton;

    [SerializeField]
    private SubMenuButton encyclopediaButton;

    [SerializeField]
    private SubMenuButton optionsButton;

    [SerializeField]
    private SubMenuButton helpButton;

    private SubMenuButton activeButton;

    private SubMenuButton[] GetSubMenuButtons()
    {
        return new SubMenuButton[]{
            partyButton,
            skillsButton,
            inventoryButton,
            mapButton,
            questsButton,
            plotItemsButton,
            statusButton,
            medalsButton,
            encyclopediaButton,
            optionsButton,
            helpButton
        };
    }

    private void SetActiveSubMenu(SubMenuButton buttonToActivate)
    {
        foreach (SubMenuButton button in GetSubMenuButtons()) {
            if (button != buttonToActivate) {
                button.SetInactive();
            }
        }
        buttonToActivate.SetActive();
        activeButton = buttonToActivate;
    }

    private void Awake()
    {
        SetActiveSubMenu(partyButton);
        canvas.enabled = false;
    }

    public void OnToggleMenu(InputAction.CallbackContext ctx)
    {
        if (canvas.enabled) {
            canvas.enabled = false;
            PlayerController.playerControllerLockValue--;
        } else {
            if (PlayerController.playerControllerLockValue <= 0) {
                canvas.enabled = true;
                PlayerController.playerControllerLockValue++;
            }
        }
    }

    public void OnMovementInput(InputAction.CallbackContext ctx)
    {
        if (canvas.enabled) {
            Vector2 inputDirection = ctx.ReadValue<Vector2>();
            if (inputDirection.y != 0f) {

                SubMenuButton[] subMenuButtons = GetSubMenuButtons();
                int currentSubMenuIndex = 0;
                for (int candidateIndex = 0; candidateIndex < subMenuButtons.Length; candidateIndex++) {
                    if (subMenuButtons[candidateIndex] == activeButton) {
                        currentSubMenuIndex = candidateIndex;
                        break;
                    }
                }

                int nextSubMenuIndex = currentSubMenuIndex + (inputDirection.y < 0f ? 1 : -1);
                if (nextSubMenuIndex < 0) nextSubMenuIndex += subMenuButtons.Length;
                if (nextSubMenuIndex >= subMenuButtons.Length) nextSubMenuIndex -= subMenuButtons.Length;
                SetActiveSubMenu(subMenuButtons[nextSubMenuIndex]);
            } else {
                // TODO Handle horizontal navigation
            }
        }
    }
}
