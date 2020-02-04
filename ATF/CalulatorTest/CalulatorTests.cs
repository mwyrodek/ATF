using System;
using Calculator;
using Calculator.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalulatorTest
{
    [TestClass]
    public class CalulatorTests
    {
        IBasicMath sut = new BasicMath();
        [DataTestMethod]
        [DataRow(1,2, 3)]
        [DataRow(2,1, 3)]
        [DataRow(1.1,1.1, 2.2)]
        [DataRow(-1.1,1.1, 0)]
        [DataRow(0.9,-1.1, -0.2)]
        public void AddTests(double A, double B, double expectedResult)
        {
            var decA = Convert.ToDecimal(A);
            var decB = Convert.ToDecimal(B);
            var decResult = Convert.ToDecimal(expectedResult);
            var result = sut.Add(decA, decB);
            var resultReversed = sut.Add(decB, decA);
            Assert.AreEqual(decResult,result);
            Assert.AreEqual(resultReversed,result, "somehow A+B != B+A");
        }
        
        [DataTestMethod]
        [DataRow(1,2, -1)]
        [DataRow(2,1, 1)]
        [DataRow(1.1,1.1, -0)]
        [DataRow(-1.1,1.1, -2.2)]
        [DataRow(0.9,-1.1, 2.0)]
        public void SubTests(double A, double B, double expectedResult)
        {
            var decA = Convert.ToDecimal(A);
            var decB = Convert.ToDecimal(B);
            var decResult = Convert.ToDecimal(expectedResult);
            var result = sut.Sub(decA, decB);
            
            Assert.AreEqual(decResult,result);
        }
        
        [DataTestMethod]
        [DataRow(1,2, 0.5)]
        [DataRow(2,1, 2)]
        [DataRow(1.1,1.1, 1)]
        [DataRow(-1.1,1.1, -1)]
        [DataRow(0.9,-2.0, -0.45)]
        public void DivTests(double A, double B, double expectedResult)
        {
            var decA = Convert.ToDecimal(A);
            var decB = Convert.ToDecimal(B);
            var decResult = Convert.ToDecimal(expectedResult);
            var result = sut.Divide(decA, decB);
            
            Assert.AreEqual(decResult,result);
        }
        
        [DataTestMethod]
        [DataRow(1,2, 2)]
        [DataRow(2,1, 2)]
        [DataRow(1.1,1.1, 1.21)]
        [DataRow(-1.1,1.1, -1.21)]
        [DataRow(0.9,-1.1, -0.99)]
        [DataRow(-2,-5, 10.0)]
        public void MultiplyTests(double A, double B, double expectedResult)
        {
            var decA = Convert.ToDecimal(A);
            var decB = Convert.ToDecimal(B);
            var decResult = Convert.ToDecimal(expectedResult);
            var result = sut.Multiply(decA, decB);
            var resultRev = sut.Multiply(decB, decA);
            
            Assert.AreEqual(decResult,result);
            Assert.AreEqual(resultRev,result, "Some how A*B != B*A");
        }
    }
}
