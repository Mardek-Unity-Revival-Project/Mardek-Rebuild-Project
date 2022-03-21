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

        public SkillEntry selectedSkill;

        void OnEnable()
        {
            OnSelect(party.Characters[0]);
            for (int index = 0; index < characters.Count; index++) {
                if (index < party.Characters.Count)
                {
                    characters[index].gameObject.SetActive(true);
                    characters[index].SetCharacter(this, party.Characters[index]);
                }
                else
                {
                    characters[index].gameObject.SetActive(false);
                }
            }
        }

        public void ToggleSelectedSkill()
        {
            if (selectedSkill != null) selectedSkill.Toggle();
        }

        public void OnSelect(Character character)
        {
            skillSetIcon.sprite = character.skillSet.sprite;
            selectedSkillName.text = character.skillSet.displayName;
            selectedSkillDescription.text = character.skillSet.description;
            selectedSkillElement.sprite = transparentSprite;
            foreach (var categorySelect in categories)
            {
                categorySelect.SetCharacter(character);
            }
        }
    }
}