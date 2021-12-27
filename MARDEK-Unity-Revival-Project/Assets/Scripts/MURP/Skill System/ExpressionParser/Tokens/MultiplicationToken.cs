using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.SkillSystem.ExpressionParser
{
    public class MultiplicationToken : BranchParserToken
    {
        public override float Evaluate()
        {
            var leftValue = left.Evaluate();
            var rightValue = right.Evaluate();
            Debug.Log($"Evaluating {leftValue} multiplied by {rightValue}");
            return leftValue * rightValue;
        }
    }
}