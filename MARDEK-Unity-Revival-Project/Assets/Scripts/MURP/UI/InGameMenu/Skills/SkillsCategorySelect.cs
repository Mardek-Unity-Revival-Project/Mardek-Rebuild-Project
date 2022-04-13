using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MURP.CharacterSystem;
using MURP.SkillSystem;
using MURP.StatsSystem;

namespace MURP.UI
{
    public class SkillsCategorySelect : Selectable
    {
        [SerializeField] Skillset category;
        [SerializeField] Image categoryIcon;
        [SerializeField] GridLayoutGroup skillEntriesLayout;
        [SerializeField] GameObject skillEntryPrefab;
        [SerializeField] Text skillCategoryLabel;

        void UpdateCategory()
        {
            var categoryToUse = category;
            if (categoryToUse == null)
                categoryToUse = CharacterSelectable.currentSelected.Character.ActionSkillset;
            categoryIcon.sprite = categoryToUse.Sprite;
            skillCategoryLabel.text = categoryToUse.Description;
        }
    }
}