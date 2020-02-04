using System;

namespace CalculatorOperationChain.CalulatorNodes
{
    [Serializable]
    public class CalculatorOperationNode : ICalulatorNode
    {
        public MathToken Token;

        public int Index { get; set; }

        public CalculatorOperationNode(MathToken token, int index)
        {
            Token = token;
            Index = index;
        }
    }
}