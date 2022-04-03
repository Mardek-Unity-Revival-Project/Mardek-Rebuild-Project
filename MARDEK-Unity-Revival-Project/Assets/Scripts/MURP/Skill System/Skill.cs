using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.StatsSystem;

namespace MURP.SkillSystem
{
    [CreateAssetMenu(menuName ="MURP/SkillSystem/Skill")]
    public class Skill : ScriptableObject
    {
        [SerializeField] string _displayName;
        [SerializeField] string _description;
        [SerializeField] int _cost;
        [SerializeField] SkillCategory _category;
        [SerializeField] Element _element;
        [SerializeField] int _masteryPoints;
        [SerializeField] IntegerStat _masteryStat;
        [SerializeField] IntegerStat _canLearnStat;
        [SerializeField] List<SkillEffect> effects = new List<SkillEffect>();

        public void Apply(IStats user, IStats target)
        {
            foreach (var effect in effects)
                effect.Apply(user, target);
        }

        public string displayName { get { return _displayName; } }

        public string description { get { return _description; } }

        // For active skills, this is the MP cost. For passive/reaction skills, this is the RP cost.
        public int cost { get { return _cost; } }

        public SkillCategory category { get { return _category; } }

        public Element element { get { return _element; } }

        public int masteryPoints { get { return _masteryPoints; } }

        public IntegerStat masteryStat { get { return _masteryStat; } }

        // While the value for this skill is larger than 0, characters can use this skill before mastering it.
        public IntegerStat canLearnStat { get { return _canLearnStat; } }
    }
}