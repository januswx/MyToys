using System;
using NUnit.Framework;
using NUnit.Common;
using NSubstitute;
using NSubstitute.Exceptions;

namespace NSubstituteLearn
{
    /// <summary>
    /// 03.设置返回值
    /// </summary>
    [TestFixture]
    public class NUnitTest3
    {
        /// <summary>
        /// 为方法设置返回值
        /// </summary>
        [Test]
        public void Test_SettingReturnValue_ReturnsValueWithSpecifiedArguments()
        {
            var calculator = Substitute.For<ICalculator>();
            calculator.Add(1, 2).Returns(3);

            Assert.AreEqual(calculator.Add(1, 2), 3);
        }

        /// <summary>
        /// Returns() 仅会被应用于指定的参数组合，
        /// 任何使用其他参数组合对该方法的调用将会返回一个默认值
        /// </summary>
        [Test]
        public void Test_SettingReturnValue_ReturnsDefaultValueWithDifferentArguments()
        {
            var calculator = Substitute.For<ICalculator>();

            // 设置调用返回值为3
            calculator.Add(1, 2).Returns(3);

            Assert.AreEqual(calculator.Add(1, 2), 3);

            // 当使用不同参数调用时,返回值不是3,返回0
            Assert.AreNotEqual(calculator.Add(3, 6), 3);
        }

        /// <summary>
        /// 为属性设置返回值
        /// </summary>
        [Test]
        public void Test_SettingReturnValue_ReturnsValueFromProperty()
        {
            var calculator = Substitute.For<ICalculator>();

            calculator.Mode.Returns("DEC");
            Assert.AreEqual(calculator.Mode, "DEC");

            calculator.Mode = "HEX";
            Assert.AreEqual(calculator.Mode, "HEX");
        }
    }
}
