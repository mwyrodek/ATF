using System;
using System.Linq;
using Calculator;
using CalculatorOperationChain;
using CalculatorOperationChain.CalulatorNodes;
using CalulatorCLI;
using NUnit.Framework;

namespace CalculatorCLITest
{
    [TestFixture]
    public class Tests
    {
        private IInputParser sut;
     
        [SetUp]
        public void SetUp()
        {
            sut = new InputParser();
        }
        
        //1+1
        [TestCase("1+1", MathToken.Add)]
        [TestCase("1 +1", MathToken.Add)]
        [TestCase("1-1", MathToken.Subtract)]
        [TestCase("1*1", MathToken.Multiply)]
        [TestCase("1/1", MathToken.Divide)]
        public void MathSymbols(string input, MathToken token)
        {
            var calulatorNodes = sut.Parse(input);
            Assert.That(calulatorNodes[0].Index, Is.EqualTo(0));
            Assert.That(((CalulatorValueNode)calulatorNodes[0]).Value, Is.EqualTo(1m));
            Assert.That(calulatorNodes[1].Index, Is.EqualTo(1));
            Assert.That(((CalculatorOperationNode)calulatorNodes[1]).Token, Is.EqualTo(token));
            Assert.That(calulatorNodes[2].Index, Is.EqualTo(2));
            Assert.That(((CalulatorValueNode)calulatorNodes[2]).Value, Is.EqualTo(1m));
        }
        
        [TestCase("-1+1", MathToken.Add)]
        public void MinusValueAtStart(string input, MathToken token)
        {
            var calulatorNodes = sut.Parse(input);
            Assert.That(calulatorNodes[0].Index, Is.EqualTo(0));
            Assert.That(((CalulatorValueNode)calulatorNodes[0]).Value, Is.EqualTo(-1m));
        }
        [TestCase()]
        public void UnkownSymbolIsSkiped()
        {
            string input = "1a+%1";
            var calulatorNodes = sut.Parse(input);
            Assert.That(calulatorNodes[0].Index, Is.EqualTo(0));
            Assert.That(((CalulatorValueNode)calulatorNodes[0]).Value, Is.EqualTo(1m));
            Assert.That(calulatorNodes[1].Index, Is.EqualTo(1));
            Assert.That(((CalculatorOperationNode)calulatorNodes[1]).Token, Is.EqualTo(MathToken.Add));
            Assert.That(calulatorNodes[2].Index, Is.EqualTo(2));
            Assert.That(((CalulatorValueNode)calulatorNodes[2]).Value, Is.EqualTo(1m));
        }
        
        [TestCase()]
        public void Brackets()
        {
            string input = "1+(2+3)";
            var calulatorNodes = sut.Parse(input);
            Assert.That(((CalulatorValueNode)calulatorNodes[0]).Value, Is.EqualTo(1m));
            Assert.That(((CalculatorOperationNode)calulatorNodes[1]).Token, Is.EqualTo(MathToken.Add));
            Assert.That(((CalculatorOperationNode)calulatorNodes[2]).Token, Is.EqualTo(MathToken.OpenBrackets));
            Assert.That(((CalulatorValueNode)calulatorNodes[3]).Value, Is.EqualTo(2m));
            Assert.That(((CalculatorOperationNode)calulatorNodes[4]).Token, Is.EqualTo(MathToken.Add));
            Assert.That(((CalulatorValueNode)calulatorNodes[5]).Value, Is.EqualTo(3m));
            Assert.That(((CalculatorOperationNode)calulatorNodes[6]).Token, Is.EqualTo(MathToken.CloseBrackets));
        }
    }


}