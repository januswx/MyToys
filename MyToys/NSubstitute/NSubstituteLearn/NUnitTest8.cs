using System;
using NUnit.Framework;
using NUnit.Common;
using NSubstitute;
using NSubstitute.Exceptions;

namespace NSubstituteLearn
{
    /// <summary>
    /// 08.替换返回值
    /// </summary>
    [TestFixture]
    public class NUnitTest8
    {
        /// <summary>
        /// 一个方法或属性的返回值可以被设置多次。
        /// 只有最后一次设置的值将被返回
        /// </summary>
        [Test]
        public void Test_ReplaceReturnValues_ReplaceSeveralTimes()
        {
            var calculator = Substitute.For<ICalculator>();

            calculator.Mode.Returns("DEC,HEX,OCT");
            calculator.Mode.Returns(x => "???");
            calculator.Mode.Returns("HEX");
            calculator.Mode.Returns("BIN");

            Assert.AreEqual(calculator.Mode, "BIN");
        }
    }
}
