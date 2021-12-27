using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.SkillSystem.ExpressionParser
{
    public class DivisionToken : LeftmostDerivationToken
    {
        public override float Evaluate()
        {
            var leftValue = left == null ? 0 : left.Evaluate();
            var rightValue = right == null ? 0 : right.Evaluate();
            Debug.Log($"Evaluating {leftValue} divided by {rightValue}");
            return leftValue / rightValue;
        }
    }
}
