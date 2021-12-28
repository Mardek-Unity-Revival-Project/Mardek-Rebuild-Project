using UnityEngine;
using MURP.Core;
using MURP.StatsSystem;

namespace MURP.SkillSystem
{
    [CreateAssetMenu(menuName = "MURP/SkillSystem/SkillEffects/ModifyStats")]
    public class ModifyStat : SkillEffect
    {
        [SerializeField] IntegerStat targetStatus; // most probably the current health stat, but who knows
        [SerializeField] SkillExpression valueExpresion = new SkillExpression();

        public override void Apply(IStats user, IStats target)
        {
            var value = -valueExpresion.Evaluate(user, target);
            target.ModifyStat(targetStatus, value);
        }
    }
}