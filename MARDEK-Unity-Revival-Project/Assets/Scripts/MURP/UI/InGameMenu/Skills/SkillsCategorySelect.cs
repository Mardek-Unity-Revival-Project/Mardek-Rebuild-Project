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
        [SerializeField] Image image;
        [SerializeField] GridLayoutGroup skillList;
        [SerializeField] GameObject skillEntryPrefab;
        [SerializeField] Text skillCategoryLabel;
        [SerializeField] Text selectedSkillName;
        [SerializeField] Text selectedSkillDescription;
        [SerializeField] Text bottomBarDescription;
        [SerializeField] Image selectedSkillElement;
        [SerializeField] ConditionBar mpBar;
        [SerializeField] ConditionBar rpBar;
        [SerializeField] GameObject selectedCategoryPointer;
        [SerializeField] GameObject selectedSkillPointer;

        Character currentCharacter;

        void OnEnable()
        {
            this.image.sprite = this.category.Sprite;
        }

        public void CancelCategorySelection()
        {
            this.ClearSkillEntries();
            this.selectedCategoryPointer.SetActive(false);
            this.selectedSkillPointer.SetActive(false);
            this.ShowMpBar();
        }

        void ClearSkillEntries()
        {
            var oldChildren = new List<Transform>();
            foreach (Transform oldChild in this.skillList.gameObject.transform)
            {
                oldChildren.Add(oldChild);
            }
            foreach (Transform oldChild in oldChildren)
            {
                oldChild.gameObject.SetActive(false);
                Destroy(oldChild.gameObject);
            }
        }

        void UpdateSkillEntries()
        {
            this.ClearSkillEntries();
            
            List<Skill> candidateSkills = category.Skills;
            //if (this.category.isActive) candidateSkills = this.currentCharacter.skillSet.skills;

            foreach (Skill skill in candidateSkills)
            {
                //bool canLearn = this.currentCharacter.GetStat(skill.canLearnStat).Value > 0;
                //bool hasMastery = this.currentCharacter.GetStat(skill.masteryStat).Value > 0;
                //bool isAlwaysLearned = skill.masteryPoints == 0;
                //if (canLearn || hasMastery || isAlwaysLearned)
                if (true)
                {
                    SkillEntry skillEntry = Instantiate(this.skillEntryPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<SkillEntry>();
                    skillEntry.Init(this.selectedSkillName, this.selectedSkillDescription, this.selectedSkillElement, this.selectedSkillPointer);
                    skillEntry.SetSkill(this.currentCharacter, skill);
                    skillEntry.transform.SetParent(this.skillList.transform);
                    skillEntry.transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }

            this.skillList.gameObject.GetComponent<SelectableLayout>().RefreshSelectables();
        }

        public void SetCharacter(Character newCharacter)
        {
            this.currentCharacter = newCharacter;
        }

        private void ShowMpBar()
        {
            this.mpBar.gameObject.SetActive(true);
            this.rpBar.gameObject.SetActive(false);
            this.bottomBarDescription.text = "MP";
            this.mpBar.SetValues(100, 150); // TODO Improve this once we actually have mana
        }

        public override void Select(bool playSFX = true)
        {
            base.Select(playSFX: playSFX);
            this.UpdateSkillEntries();
            this.skillCategoryLabel.text = this.category.Description;
            this.selectedCategoryPointer.transform.position = this.transform.position;
            this.selectedCategoryPointer.SetActive(true);
            //if (this.category.isActive)
            //{
            //    this.ShowMpBar();
            //}  
            //else
            //{
            //    this.mpBar.gameObject.SetActive(false);
            //    this.rpBar.gameObject.SetActive(true);
            //    this.bottomBarDescription.text = "RP";
            //    this.rpBar.SetValues(10, 100); // TODO Improve this once this system is more mature
            //}
        }

        public override void Deselect()
        {
            base.Deselect();
        }
    }
}