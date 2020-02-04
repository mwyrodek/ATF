using System;

namespace CalculatorOperationChain.CalulatorNodes
{
    [Serializable]
    public class CalulatorValueNode : ICalulatorNode
    {
        public decimal Value;

        public int Index { get; set; }

        public CalulatorValueNode(decimal value, int index)
        {
            Value = value;
            Index = index;
        }
    }
}