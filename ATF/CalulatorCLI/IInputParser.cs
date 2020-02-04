using System.Collections.Generic;
using CalculatorOperationChain;

namespace CalulatorCLI
{
    public interface IInputParser
    {
        List<ICalulatorNode> Parse(string input);
    }
}