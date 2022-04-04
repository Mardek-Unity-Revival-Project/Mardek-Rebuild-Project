using System.Collections.Generic;
using UnityEngine;

namespace MURP.SkillSystem
{
    [CreateAssetMenu(menuName ="MURP/SkillSystem")]
    public class Skillset : ScriptableObject
    {
        [SerializeField] List<Skill> _skills;
        [SerializeField] string _description;
        [SerializeField] Sprite _sprite;
        [SerializeField] bool _active;

        public string description { get { return _description; } }

        public Sprite sprite { get { return _sprite; } }

        public bool isActive { get { return _active; } }

        public List<Skill> skills { get { return _skills; } }
    }
}