//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace UnitTestProject.Tests
//{
//    [TestClass]
//    public class CalculatorTest
//    {
//        [TestInitialize]
//        public void Initialize()
//        {
//            // Used for Initializing before each time.
//        }

//        [TestMethod]
//        public void TestAddNumbersPositive()
//        {
//            //Arrange
//            var calc = new UnitTestProject.Calculator();

//            //Act
//            int result = calc.AddMembers(1, 2);

//            //Assert
//            Assert.AreEqual(expected: 3, actual: result);
//        }

//          [TestMethod]
//        public void TestAddNumberZero()
//        {
//            //Arrange
//            var calc = new UnitTestProject.Calculator();

//            //Act
//            int result = calc.AddMembers(0, 0);

//            //Assert
//            Assert.AreEqual(expected: 0, actual: 0);
//        }

//          [TestMethod]
//          public void TestAddNumberNegative()
//          {
//              //Arrange
//              var calc = new UnitTestProject.Calculator();

//              //Act
//              int result = calc.AddMembers(-1, -2);

//              //Assert
//              Assert.AreEqual(expected: -3, actual: result);
//          }

//          [TestMethod]
//          public void TestAddNumberOneNegative()
//          {
//              //Arrange
//              var calc = new UnitTestProject.Calculator();

//              //Act
//              int result = calc.AddMembers(-1,  2);

//              //Assert
//              Assert.AreEqual(expected:  1, actual: result);
//          }

//          [TestMethod]
//          public void TestAddNumberMaximum()
//          {
//              //Arrange
//              var calc = new UnitTestProject.Calculator();

//              //Act
//              int result = calc.AddMembers(int.MaxValue, int.MaxValue);

//              //Assert
//              Assert.AreEqual(expected: -2, actual: result);  ///Weird Unexpected Value
//          }
//    }
//}
