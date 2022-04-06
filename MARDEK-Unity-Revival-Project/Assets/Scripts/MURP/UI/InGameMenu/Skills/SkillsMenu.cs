using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MURP.CharacterSystem;

namespace MURP.UI
{
    public class SkillsMenu : MonoBehaviour
    {
        [SerializeField] Party party = null;

        [SerializeField] Image skillSetIcon;
        [SerializeField] List<SkillsCharacterSelect> characters;
        [SerializeField] Text selectedSkillName;
        [SerializeField] Text selectedSkillDescription;
        [SerializeField] Sprite transparentSprite;
        [SerializeField] Image selectedSkillElement;
        [SerializeField] List<SkillsCategorySelect> categories;

        void OnEnable()
        {
            this.OnSelect(this.party.Characters[0]);
            for (int index = 0; index < this.characters.Count; index++) {
                if (index < this.party.Characters.Count)
                {
                    this.characters[index].gameObject.SetActive(true);
                    this.characters[index].SetCharacter(this, this.party.Characters[index]);
                }
                else
                {
                    this.characters[index].gameObject.SetActive(false);
                }
            }
        }

        public void ToggleSelectedSkill()
        {
            if (SkillEntry.selectedSkill != null) SkillEntry.selectedSkill.Toggle();
        }

        public void OnSelect(Character character)
        {
            this.skillSetIcon.sprite = character.skillSet.sprite;
            this.selectedSkillName.text = character.skillSet.displayName;
            this.selectedSkillDescription.text = character.skillSet.description;
            this.selectedSkillElement.sprite = this.transparentSprite;
            foreach (var categorySelect in this.categories)
            {
                categorySelect.SetCharacter(character);
            }
        }
    }
}