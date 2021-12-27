using MURP.StatsSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.SkillSystem.ExpressionParser
{
    public class CloseParenthesisToken : ParserToken
    {
        public override float Evaluate(IStats user, IStats target)
        {
            throw new System.NotImplementedException();
        }
    }
}