using System.Collections.Generic;
using UnityEngine;

namespace MURP.SkillSystem
{
    [CreateAssetMenu(menuName ="MURP/SkillSystem/ActiveSkillSet")]
    public class ActiveSkillSet : ScriptableObject
    {
        [SerializeField] string _displayName;
        [SerializeField] string _description;
        [SerializeField] Sprite _sprite;
        [SerializeField] List<Skill> _skills;

        public string displayName { get { return _displayName; } }

        public string description { get { return _description; } }

        public Sprite sprite { get { return _sprite; } }

        public List<Skill> skills { get { return _skills; } }
    }
}