using MURP.StatsSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.SkillSystem.ExpressionParser
{
    public class MultiplicationToken : LeftmostDerivationToken
    {
        public override float Evaluate(IStats user, IStats target)
        {
            var leftValue = left.Evaluate(user, target);
            var rightValue = right.Evaluate(user, target);
            //Debug.Log($"Evaluating {leftValue} multiplied by {rightValue}");
            return leftValue * rightValue;
        }
    }
}