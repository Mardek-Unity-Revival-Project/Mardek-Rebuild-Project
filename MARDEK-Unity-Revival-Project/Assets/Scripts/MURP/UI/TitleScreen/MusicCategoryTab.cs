using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class MusicCategoryTab : Selectable
    {
        [SerializeField] Image noteImage;
        [SerializeField] Text musicTypeLabel;
        [SerializeField] string correspondingMusicTypeLabel;
        
        bool isSelected;

        public override void Select(bool playSFX = true)
        {
            base.Select(playSFX: playSFX);
            isSelected = true;
            UpdateAppearance();
        }

        public override void Deselect()
        {
            base.Deselect();
            isSelected = false;
            UpdateAppearance();
        }

        void Awake()
        {
            UpdateAppearance();
        }

        void UpdateAppearance()
        {
            if (isSelected)
            {
                noteImage.color = new Color(1f, 1f, 1f, 1f);
                musicTypeLabel.text = correspondingMusicTypeLabel;
            }
            else noteImage.color = new Color(1f, 1f, 1f, 0.1f);
        }
    }
}