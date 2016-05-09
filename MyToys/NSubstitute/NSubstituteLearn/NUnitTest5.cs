using System;
using NUnit.Framework;
using NUnit.Common;
using NSubstitute;
using NSubstitute.Exceptions;

namespace NSubstituteLearn
{
    /// <summary>
    /// 05.为任意参数设置返回值
    /// </summary>
    [TestFixture]
    public class NUnitTest5
    {
        /// <summary>
        /// 通过使用 ReturnsForAnyArgs() 方法，可以设置当一个方法被调用后，无论参数是什么，都返回指定的值
        /// ReturnsForAnyArgs() 具有与 Returns() 方法相同的重载，所以也可以指定多个返回值，或者计算返回值。
        /// </summary>
        [Test]
        public void Test_ReturnForAnyArgs_ReturnForAnyArgs()
        {
            var calculator = Substitute.For<ICalculator>();

            calculator.Add(1, 2).ReturnsForAnyArgs(100);

            Assert.AreEqual(calculator.Add(1, 2), 100);
            Assert.AreEqual(calculator.Add(-7, 15), 100);
        }
    }
}
