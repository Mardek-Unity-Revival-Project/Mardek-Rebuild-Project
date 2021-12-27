using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.SkillSystem.ExpressionParser;

namespace MURP.SkillSystem
{
    public class SkillExpression : MonoBehaviour
    {
        [SerializeField] string expression;
        ParserToken finalToken = null;
        int index = 0;
        string[] lexicalParts;

        private void Start()
        {
            
        }

        public float Evaluate()
        {
            if (finalToken == null)
                Parse();
            return finalToken.Evaluate();
        }

        [ContextMenu("Parse")]
        void Parse()
        {
            lexicalParts = expression.Split(' ');
            index = 0;
            var tree = new Stack<ParserToken>();
            tree.Push(LookAhead());
            // loop through all lexical parts
            while(index < lexicalParts.Length)
            {
                var token = LookAhead();

                var tokenAsBranch = token as BranchParserToken;
                if (tokenAsBranch != null)
                    tokenAsBranch.left = tree.Pop();

                if (token is CloseParenthesisToken)
                {                   
                    while (tree.Count > 1) //rightmost derivate till open parentesis is found
                    {
                        var last = tree.Pop();
                        var secondToLast = tree.Peek();
                        if (secondToLast is BranchParserToken)
                            (secondToLast as BranchParserToken).right = last;
                        if (secondToLast is OpenParenthesisToken)
                        {
                            (secondToLast as OpenParenthesisToken).internalToken = last;
                            break;
                        }
                    }
                    // if the token to the left of the open parenthesis token is a leftmost derivation it already got
                    // associated with the open parentheiys, thus we shouldn't keep the open parenthesis in the tree stack
                    if (tree.Count > 1)
                    {                        
                        var openParentesisToken = tree.Pop();                        
                        if (tree.Peek() is LeftmostDerivationToken == false)
                            tree.Push(openParentesisToken);
                    }
                    continue;
                }

                // leftmost derivation in special cases
                if (tree.Count > 0)
                {
                    var tokenAsLeftmostDerivation = tree.Peek() as LeftmostDerivationToken;
                    if (tokenAsLeftmostDerivation != null)
                    {
                        tokenAsLeftmostDerivation.right = token;
                        // don't add the value token to stack 'cause it was associated with the leftmost derivation token
                        if (token is ValueToken)
                            continue;
                    }
                }
                tree.Push(token);
            }
            // final rightmost derivation
            while (tree.Count > 1)
            {
                var last = tree.Pop();
                var secondToLast = tree.Peek() as BranchParserToken;
                if(secondToLast != null && secondToLast.right == null)
                    secondToLast.right = last;
            }
            finalToken = tree.Pop();
        }

        ParserToken LookAhead()
        {
            var lexicalPart = lexicalParts[index].Replace(" ", "");
            index++;
            var isNumeric = float.TryParse(lexicalPart, out var value);
            if (isNumeric)
                return new LiteralToken(value);
            if (lexicalPart == "+")
                return new AdditionToken();
            if (lexicalPart == "-")
                return new SubtractionToken();
            if (lexicalPart == "*")
                return new MultiplicationToken();
            if (lexicalPart == "/")
                return new DivisionToken();
            if (lexicalPart == "(")
                return new OpenParenthesisToken();
            if (lexicalPart == ")")
                return new CloseParenthesisToken();            
            return null;
        }
    }
}
