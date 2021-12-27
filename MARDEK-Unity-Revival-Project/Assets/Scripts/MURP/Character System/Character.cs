using UnityEngine;
using System.Collections.Generic;
using MURP.StatsSystem;
using MURP.SkillSystem;

namespace MURP.CharacterSystem
{
    [System.Serializable]
    public class Character : MonoBehaviour, IStats
    {
        [SerializeField] CharacterInfo bio;
        public CharacterInfo CharacterInfo { get { return bio; } }

        [SerializeField] StatsSet baseStatus = new StatsSet();
        List<StatsSet> statusChanges = new List<StatsSet>();
        [SerializeField] MURP.Inventory.Inventory _inventory;

        public MURP.Inventory.Inventory inventory { get { return _inventory; } }

        public StatHolder<T, StatOfType<T>> GetStat<T>(StatOfType<T> desiredStatus)
        {            
            var resultHolder = new StatHolder<T, StatOfType<T>>(desiredStatus);

            SumHolders(ref resultHolder, baseStatus.GetStat(desiredStatus));
            foreach (var set in statusChanges)
                SumHolders(ref resultHolder, set.GetStat(desiredStatus));
            return resultHolder;

            void SumHolders(ref StatHolder<T, StatOfType<T>> firstHolder, StatHolder<T, StatOfType<T>> secondHolder)
            {
                if (firstHolder == null)
                    return;
                if (typeof(T) == typeof(int))
                {
                    var firstInt = firstHolder as StatHolder<int, StatOfType<int>>;
                    var secondInt = secondHolder as StatHolder<int, StatOfType<int>>;
                    firstInt.Value += secondInt.Value;
                }                
            }
        }

        void Start()
        {
            this.inventory.Start();
        }


        [SerializeField] Skill skill;
        [ContextMenu("TriggerSkill")]
        void TriggerSkill()
        {
            skill.Apply(this, null);
        }
    }
}