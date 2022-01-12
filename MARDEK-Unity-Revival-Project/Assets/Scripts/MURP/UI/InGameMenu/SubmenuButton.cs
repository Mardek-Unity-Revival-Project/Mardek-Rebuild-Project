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

        [SerializeField] Text text;

        public override void Select(bool playSFX = true)
        {
            base.Select(playSFX: playSFX);
            text.color = ACTIVE_COLOR;
        }
        public override void Deselect()
        {
            base.Deselect();
            text.color = INACTIVE_COLOR;
        }
    }
}