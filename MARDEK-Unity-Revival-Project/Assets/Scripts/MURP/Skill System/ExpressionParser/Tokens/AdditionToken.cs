using MURP.StatsSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.SkillSystem.ExpressionParser
{
    public class AdditionToken : BranchParserToken
    {
        public override float Evaluate(IStats user, IStats target)
        {
            var leftValue = left == null ? 0 : left.Evaluate(user, target);
            var rightValue = right == null ? 0 : right.Evaluate(user, target);
            //Debug.Log($"Evaluating {leftValue} plus {rightValue}");
            return leftValue + rightValue;
        }
    }
}