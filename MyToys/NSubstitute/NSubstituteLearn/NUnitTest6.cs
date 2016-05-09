using System;
using NUnit.Framework;
using NUnit.Common;
using NSubstitute;
using NSubstitute.Exceptions;

namespace NSubstituteLearn
{
    /// <summary>
    /// 06.使用函数设置返回值
    /// </summary>
    [TestFixture]
    public class NUnitTest6
    {
        /// <summary>
        /// 使用参数匹配器来匹配所有对 Add() 方法的调用，
        /// 使用一个 Lambda 函数来计算第一个和第二个参数的和，
        /// 并将计算的结果传递给方法调用
        /// 为 Returns() 和 ReturnsForAnyArgs() 方法提供的函数是一个 Func<CallInfo, T> 类型，
        /// 在这里，T是方法调用将要返回的值的类型，CallInfo 类型提供访问参数列表的能力。
        /// 在下面的示例中，我们使用索引器 indexer 来访问参数列表。
        /// </summary>
        [Test]
        public void Test_ReturnFromFunction_ReturnSum()
        {
            var calculator = Substitute.For<ICalculator>();

            calculator.Add(Arg.Any<int>(), Arg.Any<int>()).Returns(x => (int)x[0] + (int)x[1]);

            Assert.AreEqual(calculator.Add(1, 1), 2);
            Assert.AreEqual(calculator.Add(20, 30), 50);
            Assert.AreEqual(calculator.Add(-73, 9348), 9275);
        }

        /// <summary>
        /// CallInfo 也提供了一个很简便的方法用于通过强类型方式来选择参数
        /// x.Arg<string>() 将返回方法调用中的 string 类型的参数，而没有使用 (string)x[1] 方式
        /// 如果在方法调用中有两个 string 类型的参数，NSubstitute 将通过抛出异常的方式来告诉你无法确定具体是哪个参数
        /// </summary>
        [Test]
        public void Test_ReturnFromFunction_CallInfo()
        {
            var foo = Substitute.For<IFoo>();
            foo.Bar(0, "").ReturnsForAnyArgs(x => "Hello " + x.Arg<string>());

            Assert.AreEqual("Hello World", foo.Bar(1, "World"));
        }


        /// <summary>
        /// 这种技术可用于在调用时访问一个回调函数
        /// </summary>
        [Test]
        public void Test_ReturnFromFunction_GetCallbackWhenever()
        {
            var calculator = Substitute.For<ICalculator>();

            var counter = 0;
            calculator.Add(0, 0).ReturnsForAnyArgs(x =>
            {
                counter++;
                return 0;
            });

            calculator.Add(7, 3);
            calculator.Add(2, 2);
            calculator.Add(11, -3);
            Assert.AreEqual(counter, 3);
        }

        /// <summary>
        /// 也可以在 Returns() 之后通过 AndDoes() 来指定回调
        /// </summary>
        [Test]
        public void Test_ReturnFromFunction_UseAndDoesAfterReturns()
        {
            var calculator = Substitute.For<ICalculator>();

            var counter = 0;
            calculator.Add(0, 0).ReturnsForAnyArgs(x => 0)
                .AndDoes(x => counter++);

            calculator.Add(7, 3);
            calculator.Add(2, 2);
            Assert.AreEqual(counter, 2);
        }
    }
}
