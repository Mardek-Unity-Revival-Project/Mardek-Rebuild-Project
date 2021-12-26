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
            this.subMenu.SetParty(party);
        }

        public void SetForceFocus(System.Action forceFocusAction)
        {
            if (this.subMenu is InventorySubMenu) (this.subMenu as InventorySubMenu).SetForceFocusAction(forceFocusAction);
        }

        public bool IsDeep()
        {
            return this.subMenu is FocusSubMenu;
        }

        public void SetActive()
        {
            this.text.color = ACTIVE_COLOR;
            if (this.panel != null) this.panel.SetActive(true);
            this.subMenu.SetActive();
        }

        public void SetInactive()
        {
            this.text.color = INACTIVE_COLOR;
            if (this.panel != null) this.panel.SetActive(false);
            this.subMenu.SetInActive();
        }

        public void StartFade()
        {
            if (this.text.color == ACTIVE_COLOR) {
                this.text.color = FADED_ACTIVE_COLOR;
            } else {
                this.text.color = FADED_INACTIVE_COLOR;
            }
        }

        public void StopFade()
        {
            if (this.text.color == FADED_ACTIVE_COLOR) {
                this.text.color = ACTIVE_COLOR;
            } else {
                this.text.color = INACTIVE_COLOR;
            }
        }

        public void Focus()
        {
            (this.subMenu as FocusSubMenu).StartFocus();
        }

        public bool StopFocus()
        {
            return (this.subMenu as FocusSubMenu).StopFocus();
        }
    }
}