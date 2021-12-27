using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.SkillSystem.ExpressionParser
{
    public class LiteralToken : ValueToken
    {
        float _value = 0;

        public LiteralToken(float value)
        {
            _value = value;
        }

        public override float Evaluate()
        {
            return _value;
        }
    }
}