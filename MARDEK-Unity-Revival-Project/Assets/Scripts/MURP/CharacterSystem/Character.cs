using UnityEngine;
using System.Collections.Generic;
using MURP.StatusSystem;

namespace MURP.CharacterSystem
{
    [System.Serializable]
    public class Character : MonoBehaviour, IAffectable
    {
        [SerializeField] CharacterBio bio;

        [SerializeField] StatusSet baseStatus;
        List<StatusSet> statusChanges;

        [SerializeField] ModifyStatus skill;

        public StatusHolder<int, StatusOfType<int>> GetStatus(StatusOfType<int> desiredStatus)
        {
            var resultHolder = new StatusHolder<int, StatusOfType<int>>(desiredStatus);

            SumHolders(ref resultHolder, baseStatus.GetStatus(desiredStatus));
            foreach(var set in statusChanges)
                SumHolders(ref resultHolder, set.GetStatus(desiredStatus));

            return resultHolder;

            void SumHolders(ref StatusHolder<int, StatusOfType<int>> baseHolder, StatusHolder<int, StatusOfType<int>> targetHolder)
            {
                if (targetHolder != null) baseHolder.Value += targetHolder.Value;
            }
        }
        public StatusHolder<float, StatusOfType<float>> GetStatus(StatusOfType<float> desiredStatus)
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