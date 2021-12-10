using UnityEngine;
using MURP.StatusSystem;

namespace MURP.CharacterSystem
{
    [System.Serializable]
    public class Character : MonoBehaviour, IAffectable
    {
        [SerializeField] CharacterBio bio;
        [SerializeField] CharacterStatusSet baseStatus;
        [SerializeField] ModifyStatusEffect skill;

        public StatusHolder<int, StatusOfType<int>> GetStatus(StatusOfType<int> desiredStatus)
        {
            return baseStatus.GetStatus(desiredStatus);
        }
        public StatusHolder<float, StatusOfType<float>> GetStatus(StatusOfType<float> desiredStatus)
        {
            return baseStatus.GetStatus(desiredStatus);
        }
        
        [ContextMenu("TriggerSkill")]
        void TriggerSkill()
        {
            skill.Apply(this, this);
        }
    }
}