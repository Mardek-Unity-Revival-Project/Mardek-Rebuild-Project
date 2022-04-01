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


        void Start()
        {
            this.inventory.Start();
        }

        public void BattleAct(List<Character> allies, List<Character> enemies)
        {
            var randomEnemy = enemies[Random.Range(0, enemies.Count)];
            skill.Apply(this, randomEnemy);
        }

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

        [SerializeField] Skill skill;
    }
}