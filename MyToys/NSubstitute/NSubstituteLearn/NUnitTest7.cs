using System;
using NUnit.Framework;
using NUnit.Common;
using NSubstitute;
using NSubstitute.Exceptions;

namespace NSubstituteLearn
{
    /// <summary>
    /// 07.设置多个返回值
    /// </summary>
    [TestFixture]
    public class NUnitTest7
    {
        /// <summary>
        /// 可以配置在多次调用中分别返回不同的值(注意调用顺序)。
        /// 下面的示例通过调用属性来演示此功能，对于方法的调用是相同的。
        /// 这种方式也可以通过使用函数返回结果的方式来完成，
        /// 但传递多个值到 Returns() 看起来更加简单，可读性更好
        /// </summary>
        [Test]
        public void Test_MultipleReturnValues_ReturnMultipleValues()
        {
            var calculator = Substitute.For<ICalculator>();

            calculator.Mode.Returns("DEC", "HEX", "BIN");

            Assert.AreEqual("DEC", calculator.Mode);
            Assert.AreEqual("HEX", calculator.Mode);
            Assert.AreEqual("BIN", calculator.Mode);
        }

        /// <summary>
        /// Returns() 也支持传递多个创建返回结果的函数，
        /// 这允许在一系列的调用或者抛异常，或者执行一些动作。
        /// </summary>
        [Test]
        public void Test_MultipleReturnValues_UsingCallbacks()
        {
            var calculator = Substitute.For<ICalculator>();

            calculator.Mode.Returns(x => "DEC", x => "HEX", x => { throw new Exception(); });

            Assert.AreEqual("DEC", calculator.Mode);
            Assert.AreEqual("HEX", calculator.Mode);

            Assert.Throws<Exception>(() => { var result = calculator.Mode; });
        }
    }
}
