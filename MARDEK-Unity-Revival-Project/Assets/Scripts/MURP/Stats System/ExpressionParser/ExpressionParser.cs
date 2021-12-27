using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.StatsSystem
{
    public class ExpressionParser : MonoBehaviour
    {
        public void Start()
        {
            Parse();
        }

        [SerializeField] string expression;
        ParserToken finalToken = null;
        int index = 0;
        string[] lexicalParts;

        [ContextMenu("Parse")]
        void Parse()
        { 
            lexicalParts = expression.Split(' ');
            index = 0;
            var tree = new Stack<ParserToken>();
            tree.Push(LookAhead());
            while(index < lexicalParts.Length)
            {
                var token = LookAhead();
                if(token is AdditionToken)
                    (token as AdditionToken).left = tree.Pop();
                if (token is DivisionToken)
                    (token as DivisionToken).left = tree.Pop();
                if(token is CloseParenthesisToken)
                {
                    foreach (var a in tree)
                        Debug.Log(a);
                    //rightmost derivation till open parentesis is found
                    while (tree.Count > 1)
                    {
                        var last = tree.Pop();
                        var secondToLast = tree.Peek();
                        if (secondToLast is OpenParenthesisToken)
                        {
                            Debug.Log($" {last} is internal to {secondToLast}");
                            (secondToLast as OpenParenthesisToken).internalToken = last;
                            break;
                        }
                        if (secondToLast is BranchParserToken)
                        {
                            Debug.Log($" {last} is right to {secondToLast}");
                            (secondToLast as BranchParserToken).right = last;
                        }
                    }
                    if(tree.Count > 1)
                    {
                        var openToken = tree.Pop();
                        if(tree.Peek() is DivisionToken)
                        {

                        }
                        else
                        {
                            tree.Push(openToken);
                        }
                    }
                    continue;
                }

                // leftmost derivation in special cases
                if (tree.Count > 0)
                {
                    if (tree.Peek() is DivisionToken)
                    {
                        (tree.Peek() as DivisionToken).right = token;
                        if (token is LiteralToken)
                            continue;
                    }
                }
                tree.Push(token);
            }
            // rightmost derivation
            while (tree.Count > 1)
            {
                var last = tree.Pop();
                var secondToLast = tree.Peek() as BranchParserToken;
                if(secondToLast.right == null)
                    secondToLast.right = last;
            }
            finalToken = tree.Pop();
            Debug.Log(finalToken.Evaluate());
        }

        ParserToken LookAhead()
        {
            var lexicalPart = lexicalParts[index].Replace(" ", "");
            index++;
            Debug.Log(lexicalPart);
            var isNumeric = float.TryParse(lexicalPart, out var value);
            if (isNumeric)
                return new LiteralToken(value);
            if (lexicalPart == "+")
                return new AdditionToken();
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
