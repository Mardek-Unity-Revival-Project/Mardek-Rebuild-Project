using MURP.DiscoverySystem;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class EncyclopediaSection : Selectable
    {
        public static EncyclopediaSection selected { get; private set; }

        [SerializeField] Image image;
        public GameObject section;
        public EncyclopediaList list;

        override public void Select(bool playSFX = true)
        {
            base.Select(playSFX);
            selected = this;
            image.color = new Color(1f, 1f, 1f, 1f);
        }

        override public void Deselect()
        {
            base.Deselect();
            image.color = new Color(1f, 1f, 1f, 45f / 255f);
        }
    }
}
