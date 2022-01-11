using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class SelectableWithTintTransition : Selectable
    {
        [SerializeField] Text selectableText;
        [SerializeField] Color DeselectedTextColor = new Color(104f / 255f, 95f / 255f, 88f / 255f);
        [SerializeField] Color SelectedTextColor = new Color(238f / 255f, 203f / 255f, 127f / 255f);

        [Space]
        [SerializeField] Image selectableImage;
        [SerializeField] Color DeselectedImageColor = new Color(56f / 255f, 40f / 255f, 23f / 255f, 100f / 255f);
        [SerializeField] Color SelectedImageColor = new Color(84f / 255f, 64f / 255f, 39f / 255f, 170f / 255f);

        private void OnValidate()
        {
            Paint(false);
        }

        public override void Select(bool playSFX = true)
        {
            Paint(true);
            base.Select(playSFX);
        }
        public override void Deselect()
        {
            Paint(false);
            base.Deselect();
        }
        
        void Paint(bool asSelected)
        {
            if (selectableText)
                selectableText.color = asSelected ? SelectedTextColor : DeselectedTextColor;
            if (selectableImage)
                selectableImage.color = asSelected ? SelectedImageColor : DeselectedImageColor;
        }
    }
}
