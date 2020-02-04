using System;
using System.Collections.Generic;
using System.Linq;
using Calculator;
using CalculatorOperationChain;
using NUnit.Framework;

namespace CalulatorOpertaionChainTest
{
    [TestFixture]
    public class Tests
    {

         private ICalculatorBuilder _sut;

        //[OneTimeSetUp]
        [SetUp]
        public void SetUP()
        {
            _sut = new CalculatorBuilder(new BasicMath());
        }
        
        [Test]
        public void OnlyAddingOneTenTimes()
        {
            Console.WriteLine("Math  1+1+1+1+1+1+1+1+1+1=10");
            var calculate = _sut
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(1)
                .Calculate();
            Assert.AreEqual(10m,calculate);
        }
        
        
        [Test]
        public void SimpleBracesTest()
        {
            Console.WriteLine("Math  1-(1+2)=-2");
            var calculate = _sut
                .AddValue(1)
                .AddSymbol(MathToken.Subtract)
                .AddSymbol(MathToken.OpenBrackets)
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(2)
                .AddSymbol(MathToken.CloseBrackets)
                
                .Calculate();
            Assert.AreEqual(-2m,calculate);
        }
        
        [Test]
        public void SimpleBracesTest_LongerBracketQuery()
        {
            Console.WriteLine("Math  1-(1+2+2)=-5");
            var calculate = _sut
                .AddValue(1)
                .AddSymbol(MathToken.Subtract)
                .AddSymbol(MathToken.OpenBrackets)
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(2)
                .AddSymbol(MathToken.Add)
                .AddValue(2)
                .AddSymbol(MathToken.CloseBrackets)
                
                .Calculate();
            Assert.AreEqual(-4m,calculate);
        }
        
        
        [Test]
        public void SimpleBracesTest_BracketsInBrackets()
        {
            Console.WriteLine("Math  1-(1-(2+1))=3");
            var calculate = _sut
                .AddValue(1)
                .AddSymbol(MathToken.Subtract)
                .AddSymbol(MathToken.OpenBrackets)
                .AddValue(1)
                .AddSymbol(MathToken.Subtract)
                .AddSymbol(MathToken.OpenBrackets)
                .AddValue(2)
                .AddSymbol(MathToken.Add)
                .AddValue(1)
                .AddSymbol(MathToken.CloseBrackets)
                .AddSymbol(MathToken.CloseBrackets)
                
                .Calculate();
            Assert.AreEqual(3m,calculate);
        }
        
        [Test]
        public void AddingSubTratingMixed_Expected_Zero()
        {
            var calculate = _sut
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(1)
                .AddSymbol(MathToken.Subtract)
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(1)
                .AddSymbol(MathToken.Subtract)
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(1)
                .AddSymbol(MathToken.Subtract)
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(1)
                .AddSymbol(MathToken.Subtract)
                .AddValue(1)
                .AddSymbol(MathToken.Subtract)
                .AddValue(1)
                .Calculate();
            Assert.AreEqual(calculate,0m);
        }
        
        [Test]
        public void AddingMuliplyTratingMixed()
        {
            var calculate = _sut
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(2)
                .AddSymbol(MathToken.Multiply)
                .AddValue(3)
                .AddSymbol(MathToken.Add)
                .AddValue(4)
                .AddSymbol(MathToken.Multiply)
                .AddValue(5)
                .AddSymbol(MathToken.Add)
                .AddValue(6)
                .AddSymbol(MathToken.Multiply)
                .AddValue(7)
                .AddSymbol(MathToken.Add)
                .AddValue(8)
                .AddSymbol(MathToken.Multiply)
                .AddValue(9)
                .AddSymbol(MathToken.Multiply)
                .AddValue(10)
                .Calculate();
            Assert.AreEqual(calculate,789m);
        }
        
        [Test]
        public void Braces_AddSuB()
        {
            //1+2-3+4-(5+6)-7+8-(9+10) = 
            //10+1-19
            var calculate = _sut
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(2)
                .AddSymbol(MathToken.Subtract)
                .AddValue(3)
                .AddSymbol(MathToken.Add)
                .AddValue(4)
                .AddSymbol(MathToken.Subtract)
                .AddSymbol(MathToken.OpenBrackets)
                .AddValue(5)
                .AddSymbol(MathToken.Add)
                .AddValue(6)
                .AddSymbol(MathToken.CloseBrackets)
                .AddSymbol(MathToken.Subtract)
                .AddValue(7)
                .AddSymbol(MathToken.Add)
                .AddValue(8)
                .AddSymbol(MathToken.Subtract)
                .AddSymbol(MathToken.OpenBrackets)
                .AddValue(9)
                .AddSymbol(MathToken.Add)
                .AddValue(10)
                .AddSymbol(MathToken.CloseBrackets)
                .Calculate();
            Assert.AreEqual(calculate,-25m);
        }
        
        [Test]
        public void Braces_AddMultiply_NoBrackets()
        {
            //1+2*3*4+5*6
            var calculate = _sut
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddValue(2)
                .AddSymbol(MathToken.Multiply)
                .AddValue(3)
                .AddSymbol(MathToken.Multiply)
                .AddValue(4)
                .AddSymbol(MathToken.Add)
                .AddValue(5)
                .AddSymbol(MathToken.Multiply)
                .AddValue(6)
                .Calculate();
            Assert.AreEqual(calculate,55m);
        }
        
        [Test]
        public void Braces_AddMultiply_WithBrackets()
        {
            //1 + (2 *3) - 4 * (5 + 6) - 7 *8+ 9- 10
            var calculate = _sut
                .AddValue(1)
                .AddSymbol(MathToken.Add)
                .AddSymbol(MathToken.OpenBrackets)
                .AddValue(2)
                .AddSymbol(MathToken.Multiply)
                .AddValue(3)
                .AddSymbol(MathToken.CloseBrackets)
                .AddSymbol(MathToken.Subtract)
                .AddValue(4)
                .AddSymbol(MathToken.Multiply)
                .AddSymbol(MathToken.OpenBrackets)
                .AddValue(5)
                .AddSymbol(MathToken.Add)
                .AddValue(6)
                .AddSymbol(MathToken.CloseBrackets)
                .AddSymbol(MathToken.Subtract)
                .AddValue(7)
                .AddSymbol(MathToken.Multiply)
                .AddValue(8)
                .AddSymbol(MathToken.Add)
                .AddValue(9)
                .AddSymbol(MathToken.Subtract)
                .AddValue(10)
                .Calculate();
            Assert.AreEqual(calculate,-94m);
        }
    }
}