namespace Calculator.Properties
{
    public interface IBasicMath
    {
        decimal Add(decimal basicValue, decimal addedValue);
        decimal Sub(decimal basicValue, decimal removedValue);
        decimal Divide(decimal basicValue, decimal divider);
        decimal Multiply(decimal basicValue, decimal multiplier);
    }
}