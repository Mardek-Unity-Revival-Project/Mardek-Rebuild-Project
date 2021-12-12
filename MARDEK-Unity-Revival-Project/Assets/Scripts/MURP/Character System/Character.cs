using UnityEngine;
using System.Collections.Generic;
using MURP.StatsSystem;

namespace MURP.CharacterSystem
{
    [System.Serializable]
    public class Character : MonoBehaviour, IStats
    {
        [SerializeField] CharacterBio bio;

        [SerializeField] StatsSet baseStatus;
        List<StatsSet> statusChanges;

        [SerializeField] ModifyStat skill;

        public StatHolder<int, StatOfType<int>> GetStatus(StatOfType<int> desiredStatus)
        {
            var resultHolder = new StatHolder<int, StatOfType<int>>(desiredStatus);

            SumHolders(ref resultHolder, baseStatus.GetStatus(desiredStatus));
            foreach(var set in statusChanges)
                SumHolders(ref resultHolder, set.GetStatus(desiredStatus));

            return resultHolder;

            void SumHolders(ref StatHolder<int, StatOfType<int>> baseHolder, StatHolder<int, StatOfType<int>> targetHolder)
            {
                if (targetHolder != null) baseHolder.Value += targetHolder.Value;
            }
        }
        public StatHolder<float, StatOfType<float>> GetStatus(StatOfType<float> desiredStatus)
        {
            throw new System.NotImplementedException();
        }

        [ContextMenu("TriggerSkill")]
        void TriggerSkill()
        {
            skill.Apply(this, this);
        }
    }
}