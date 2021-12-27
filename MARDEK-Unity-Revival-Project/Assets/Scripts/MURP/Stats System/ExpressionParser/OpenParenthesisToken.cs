using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.StatsSystem
{
    public class OpenParenthesisToken : ParserToken
    {
        public ParserToken internalToken = null;

        public override float Evaluate()
        {
            Debug.Log("Evaluating Parenthesis");
            return internalToken.Evaluate();
        }
    }
}