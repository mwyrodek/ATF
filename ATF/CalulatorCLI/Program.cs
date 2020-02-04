using System;
using Calculator;
using CalculatorOperationChain;

namespace CalulatorCLI
{
    class Program
    {
        //struct runprogram
        //which will run cli class
        //which will translate input into request to system.
        // option
        // Q - in string quites app
        //
        // system:
        //ui
        // parser
        // lexer
        static void Main(string[] args)
        {
            var basicMath = new BasicMath();
            var calculatorBuilder = new CalculatorBuilder(basicMath);
            var inputParser = new InputParser();
            var calculatorControler = new CalculatorControler(calculatorBuilder, inputParser);
            while (true)
            {
                Console.WriteLine("Hello World! Give input the click Enter");
                Console.WriteLine("App will give you result of your math query");
                Console.WriteLine("If Querry has Q in it will close the app.");
                var input = Console.ReadLine();
                if (input.ToLower().Contains("q"))
                {
                    Console.WriteLine("closing app press any key");
                    Console.ReadKey();
                    return;
                }

                var result = calculatorControler.Calculate(input);
                Console.WriteLine($"operation result is: {result}");
            }
        }
    }
}