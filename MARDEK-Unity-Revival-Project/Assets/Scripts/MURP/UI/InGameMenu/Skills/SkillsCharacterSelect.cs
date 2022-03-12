using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MURP.CharacterSystem;

namespace MURP.UI
{
    public class SkillsCharacterSelect : Selectable
    {
        Color unselectedColor = new Color(0f, 0f, 0f, 0f);
        Color selectedColor = new Color(165f / 255f, 205f / 255f, 1f, 0.3f);

        [SerializeField] Image backgroundImage;
        [SerializeField] Image characterImage;

        SkillsMenu skillsMenu;
        Character character;

        public void SetCharacter(SkillsMenu skillsMenu, Character character)
        {
            // TODO Set character image
            this.skillsMenu = skillsMenu;
            this.character = character;
        }

        public override void Select(bool playSFX = true)
        {
            base.Select(playSFX: playSFX);
            this.backgroundImage.color = selectedColor;
            this.skillsMenu.OnSelect(this.character);
        }

        public override void Deselect()
        {
            base.Deselect();
            this.backgroundImage.color = unselectedColor;
        }
    }
}