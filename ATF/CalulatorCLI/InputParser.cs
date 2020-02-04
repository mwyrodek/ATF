using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CalculatorOperationChain;
using CalculatorOperationChain.CalulatorNodes;

namespace CalulatorCLI
{
    public class InputParser : IInputParser
    {
        public List<ICalulatorNode> Parse(string input)
        {
            var calulatorNodes = new List<ICalulatorNode>();
            //macthes both : and / as divide symbol
            string Pattern = @"[\d]+|[\*\+\(\)\-\/\:]";
            Regex rex = new Regex(Pattern,
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rex.Matches(input);
            int index = 0;
            foreach (Match match in matches)
            {
                int parsedresult;
                var tryParse = int.TryParse(match.Value,out parsedresult);
                if (tryParse)
                {
                    if (CheckIFMinus(calulatorNodes))
                    {
                        calulatorNodes[index - 1] = new CalulatorValueNode(-parsedresult, index - 1);
                    }
                    else
                    {
                        calulatorNodes.Add(new CalulatorValueNode(parsedresult, index));
                        index++;                        
                    }
                }
                else
                {
                    MathToken token = ParseToken(match.Value);
                    calulatorNodes.Add(new CalculatorOperationNode(token, index));
                    index++;
                }
            }

            return calulatorNodes;
        }

        private bool CheckIFMinus(List<ICalulatorNode> calulatorNodes)
        {
            var calulatorNodesCount = calulatorNodes.Count;
            if (calulatorNodesCount==0)
            {
                return false;
            }
            if (calulatorNodesCount==1&& calulatorNodes.OfType<CalculatorOperationNode>().Last().Token==MathToken.Subtract)
            {
                return true;
            }
            
            if(calulatorNodes.OfType<CalculatorOperationNode>().Last().Index == calulatorNodesCount - 1 && calulatorNodes.OfType<CalculatorOperationNode>().Last().Token==MathToken.Subtract)
            {
                return  calulatorNodes.OfType<CalculatorOperationNode>().Any(node => node.Index == calulatorNodesCount - 2);
            }
            return false;
        }

        private MathToken ParseToken(string matchValue)
        {
            switch (matchValue)
            {
                case "+":
                    return MathToken.Add;
                case "-":
                    return MathToken.Subtract;
                case "*":
                    return MathToken.Multiply;
                case ":":
                case "/":
                    return MathToken.Divide;
                case "(":
                    return MathToken.OpenBrackets;
                case ")":
                    return MathToken.CloseBrackets;
                default:
                    throw new ArgumentOutOfRangeException($"Symbol {matchValue} is not recognized from begining");
            }
        }
    }
}