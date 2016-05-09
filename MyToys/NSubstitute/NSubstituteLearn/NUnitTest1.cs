using System;
using NUnit.Framework;
using NUnit.Common;
using NSubstitute;
using NSubstitute.Exceptions;

namespace NSubstituteLearn
{
    /// <summary>
    /// 01.NSubstitute入门基础
    /// </summary>
    [TestFixture]
    public class NUnitTest1
    {
        [Test]
        public void Test_GetStarted_ReturnSpecifiedValue()
        {
            ICalculator calculator = Substitute.For<ICalculator>();
            calculator.Add(1, 2).Returns(3);

            int actual = calculator.Add(1, 2);

            Assert.AreEqual(3, actual);
        }

        /// <summary>
        /// 检查该替代实例是否接收到了一个指定的调用，或者未收到某指定调用
        /// </summary>
        [Test]
        public void Test_GetStarted_ReceivedSpecificCall()
        {
            ICalculator calculator = Substitute.For<ICalculator>();
            calculator.Add(1, 2);

            calculator.Received().Add(1, 2);
            calculator.DidNotReceive().Add(5, 7);
        }

        /// <summary>
        /// 如果 Received() 断言失败，NSubstitute 会尝试给出有可能是什么问题
        /// </summary>
        [Test]
        public void Test_GetStarted_DidNotReceivedSpecificCall()
        {
            ICalculator calculator = Substitute.For<ICalculator>();

            calculator.Add(5, 7);

            Assert.Throws<ReceivedCallsException>(() => calculator.Received().Add(1, 2));
        }

        /// <summary>
        /// 可以对属性使用与方法类似的 Retures() 语法，或者继续使用简单的属性 setter 来设置返回值
        /// </summary>
        [Test]
        public void Test_GetStarted_SetPropertyValue()
        {
            ICalculator calculator = Substitute.For<ICalculator>();

            calculator.Mode.Returns("DEC");
            Assert.AreEqual("DEC", calculator.Mode);

            calculator.Mode = "HEX";
            Assert.AreEqual("HEX", calculator.Mode);

        }

        [Test]
        public void Test_GetStarted_MatchArguments()
        {
            ICalculator calculator = Substitute.For<ICalculator>();

            calculator.Add(10, -5);

            calculator.Received().Add(10, Arg.Any<int>());
            calculator.Received().Add(10, Arg.Is<int>(x => x < 0));
        }

        /// <summary>
        /// 在使用参数匹配功能的同时，传递一个函数给 Returns() ，以此来使替代实例具有更多的功能
        /// </summary>
        [Test]
        public void Test_GetStarted_PassFuncToReturns()
        {
            ICalculator calculator = Substitute.For<ICalculator>();

            calculator.Add(Arg.Any<int>(), Arg.Any<int>())
                .Returns(x => (int)x[0] + (int)x[1]);

            int actual = calculator.Add(5, 10);

            Assert.AreEqual(15, actual);
        }

        /// <summary>
        /// Returns() 也可通过构造一个返回值序列来指定多个参数
        /// </summary>
        [Test]
        public void Test_GetStarted_MultipleValues()
        {
            ICalculator calculator = Substitute.For<ICalculator>();
            calculator.Mode.Returns("HEX", "DEC", "BIN");

            Assert.AreEqual("HEX", calculator.Mode);
            Assert.AreEqual("DEC", calculator.Mode);
            Assert.AreEqual("BIN", calculator.Mode);
        }

        /// <summary>
        /// 可以在替代实例上引发事件通知
        /// </summary>
        [Test]
        public void Test_GetStarted_RaiseEvents()
        {
            ICalculator calculator = Substitute.For<ICalculator>();
            bool eventWasRaised = false;

            calculator.PoweringUp += (sender, args) =>
                {
                    eventWasRaised = true;
                };

            calculator.PoweringUp += Raise.Event();

            Assert.IsTrue(eventWasRaised);
        }
    }
}