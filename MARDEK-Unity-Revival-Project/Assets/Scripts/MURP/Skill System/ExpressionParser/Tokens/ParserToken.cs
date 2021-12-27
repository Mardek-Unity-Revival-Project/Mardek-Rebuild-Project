using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.SkillSystem.ExpressionParser
{
    public abstract class ParserToken
    {
        public abstract float Evaluate();
    }
}