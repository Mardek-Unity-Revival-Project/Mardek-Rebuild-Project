using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class ItemInfoTab : Selectable
    {
        static readonly Color LOWER_PANEL_BASE_COLOR = new Color(56f / 255f, 40f / 255f, 23f / 255f, 100f / 255f);
        static readonly Color LOWER_PANEL_ACTIVE_COLOR = new Color(84f / 255f, 64f / 255f, 39f / 255f, 170f / 255f);
        static readonly Color LOWER_TEXT_BASE_COLOR = new Color(104f / 255f, 95f / 255f, 88f / 255f);
        static readonly Color LOWER_TEXT_ACTIVE_COLOR = new Color(238f / 255f, 203f / 255f, 127f / 255f);

        [SerializeField] Text tabName;
        [SerializeField] Image panelImage;

        public override void Select(bool playSFX = true)
        {
            tabName.color = LOWER_TEXT_ACTIVE_COLOR;
            panelImage.color = LOWER_PANEL_ACTIVE_COLOR;
            base.Select(playSFX);
        }

        public override void Deselect()
        {
            tabName.color = LOWER_TEXT_BASE_COLOR;
            panelImage.color = LOWER_PANEL_BASE_COLOR;
            base.Deselect();
        }
    }
}
