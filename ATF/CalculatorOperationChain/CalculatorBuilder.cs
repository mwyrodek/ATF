using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Calculator.Properties;
using CalculatorOperationChain.CalulatorNodes;

namespace CalculatorOperationChain
{
    public class CalculatorBuilder : ICalculatorBuilder
    {
        private IBasicMath math;
        private List<ICalulatorNode> _calculatorNodes;

        public CalculatorBuilder(IBasicMath math)
        {
            this.math = math;
            this._calculatorNodes = new List<ICalulatorNode>();
        }

        public ICalculatorBuilder AddValue(decimal Value)
        {
            _calculatorNodes.Add(new CalulatorValueNode(Value, _calculatorNodes.Count));
            return this;
        }

        public ICalculatorBuilder AddSymbol(MathToken token)
        {
            _calculatorNodes.Add(new CalculatorOperationNode(token, _calculatorNodes.Count));
            return this;
        }

        public ICalculatorBuilder AddNode(ICalulatorNode node)
        {
            _calculatorNodes.Add(node);
            return this;
        }


        public decimal Calculate()
        {
            return Calculate(_calculatorNodes);
        }

        public void Clear()
        {
            this._calculatorNodes = new List<ICalulatorNode>();
        }

        private decimal Calculate(List<ICalulatorNode> opreationList)
        {
            if (opreationList.OfType<CalculatorOperationNode>().Any(el => el.Token == MathToken.OpenBrackets))
            {
                CalculateBraces(opreationList);
            }



            while (opreationList.OfType<CalculatorOperationNode>().Any(on =>
                on.Token == MathToken.Multiply || on.Token == MathToken.Divide))
            {
                RecalculateIndex(opreationList);
                var calculatorOperationNode = opreationList.OfType<CalculatorOperationNode>().First(on =>
                    @on.Token == MathToken.Multiply || @on.Token == MathToken.Divide);
                var Index = calculatorOperationNode.Index;
                var firstValue = ((CalulatorValueNode) opreationList[Index - 1]).Value;
                var operationValue = ((CalulatorValueNode) opreationList[calculatorOperationNode.Index + 1]).Value;

                firstValue = PerformOperation(firstValue, calculatorOperationNode.Token, operationValue);
                opreationList.RemoveAt(Index + 1);
                opreationList.RemoveAt(Index);
                opreationList[Index - 1] = new CalulatorValueNode(firstValue, Index - 1);
                RecalculateIndex(opreationList);
            }
            var result = ((CalulatorValueNode) opreationList[0]).Value;
            opreationList.RemoveAt(0);
            while (opreationList.Count != 0)
            {
                var operation = ((CalculatorOperationNode) opreationList[0]).Token;
                var operationValue = ((CalulatorValueNode) opreationList[1]).Value;
                result = PerformOperation(result, operation, operationValue);
                opreationList.RemoveAt(0);
                opreationList.RemoveAt(0);
            }

            return result;
        }

        private void CalculateBraces(List<ICalulatorNode> opreationList)
        {
            while (opreationList.OfType<CalculatorOperationNode>().Any(el => el.Token == MathToken.OpenBrackets))
            {
                var tempolaryNodeClones = opreationList.DeepClone();
                var indexsOfOpeningBraces = GetIndexs(tempolaryNodeClones, MathToken.OpenBrackets);
                var indexsOfClosingBraces = GetIndexs(tempolaryNodeClones, MathToken.CloseBrackets);

                BracesDistances shortestDistance;
                if (indexsOfClosingBraces.Count > 1 && indexsOfOpeningBraces.Count > 1)
                {
                    shortestDistance = FindShortestDistance(indexsOfOpeningBraces, indexsOfClosingBraces);
                    //+1 and -1 because we dont want to get braces;
                }
                else
                {
                    shortestDistance = new BracesDistances(indexsOfOpeningBraces.First(), indexsOfClosingBraces.First(),
                        indexsOfClosingBraces.First() - indexsOfOpeningBraces.First());
                }

                var calulatorNodes = tempolaryNodeClones.GetRange(shortestDistance.OpeningBraceIndex + 1,
                    shortestDistance.Distance - 1);
                //perform operation
                var calculate = Calculate(calulatorNodes);
                opreationList[shortestDistance.OpeningBraceIndex] =
                    new CalulatorValueNode(calculate, shortestDistance.OpeningBraceIndex);
                opreationList.RemoveRange(shortestDistance.OpeningBraceIndex + 1, shortestDistance.Distance);
                RecalculateIndex(opreationList);
            }
        }

        private void RecalculateIndex(IList<ICalulatorNode> operationNodes)
        {
            for (int i = 0; i < operationNodes.Count; i++)
            {
                operationNodes[i].Index = i;
            }
        }

        //this need it own class and unit tests
        private BracesDistances FindShortestDistance(List<int> indexsOfOpeningBraces, List<int> indexOfClosingBraces)
        {
            //opening value, lowest distance
            var bracesDistanceses = new List<BracesDistances>();
            foreach (var closingBrace in indexOfClosingBraces)
            {
                //stolen from https://stackoverflow.com/a/5953818
                int closest = indexsOfOpeningBraces.Where(item => (closingBrace - item) > 0)
                    .OrderBy(item => Math.Abs(closingBrace - item)).First();

                bracesDistanceses.Add(new BracesDistances(closest, closingBrace, closingBrace - closest));
            }

            return bracesDistanceses.OrderBy(bd => bd.Distance).First();
        }

        private List<int> GetIndexs(List<ICalulatorNode> calculatorOperationNodes, MathToken token)
        {
            var indexsOfElement = new List<int>();

            while (calculatorOperationNodes.OfType<CalculatorOperationNode>().Any(e => e.Token == token))
            {
                var index = calculatorOperationNodes.OfType<CalculatorOperationNode>().First(el => el.Token == token)
                    .Index;
                indexsOfElement.Add(index);
                ((CalculatorOperationNode) calculatorOperationNodes[index]).Token = MathToken.Blank;
            }

            return indexsOfElement;
        }

        private decimal PerformOperation(decimal result, MathToken operation, decimal operationValue)
        {
            switch (operation)
            {
                case MathToken.Add:
                    result = math.Add(result, operationValue);
                    break;
                case MathToken.Subtract:
                    result = math.Sub(result, operationValue);
                    break;
                case MathToken.Multiply:
                    result = math.Multiply(result, operationValue);
                    break;
                case MathToken.Divide:
                    result = math.Divide(result, operationValue);
                    break;
            }

            return result;
        }
    }
}