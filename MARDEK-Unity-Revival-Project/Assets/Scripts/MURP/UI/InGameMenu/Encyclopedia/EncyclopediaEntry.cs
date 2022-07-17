using MURP.DiscoverySystem;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class EncyclopediaEntry : Selectable
    {
        static readonly Color SELECTED_TEXT_COLOR = new Color(240f / 255f, 224f / 255f, 185f / 255f);
        static readonly Color DEFAULT_TEXT_COLOR = new Color(238f / 255f, 203f / 255f, 127f / 255f);
        static readonly Color UNDISCOVERED_TEXT_COLOR = new Color(94f / 255f, 87f / 255f, 71f / 255f);

        static readonly Color SELECTED_BACKGROUND_COLOR = new Color(0f, 0f, 200f / 255f, 0.3f);
        static readonly Color DEFAULT_BACKGROUND_COLOR = new Color(0f, 0f, 0f, 0.2f);

        public static EncyclopediaEntry selectedEntry { get; private set; }

        [SerializeField] Image background;
        [SerializeField] Image icon;
        [SerializeField] Text text;

        public EncyclopediaItem item { get; private set; }
        bool selected;

        override public void Select(bool playSFX = true)
        {
            base.Select(playSFX);
            selected = true;
            selectedEntry = this;
            UpdateAppearance();
        }

        override public void Deselect()
        {
            base.Deselect();
            selected = false;
            UpdateAppearance();
        }

        public void Init(EncyclopediaItem item, int itemIndex)
        {
            if (item.isDiscovered)
            {
                this.text.text = (itemIndex + 1) + ") " + item.displayName;
                this.icon.sprite = item.icon;
            }
            else
            {
                this.text.text = (itemIndex + 1) + ") ------";
                this.text.color = UNDISCOVERED_TEXT_COLOR;
                this.icon.color = new Color(0f, 0f, 0f, 0f);
            }
            this.item = item;
        }

        void UpdateAppearance()
        {
            background.color = selected ? SELECTED_BACKGROUND_COLOR : DEFAULT_BACKGROUND_COLOR;
            text.color = selected ? SELECTED_TEXT_COLOR : (item.isDiscovered ? DEFAULT_TEXT_COLOR : UNDISCOVERED_TEXT_COLOR);
        }
    }
}
