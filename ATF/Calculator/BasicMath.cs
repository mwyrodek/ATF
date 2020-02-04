using System;
using Calculator.Properties;

namespace Calculator
{
    public class BasicMath : IBasicMath
    {
        
        // add
        // subtract
        // divide
        // multiply
        // operation order
        // braces only ()


        public decimal Add(decimal basicValue, decimal addedValue)
        {
            return basicValue + addedValue;
        }

        public decimal Sub(decimal basicValue, decimal removedValue)
        {
            return basicValue - removedValue;
        }

        public decimal Divide(decimal basicValue, decimal divider)
        {
            if (divider == 0)
            {
                throw new ArgumentException("Divder can't be zero!");
            }
            return basicValue / divider;
        }

        public decimal Multiply(decimal basicValue, decimal multiplier)
        {
            return basicValue * multiplier;
        }
    }
}