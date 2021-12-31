using UnityEngine;
using UnityEngine.UI;
using MURP.CharacterSystem;

namespace MURP.UI
{
    public class SubMenuButton : MonoBehaviour
    {
        static readonly Color INACTIVE_COLOR = new Color(245 / 255f, 229 / 255f, 156 / 255f);
        static readonly Color ACTIVE_COLOR = new Color(110 / 255f, 170 / 255f, 220 / 255f);
        static readonly Color FADED_INACTIVE_COLOR = new Color(210 / 255f, 190 / 255f, 180 / 255f);
        static readonly Color FADED_ACTIVE_COLOR = new Color(150 / 255f, 170 / 255f, 200 / 255f);

        [SerializeField] Text text;
        [SerializeField] GameObject panel;
        [SerializeField] SubMenu subMenu;

        public void SetParty(Party party)
        {
            subMenu.SetParty(party);
        }

        public void SetForceFocus(System.Action forceFocusAction)
        {
            if (subMenu is InventorySubMenu) (subMenu as InventorySubMenu).SetForceFocusAction(forceFocusAction);
        }

        public bool IsDeep()
        {
            return subMenu is FocusSubMenu;
        }

        public void PropagateVerticalMovement(float movement)
        {
            (subMenu as FocusSubMenu).HandleVerticalMovement(movement);
        }

        public void PropagateHorizontalMovement(float movement)
        {
            subMenu.HandleHorizontalMovement(movement);
        }

        public void SetActive()
        {
            text.color = ACTIVE_COLOR;
            if (panel != null) panel.SetActive(true);
            subMenu.SetActive();
        }

        public void SetInactive()
        {
            text.color = INACTIVE_COLOR;
            if (panel != null) panel.SetActive(false);
            subMenu.SetInActive();
        }

        public void StartFade()
        {
            if (text.color == ACTIVE_COLOR) {
                text.color = FADED_ACTIVE_COLOR;
            } else {
                text.color = FADED_INACTIVE_COLOR;
            }
        }

        public void StopFade()
        {
            if (text.color == FADED_ACTIVE_COLOR) {
                text.color = ACTIVE_COLOR;
            } else {
                text.color = INACTIVE_COLOR;
            }
        }

        public void Focus()
        {
            (subMenu as FocusSubMenu).StartFocus();
        }

        public bool StopFocus()
        {
            return (subMenu as FocusSubMenu).StopFocus();
        }
    }
}