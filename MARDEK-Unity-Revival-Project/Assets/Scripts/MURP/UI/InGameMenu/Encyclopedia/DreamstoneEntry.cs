using MURP.DiscoverySystem;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class DreamstoneEntry : Selectable
    {
        public static Dreamstone selected { get; private set; }

        [SerializeField] Image selectedAura;
        [SerializeField] Image newMarker;

        Dreamstone dreamstone;
        bool isSelected;

        override public void Select(bool playSFX = true)
        {
            base.Select(playSFX);
            isSelected = true;
            selected = dreamstone;
            UpdateAppearance();
        }

        override public void Deselect()
        {
            base.Deselect();
            isSelected = false;
            UpdateAppearance();
        }

        void UpdateAppearance()
        {
            selectedAura.color = isSelected ? new Color(1f, 1f, 1f, 0.7f) : new Color(1f, 1f, 1f, 0f);
            newMarker.gameObject.SetActive(dreamstone.isNew);
        }

        public void Init(Dreamstone dreamstone)
        {
            this.dreamstone = dreamstone;
        }

        void OnEnable()
        {
            if (dreamstone != null) UpdateAppearance();
        }
    }
}
