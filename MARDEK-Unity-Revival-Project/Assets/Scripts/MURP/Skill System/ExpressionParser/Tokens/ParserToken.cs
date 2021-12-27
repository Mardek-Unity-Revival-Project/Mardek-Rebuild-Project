using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.StatsSystem;

namespace MURP.SkillSystem.ExpressionParser
{
    public abstract class ParserToken
    {
        public abstract float Evaluate(IStats user, IStats target);
    }
}