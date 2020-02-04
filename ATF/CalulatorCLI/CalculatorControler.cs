using System;
using CalculatorOperationChain;

namespace CalulatorCLI
{
    public class CalculatorControler
    {
        private ICalculatorBuilder Builder;
        private IInputParser parser;

        public CalculatorControler(ICalculatorBuilder builder, IInputParser parser)
        {
            Builder = builder;
            this.parser = parser;
        }
            
        public  string Calculate(string input)
        {
            var calulatorNodes = parser.Parse(input);
            if (calulatorNodes.Count <= 2)
            {
                return "not real math";
            }
            foreach (var calculatorNode in calulatorNodes)
            {
                Builder.AddNode(calculatorNode);
            }

            var calculate = Builder.Calculate().ToString();
            Builder.Clear();
            return calculate;
        }
    }
}