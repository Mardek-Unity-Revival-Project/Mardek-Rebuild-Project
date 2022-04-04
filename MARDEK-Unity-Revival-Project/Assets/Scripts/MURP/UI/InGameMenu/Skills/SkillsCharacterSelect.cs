using UnityEngine;
using UnityEngine.UI;
using MURP.CharacterSystem;
using MURP.StatsSystem;

namespace MURP.UI
{
    public class SkillsCharacterSelect : Selectable
    {
        Color unselectedColor = new Color(0f, 0f, 0f, 0f);
        Color selectedColor = new Color(165f / 255f, 205f / 255f, 1f, 0.3f);

        [SerializeField] Image backgroundImage;
        [SerializeField] Text selectedCharacterName;
        [SerializeField] ConditionBar mpBar;

        [SerializeField] SkillsMenu skillsMenu;
        Character character;

        public void SetCharacter(Character character)
        {
            this.character = character;
        }

        public override void Select(bool playSFX = true)
        {
            base.Select(playSFX: playSFX);
            this.backgroundImage.color = selectedColor;
            this.skillsMenu.OnSelect(this.character);
            this.selectedCharacterName.text = character.name;
            this.mpBar.SetValues(100, 150); // TODO Fix this once we have mana
        }

        public override void Deselect()
        {
            base.Deselect();
            this.backgroundImage.color = unselectedColor;
        }
    }
}