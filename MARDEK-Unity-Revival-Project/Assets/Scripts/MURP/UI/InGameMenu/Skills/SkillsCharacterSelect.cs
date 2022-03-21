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
        [SerializeField] Image characterImage;
        [SerializeField] Text selectedCharacterName;
        [SerializeField] ConditionBar mpBar;

        SkillsMenu skillsMenu;
        Character character;
        int currentTick;

        public void SetCharacter(SkillsMenu skillsMenu, Character character)
        {
            this.skillsMenu = skillsMenu;
            this.character = character;
            this.currentTick = 0;
        }

        public void FixedUpdate()
        {
            if (this.currentTick == 0)
            {
                this.characterImage.sprite = this.character.downSprite1;
            }
            if (this.currentTick == 15)
            {
                this.characterImage.sprite = this.character.downSprite2;
            }
            this.currentTick += 1;
            if (this.currentTick == 30)
            {
                this.currentTick = 0;
            }
        }

        public override void Select(bool playSFX = true)
        {
            base.Select(playSFX: playSFX);
            this.backgroundImage.color = selectedColor;
            if (character == null)
                return;
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