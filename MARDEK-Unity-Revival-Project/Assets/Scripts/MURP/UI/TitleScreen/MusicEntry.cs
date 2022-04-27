using MURP.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class MusicEntry : Selectable
    {
        public static Music selectedMusic { private set; get; }

        [SerializeField] Text displayName;
        [SerializeField] Image backgroundImage;
        [SerializeField] Music music;

        public override void Select(bool playSFX = true)
        {
            base.Select(playSFX: playSFX);
            selectedMusic = music;
            UpdateAppearance();
        }

        public override void Deselect()
        {
            base.Deselect();
            selectedMusic = null;
            UpdateAppearance();
        }

        void OnEnable()
        {
            UpdateAppearance();
        }

        void UpdateAppearance()
        {
            if (selectedMusic == music)
            {
                displayName.color = new Color(221f / 255f, 238f / 255f, 253f / 255f);
                backgroundImage.color = new Color(44f / 255f, 85f / 255f, 129f / 255f);
            }
            else
            {
                displayName.color = new Color(248f / 255f, 232f / 255f, 193f / 255f);
                backgroundImage.color = new Color(126f / 255f, 66f / 255f, 16f / 255f);
            }
        }
    }
}
