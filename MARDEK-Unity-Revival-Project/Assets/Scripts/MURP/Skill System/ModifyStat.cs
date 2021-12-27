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
            // TODO: modify the stat
            //var statusHolder = target.GetStat(targetStatus);
            Debug.Log($"{user} should deal {valueExpresion.Evaluate(user, target)} damage to {target}'s {targetStatus}");
        }
    }
}