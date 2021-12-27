using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.StatsSystem;

namespace MURP.SkillSystem
{
    [CreateAssetMenu(menuName ="MURP/SkillSystem/Skill")]
    public class Skill : ScriptableObject
    {
        [SerializeField] List<SkillEffect> effects = new List<SkillEffect>();

        public void Apply(IStats user, IStats target)
        {
            foreach (var effect in effects)
                effect.Apply(user, target);
        }
    }
}