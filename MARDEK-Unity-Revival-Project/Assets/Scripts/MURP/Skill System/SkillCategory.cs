using System.Collections.Generic;
using UnityEngine;

namespace MURP.SkillSystem
{
    [CreateAssetMenu(menuName ="MURP/SkillSystem/SkillCategory")]
    public class SkillCategory : ScriptableObject
    {
        [SerializeField] string _description;
        [SerializeField] Sprite _sprite;
        [SerializeField] bool _active;
        [SerializeField] List<Skill> _skills;

        public string description { get { return _description; } }

        public Sprite sprite { get { return _sprite; } }

        public bool isActive { get { return _active; } }

        public List<Skill> skills { get { return _skills; } }
    }
}