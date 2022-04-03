using MURP.CharacterSystem;
using MURP.StatsSystem;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class SingleResistance : MonoBehaviour
    {
        static readonly Color VULNERABLE_BACKGROUND_COLOR = new Color(0.7f, 0f, 0f, 0.1f);
        static readonly Color NEUTRAL_BACKGROUND_COLOR = new Color(0f, 0f, 0f, 0.2f);
        static readonly Color RESIST_BACKGROUND_COLOR = new Color(0.2f, 0.9f, 1f, 0.1f);
        static readonly Color ABSORB_BACKGROUND_COLOR = new Color(0.3f, 1f, 0.15f, 0.1f);

        static readonly Color VULNERABLE_TEXT_COLOR = new Color(254f / 255f, 169f / 255f, 169f / 255f);
        static readonly Color RESIST_TEXT_COLOR = new Color(84f / 255f, 220f / 255f, 254f / 255f);
        static readonly Color ABSORT_TEXT_COLOR = new Color(152f / 255f, 254f / 255f, 101f / 255f);

        static readonly Color PASSIVE_ICON_COLOR = new Color(1f, 1f, 1f, 0.05f);
        static readonly Color ACTIVE_ICON_COLOR = new Color(1f, 1f, 1f, 0.9f);

        [SerializeField] FloatStat resistanceStat;
        [SerializeField] Image icon;
        [SerializeField] Image background;
        [SerializeField] Text text;

        public void SetCharacter(Character character)
        {
            this.SetValue(this.resistanceStat == null ? 0f : character.GetStat(this.resistanceStat).Value);
        }

        void SetValue(float newValue)
        {
            int percentage = (int) (newValue * 100f + 0.5f * (newValue > 0f ? 1f : -1f));

            text.gameObject.SetActive(percentage != 0);
            text.text = percentage + "%";


            if (percentage < 0)
            {
                background.color = VULNERABLE_BACKGROUND_COLOR;
                text.color = VULNERABLE_TEXT_COLOR;
                icon.color = ACTIVE_ICON_COLOR;
            }
            if (percentage == 0)
            {
                background.color = NEUTRAL_BACKGROUND_COLOR;
                icon.color = PASSIVE_ICON_COLOR;
            }
            if (percentage > 0 && percentage <= 100)
            {
                background.color = RESIST_BACKGROUND_COLOR;
                text.color = RESIST_TEXT_COLOR;
                icon.color = ACTIVE_ICON_COLOR;
            }
            if (percentage > 100)
            {
                background.color = ABSORB_BACKGROUND_COLOR;
                text.color = ABSORT_TEXT_COLOR;
                icon.color = ACTIVE_ICON_COLOR;
            }
        }
    }
}
