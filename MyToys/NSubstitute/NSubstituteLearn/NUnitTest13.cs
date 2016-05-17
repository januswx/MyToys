using System;
using NUnit.Framework;
using NUnit.Common;
using NSubstitute;
using NSubstitute.Exceptions;
using System.Collections.Generic;

namespace NSubstituteLearn
{
    /// <summary>
    /// 抛出异常
    /// </summary>
    [TestFixture]
    public class NUnitTest13
    {
        [Test]
        public void Test_ThrowingExceptions_ForVoid()
        {
            var calculator = Substitute.For<ICalculator>();

            // 对无返回值函数
            calculator.Add(-1, -1).Returns(x => { throw new Exception(); });

            // 抛出异常
            Assert.Throws<Exception>(()=>calculator.Add(-1, -1));
        }

        [Test]
        public void Test_ThrowingExceptions_ForNonVoidAndVoid()
        {
            var calculator = Substitute.For<ICalculator>();

            // 对有返回值或无返回值函数
            calculator.When(x => x.Add(-2, -2)).Do(x => { throw new Exception(); });

            Assert.Throws<Exception>(() => calculator.Add(-2, -2));
        }
    }
}
