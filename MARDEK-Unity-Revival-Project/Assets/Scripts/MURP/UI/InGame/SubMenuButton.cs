using UnityEngine;
using UnityEngine.UI;
using MURP.CharacterSystem;
using UnityEngine.EventSystems;

namespace MURP.UI
{
    public class SubmenuButton : Selectable
    {
        static readonly Color INACTIVE_COLOR = new Color(245 / 255f, 229 / 255f, 156 / 255f);
        static readonly Color ACTIVE_COLOR = new Color(110 / 255f, 170 / 255f, 220 / 255f);

        static SubmenuButton lastSelectedSubmenu = null;
        [SerializeField] Text text;
        [SerializeField] GameObject submenuPanel;

        void OnValidate()
        {
            PaintAsDeselected();
        }
        public override void Select()
        {
            text.color = ACTIVE_COLOR;
            if (lastSelectedSubmenu)
            {
                lastSelectedSubmenu.CloseSubmenuPanel();
                lastSelectedSubmenu.PaintAsDeselected();
            }
            OpenSubmenuPanel();
            lastSelectedSubmenu = this;
        }
        public override void Deselect()
        {
            PaintAsDeselected();
            CloseSubmenuPanel();
        }
        public void PaintAsDeselected()
        {
            text.color = INACTIVE_COLOR;
        }
        public void CloseSubmenuPanel()
        {
            if (submenuPanel)
                submenuPanel.SetActive(false);
        }
        void OpenSubmenuPanel()
        {
            if(submenuPanel)
                submenuPanel.SetActive(true);
        }
    }
}