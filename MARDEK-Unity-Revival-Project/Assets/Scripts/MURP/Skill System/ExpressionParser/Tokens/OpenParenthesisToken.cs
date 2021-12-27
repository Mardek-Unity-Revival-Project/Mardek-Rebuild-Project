using MURP.StatsSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.SkillSystem.ExpressionParser
{
    public class OpenParenthesisToken : ParserToken
    {
        public ParserToken internalToken = null;

        public override float Evaluate(IStats user, IStats target)
        {
            return internalToken.Evaluate(user, target);
        }
    }
}