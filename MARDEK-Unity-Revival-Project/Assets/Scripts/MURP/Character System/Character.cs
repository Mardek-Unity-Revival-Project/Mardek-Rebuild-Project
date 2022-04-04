using UnityEngine;
using System.Collections.Generic;
using MURP.InventorySystem;
using MURP.StatsSystem;
using MURP.SkillSystem;

namespace MURP.CharacterSystem
{
    [System.Serializable]
    public class Character : MonoBehaviour, IStats
    {
        [field: SerializeField] public CharacterInfo CharacterInfo { get; private set; }
        [SerializeField] StatsSet baseStatus = new StatsSet();
        List<StatsSet> statusChanges = new List<StatsSet>();
        [field: SerializeField] public Inventory EquippedItems { get; private set; }
        [field: SerializeField] public Inventory Inventory { get; private set; }
        [field: SerializeField] public Skillset ActionSkillset { get; private set; }
        [SerializeField] List<Skill> startingSkills = new List<Skill>();

        Dictionary<Skill, SkillProgress> internalSkills = new Dictionary<Skill, SkillProgress>();
        void Awake()
        {
            foreach(var skill in startingSkills)
            {
                var slot = new SkillProgress();
                slot.MasteryPoints = -1;
                internalSkills.Add(skill, slot);
            }
        }

        void GetSkills(
            ref Dictionary<Skill, SkillProgress> masteredSkills,
            ref Dictionary<Skill, SkillProgress> equippedSkills,
            ref Dictionary<Skill, SkillProgress> unequippedSkills
            )
        {
            UpdateSkillDictionary();
            masteredSkills = new Dictionary<Skill, SkillProgress>();
            foreach (var entry in internalSkills)
            {
                if (IsSkillMastered(entry.Key, entry.Value))
                    masteredSkills.Add(entry.Key, entry.Value);
            }
        }

        bool IsSkillMastered(Skill skill, SkillProgress progress)
        {
            var points = progress.MasteryPoints;
            var requiredPoints = skill.PointsRequiredToMaster;
            return ((points >= requiredPoints) || (points == -1));
        }

        public void UpdateSkillDictionary()
        {
            foreach (var slot in EquippedItems.Slots)
                foreach (var skill in slot.item.SkillsToEquip)
                {
                    if (internalSkills.ContainsKey(skill))
                        continue;
                    internalSkills.Add(skill, new SkillProgress());
                }
        }
        public StatHolder<T, StatOfType<T>> GetStat<T>(StatOfType<T> desiredStatus)
        {            
            var resultHolder = new StatHolder<T, StatOfType<T>>(desiredStatus);

            SumHolders(ref resultHolder, baseStatus.GetStat(desiredStatus));
            foreach (var set in statusChanges)
                SumHolders(ref resultHolder, set.GetStat(desiredStatus));

            foreach(var slot in EquippedItems.Slots)
            {
                var equippableItem = slot.item as EquippableItem;
                if(equippableItem != null)
                    SumHolders(ref resultHolder, equippableItem.statBoosts.GetStat(desiredStatus));
            }
            
            return resultHolder;

            void SumHolders(ref StatHolder<T, StatOfType<T>> firstHolder, StatHolder<T, StatOfType<T>> secondHolder)
            {
                if (firstHolder == null)
                    return;
                if (typeof(T) == typeof(int))
                {
                    var firstValue = firstHolder as StatHolder<int, StatOfType<int>>;
                    var secondValue = secondHolder as StatHolder<int, StatOfType<int>>;
                    firstValue.Value += secondValue.Value;
                }
                if (typeof(T) == typeof(float))
                {
                    var firstValue = firstHolder as StatHolder<float, StatOfType<float>>;
                    var secondValue = secondHolder as StatHolder<float, StatOfType<float>>;
                    firstValue.Value += secondValue.Value;
                }
            }
        }
        public void ModifyStat<T>(StatOfType<T> stat, float delta)
        {
            baseStatus.ModifyStat(stat, delta);
        }
    }
}