namespace CalculatorOperationChain
{
    public interface ICalculatorBuilder
    {
        ICalculatorBuilder AddValue(decimal Value);
        ICalculatorBuilder AddSymbol(MathToken token);
        ICalculatorBuilder AddNode(ICalulatorNode node);
        decimal Calculate();
        void Clear();

    }
}